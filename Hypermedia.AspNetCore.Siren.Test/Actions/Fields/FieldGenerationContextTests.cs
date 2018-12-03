using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields
{
    public class FieldGenerationContextTests
    {
        [Fact]
        private void ShouldThrowArgumentNullExceptionWhenFieldDescriptorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FieldGenerationContext(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(
            FieldDescriptor descriptor)
        {
            try
            {
                var _ = new FieldGenerationContext(descriptor);
            }
            catch
            {
                Assert.True(false, "Exception was thrown when none was expected!");
            }
        }
    }
}