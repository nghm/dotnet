using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Siren.Util;
using Newtonsoft.Json;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields
{
    using Builders.Abstractions;

    public class FieldJsonConverterTests
    {
        [Theory]
        [InlineAutoMockData(typeof(IField))]
        [InlineAutoMockData(typeof(Field))]
        private void ShouldReturnTrue(
            System.Type type,
            FieldJsonConverter sut)
        {
            var canConvert = sut.CanConvert(type);

            Assert.True(canConvert);
        }

        [Theory]
        [InlineAutoMockData(null)]
        [InlineAutoMockData(typeof(object))]
        [InlineAutoMockData(typeof(int))]
        [InlineAutoMockData(typeof(string))]
        [InlineAutoMockData(typeof(DataType))]
        private void ShouldReturnFalse(
            System.Type type,
            FieldJsonConverter sut)
        {
            var canConvert = sut.CanConvert(type);

            Assert.False(canConvert);
        }

        [Theory]
        [AutoMockData]
        private void ShouldRemainNotImplemented(
            FieldJsonConverter sut)
        {
            Assert.Throws<NotImplementedException>(() => sut.ReadJson(
                null,
                null,
                null,
                null)
            );
        }

        [Theory]
        [InlineAutoMockData(null)]
        [InlineAutoMockData(1)]
        [InlineAutoMockData("string")]
        [InlineAutoMockData(DataType.Custom)]
        private void ShouldThrowInvalidCastExceptionWhenValueIsNotField(
            object value,
            JsonWriter writer,
            JsonSerializer serializer,
            FieldJsonConverter sut)
        {
            try
            {
                sut.WriteJson(writer, value, serializer);

                Assert.True(false, "An exception was expected, but none was thrown.");
            }
            catch (InvalidCastException e)
            {
                Assert.Equal(e.Message, $"Object is not {nameof(Field)}!");
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldUseCamelCaseNameFromField(
            Field field,
            FieldJsonConverter sut)
        {
            var stringBuilder = new StringBuilder();
            var writer = new JsonTextWriter(new StringWriter(stringBuilder));

            sut.WriteJson(writer, field, null);
            var result = stringBuilder.ToString();

            Assert.Contains(
                "\"name\":\"" + field.Name.ToCamelCase() + "\"",
                result);
        }

        [Theory]
        [AutoMockData]
        private void ShouldUseValueFromField(
            Field field,
            FieldJsonConverter sut)
        {
            var stringBuilder = new StringBuilder();
            var writer = new JsonTextWriter(new StringWriter(stringBuilder));

            sut.WriteJson(writer, field, null);
            var result = stringBuilder.ToString();

            Assert.Contains(
                "\"value\":{}",
                result);
        }

        [Theory]
        [AutoMockData]
        private void ShouldSkipValueWhenItIsNull(
            string name,
            FieldJsonConverter sut)
        {
            var field = new Field(name, null);
            var stringBuilder = new StringBuilder();
            var writer = new JsonTextWriter(new StringWriter(stringBuilder));

            sut.WriteJson(writer, field, null);
            var result = stringBuilder.ToString();

            Assert.DoesNotContain(
                "\"value\":",
                result);
        }

        [Theory]
        [AutoMockData]
        private void ShouldUseMetadataFromField(
            Field field,
            FieldJsonConverter sut)
        {
            var stringBuilder = new StringBuilder();
            var writer = new JsonTextWriter(new StringWriter(stringBuilder));

            sut.WriteJson(writer, field, null);
            var result = stringBuilder.ToString();

            foreach (var meta in field.GetMetadata())
            {
                Assert.Contains(
                    "\"" + meta.Key + "\":{}",
                    result);
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldSkipDuplicateMetaValues(
            string name,
            KeyValuePair<string, object> meta,
            FieldJsonConverter sut)
        {
            var metadata = new[]
            {
                meta, meta, meta
            };
            var field = new Field(name, null, metadata);
            var stringBuilder = new StringBuilder();
            var writer = new JsonTextWriter(new StringWriter(stringBuilder));

            try
            {
                sut.WriteJson(writer, field, null);
                var _ = stringBuilder.ToString();
            }
            catch
            {
                Assert.True(false, "Exception was thrown when none was expected!");
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldSkipNullMetaValues(
            string name,
            KeyValuePair<string, object> meta,
            FieldJsonConverter sut)
        {
            var metadata = new[]
            {
                meta,
                new KeyValuePair<string, object>("1", null),
                new KeyValuePair<string, object>("2", null),
            };
            var field = new Field(name, null, metadata);
            var stringBuilder = new StringBuilder();
            var writer = new JsonTextWriter(new StringWriter(stringBuilder));

            sut.WriteJson(writer, field, null);
            var result = stringBuilder.ToString();

            Assert.DoesNotContain(
                "\"1\":",
                result);

            Assert.DoesNotContain(
                "\"2\":",
                result);

            Assert.Contains(
                "\"" + meta.Key + "\":{}",
                result);
        }
    }
}