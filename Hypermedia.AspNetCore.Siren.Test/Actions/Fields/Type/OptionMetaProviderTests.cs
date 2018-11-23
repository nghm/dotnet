
namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    using AutoFixture.Xunit2;
    using Hypermedia.AspNetCore.Siren.Actions.Fields;
    using Hypermedia.AspNetCore.Siren.Actions.Fields.Type;
    using Moq;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class OptionMetaProviderTests
    {
        [Fact]
        private void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new OptionMetaProvider(null);
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(IEnumOptionsExtractor enumOptionsExtractor)
        {
            try
            {
                var _ = new OptionMetaProvider(enumOptionsExtractor);
            }
            catch
            {
                Assert.True(false, "Exception was thrown when none was expected!");
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenGettingMetadata(
            OptionMetaProvider optionMetaProvider)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = optionMetaProvider.GetMetadata(null)
                    .ToArray();
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetOptionsFromEnumOptionsExtractor(
            [Frozen] Mock<IEnumOptionsExtractor> enumOptionsExtractor,
            FieldGenerationContext fieldGenerationContext,
            OptionMetaProvider optionMetaProvider)
        {
            var _ = optionMetaProvider.GetMetadata(fieldGenerationContext).ToArray();

            FieldOption[] options;
            enumOptionsExtractor
                .Verify(e =>
                        e.TryGetEnumOptions(
                            fieldGenerationContext.FieldDescriptor.PropertyType,
                            out options
                        ), Times.Once);

        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnEmptyMetadata(
            [Frozen] Mock<IEnumOptionsExtractor> enumOptionsExtractor,
            FieldGenerationContext fieldGenerationContext,
            OptionMetaProvider optionMetaProvider)
        {
            FieldOption[] options;
            enumOptionsExtractor
                .Setup(e =>
                    e.TryGetEnumOptions(
                        fieldGenerationContext.FieldDescriptor.PropertyType,
                        out options
                    ))
                .Returns(false);

            var metadata = optionMetaProvider.GetMetadata(fieldGenerationContext).ToArray();

            Assert.Empty(metadata);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadata(
            [Frozen] Mock<IEnumOptionsExtractor> enumOptionsExtractor,
            FieldGenerationContext fieldGenerationContext,
#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
            FieldOption[] expectedFieldOptions,
#pragma warning restore xUnit1026 // Theory methods should use all of their parameters
            OptionMetaProvider optionMetaProvider)
        {
            enumOptionsExtractor
                .Setup(e =>
                    e.TryGetEnumOptions(
                        fieldGenerationContext.FieldDescriptor.PropertyType,
                        out expectedFieldOptions
                    ))
                .Returns(true);

            var metadata = optionMetaProvider.GetMetadata(fieldGenerationContext).ToArray();

            Assert.Equal(new KeyValuePair<string, object>("type", "option"), metadata[0]);
            Assert.Same("options", metadata[1].Key);
            Assert.Same(expectedFieldOptions, metadata[1].Value);
        }
    }
}