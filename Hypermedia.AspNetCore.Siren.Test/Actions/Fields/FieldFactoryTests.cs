using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoFixture.Xunit2;
using Hypermedia.AspNetCore.Siren.Actions;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Moq;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using Xunit;
using Xunit.Abstractions;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields
{
    public class FieldFactoryTests
    {
        [Fact]
        private void ShouldThrowArgumentNullExceptionWhenFieldMetadataProviderCollectionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FieldFactory(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(
            IFieldMetadataProviderCollection fieldMetadataProviderCollection)
        {
            try
            {
                var _ = new FieldFactory(fieldMetadataProviderCollection);
            }
            catch
            {
                Assert.True(false, "Exception was thrown when none was expected!");
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenFieldDescriptorIsNull(
            FieldFactory sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.MakeField(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetMetadataProvidersFromProviderCollection(
            FieldDescriptor fieldDescriptor,
            [Frozen]Mock<IFieldMetadataProviderCollection> fieldMetadataProviderCollection,
            FieldFactory sut)
        {
            var _ = sut.MakeField(fieldDescriptor);

            fieldMetadataProviderCollection.Verify(c => c.GetMetadataProviders(), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private void ShouldTryToGetMetadataFromAllProviders(
            FieldDescriptor fieldDescriptor,
            [Frozen]IEnumerable<Mock<IFieldMetadataProvider>> fieldMetadataProviders,
            [Frozen]Mock<IFieldMetadataProviderCollection> fieldMetadataProviderCollection,
            FieldFactory sut)
        {
            fieldMetadataProviderCollection.Setup(c => c.GetMetadataProviders())
                .Returns(fieldMetadataProviders.Select(p => p.Object));

            var _ = sut.MakeField(fieldDescriptor);

            foreach (var fieldMetadataProvider in fieldMetadataProviders)
            {
                fieldMetadataProvider.Verify(p => p.GetMetadata(fieldDescriptor), Times.Once());
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldMakeFieldWithNameFromDescriptor(
            FieldDescriptor fieldDescriptor,
            FieldFactory sut)
        {
            var field = sut.MakeField(fieldDescriptor) as Field;

            Assert.Equal(fieldDescriptor.Name, field.Name);
        }

        [Theory]
        [AutoMockData]
        private void ShouldMakeFieldWithValueFromDescriptor(
            FieldDescriptor fieldDescriptor,
            FieldFactory sut)
        {
            var field = sut.MakeField(fieldDescriptor) as Field;

            Assert.Equal(fieldDescriptor.Value, field.Value);
        }

        [Theory]
        [AutoMockData]
        private void ShouldMakeFieldWithMetadataFromAllProviders(
            KeyValuePair<string, object>[] metaKeyValuePairs,
            [Frozen]Mock<IFieldMetadataProvider>[] fieldMetadataProviders,
            [Frozen]Mock<IFieldMetadataProviderCollection> fieldMetadataProviderCollection,
            FieldDescriptor fieldDescriptor,
            FieldFactory sut)
        {
            var metaCount = metaKeyValuePairs.Length;
            var providerCount = fieldMetadataProviders.Length;
            var metaPerProvider = metaCount / providerCount;

            fieldMetadataProviderCollection.Setup(c => c.GetMetadataProviders())
                .Returns(fieldMetadataProviders.Select(p => p.Object));

            for (var i = 0; i < providerCount; ++i)
            {
                fieldMetadataProviders[i].Setup(p => p.GetMetadata(fieldDescriptor))
                    .Returns(metaKeyValuePairs.Skip(i * metaPerProvider).Take(metaPerProvider));
            }

            var field = sut.MakeField(fieldDescriptor) as Field;

            Assert.Equal(metaKeyValuePairs, field.GetMetadata());
        }
    }
}
