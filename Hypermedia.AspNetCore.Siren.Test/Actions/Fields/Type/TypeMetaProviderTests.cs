namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    using Hypermedia.AspNetCore.Siren.Actions.Fields;
    using Hypermedia.AspNetCore.Siren.Actions.Fields.Type;
    using Moq;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class TypeMetaProviderTests
    {
        [Fact]
        private void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new TypeMetadataProvider(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(IEnumerable<ITypeMetaProvider> typeMetaProviders)
        {
            var sut = new TypeMetadataProvider(typeMetaProviders);

            Assert.NotNull(sut);
        }

        [Theory, AutoMockData]
        private void ShouldIterateOverTypeMetaProviders(
            Mock<IEnumerable<ITypeMetaProvider>> typeMetaProviders,
            FieldDescriptor fieldDescriptor
        )
        {
            var sut = new TypeMetadataProvider(typeMetaProviders.Object);
            var meta = sut.GetMetadata(fieldDescriptor).ToArray();

            typeMetaProviders.Verify(e => e.GetEnumerator(), Times.Once);
        }

        [Theory, AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenContextIsNull(
            TypeMetadataProvider sut
        )
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetMetadata(null).ToArray());
        }

        [Theory, AutoMockData]
        private void ShouldCallGetMetadataForEachProvider(
            List<Mock<ITypeMetaProvider>> typeMetaProviderMocks,
            FieldDescriptor fieldDescriptor
        )
        {
            var typeMetaProviders = typeMetaProviderMocks.Select(m => m.Object).ToArray();

            var sut = new TypeMetadataProvider(typeMetaProviders);
            var meta = sut.GetMetadata(fieldDescriptor).ToArray();

            foreach (var typeMetaProviderMock in typeMetaProviderMocks)
            {
                typeMetaProviderMock.Verify(m => m.GetMetadata(fieldDescriptor), Times.Once);
            }
        }

        [Theory, AutoMockData]
        private void ShouldForwardGetMetadataForEachProviderAsReturn(
            List<IEnumerable<KeyValuePair<string, object>>> expectedKeyValuePairs,
            List<Mock<ITypeMetaProvider>> typeMetaProviderMocks,
            FieldDescriptor fieldDescriptor
        )
        {
            var typeMetaProviders = typeMetaProviderMocks.Select(m => m.Object).ToArray();

            for (var index = 0; index < typeMetaProviderMocks.Count; index++)
            {
                var typeMetaProviderMock = typeMetaProviderMocks[index];

                typeMetaProviderMock
                    .Setup(m => m.GetMetadata(fieldDescriptor))
                    .Returns(expectedKeyValuePairs[index]);
            }

            var sut = new TypeMetadataProvider(typeMetaProviders);
            var meta = sut.GetMetadata(fieldDescriptor).ToArray();

            Assert.Equal(meta, expectedKeyValuePairs.SelectMany(kvp => kvp));
        }
    }
}