using AutoFixture.Xunit2;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Siren.Actions.Fields.Validation;
using Hypermedia.AspNetCore.Tests.Common;
using Moq;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields
{
    public class ValidationMetadataProviderTests
    {
        [Fact]
        private void ShouldThrowArgumentNullExceptionWhenValidationMetaProvidersIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ValidationMetadataProvider(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(
            IValidationMetaProvider[] validationMetaProviders)
        {
            AssertUtils.NoExceptions(() => new ValidationMetadataProvider(validationMetaProviders));
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenFieldDescriptorIsNull(
            ValidationMetadataProvider sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetMetadata(null).ToList());
        }

        [Theory]
        [AutoMockData]
        private void ShouldTryToGetMetadataFromAllProviders(
            FieldDescriptor fieldDescriptor,
            Mock<IValidationMetaProvider>[] validationMetaProviders)
        {
            var sut = new ValidationMetadataProvider(validationMetaProviders.Select(p => p.Object).ToArray());

            var _ = sut.GetMetadata(fieldDescriptor).ToList();

            foreach (var validationMetaProvider in validationMetaProviders)
            {
                foreach (var customAttribute in fieldDescriptor.CustomAttributes)
                {
                    validationMetaProvider.Verify(p => p.GetMetadata(customAttribute), Times.Once());
                }
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldMakeFieldWithMetadataFromAllProviders(
            KeyValuePair<string, object>[] metaKeyValuePairs,
            [Frozen]Mock<IValidationMetaProvider>[] validationMetaProviders,
            FieldDescriptor fieldDescriptor,
            ValidationMetadataProvider sut)
        {
            foreach (var validationMetaProvider in validationMetaProviders)
            {
                validationMetaProvider.Setup(p => p.GetMetadata(It.IsAny<object>()))
                    .Returns(Enumerable.Empty<KeyValuePair<string, object>>());
            }

            validationMetaProviders
                .Zip(metaKeyValuePairs, (p, m) => (p, m))
                .Zip(fieldDescriptor.CustomAttributes, (pm, a) =>
                {
                    var (p, m) = pm;

                    return (p, m, a);
                })
                .ToList()
                .ForEach((pma) =>
                {
                    var (p, m, a) = pma;
                    p.Setup(provider => provider.GetMetadata(a))
                        .Returns(new[] { m });
                });

            var metadata = sut.GetMetadata(fieldDescriptor).ToList();

            Assert.Equal(metaKeyValuePairs.ToList(), metadata);
        }
    }
}
