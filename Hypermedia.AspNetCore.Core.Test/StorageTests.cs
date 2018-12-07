using System;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Hypermedia.AspNetCore.Core.Test
{
    using Xunit;
    using Objectivity.AutoFixture.XUnit2.AutoFakeItEasy.Attributes;

    public class StorageTests
    {
        [Theory]
        [AutoMockData]
        private void ShouldReturnCountOfElements(string[] elements)
        {
            var storage = new Storage<string>();

            foreach (var element in elements)
            {
                storage.Add(element);
            }

            Assert.Equal(elements.Length, storage.Count);
        }

        [Theory]
        [AutoMockData]
        private void ShouldAddElements(string[] elements)
        {
            var storage = new Storage<string>();

            foreach (var element in elements)
            {
                storage.Add(element);
            }

            Assert.Equal(elements, storage);
        }

        [Theory]
        [AutoMockData]
        private void ShouldClearElements(string[] elements)
        {
            var storage = new Storage<string>();

            foreach (var element in elements)
            {
                storage.Add(element);
            }

            storage.Clear();

            Assert.Empty(storage);
        }

        [Theory]
        [AutoMockData]
        private void ShouldCheckIfElementIsInCollection(string element)
        {
            const int startIndex = 2;
            var storage = new Storage<string> {element};
            var targetArray = new string[startIndex + storage.Count];

            for (var i = 0; i < startIndex; i++)
            {
                targetArray[i] = i.ToString();
            }

            storage.CopyTo(targetArray, startIndex);

            Assert.Equal(targetArray.Skip(startIndex).Take(storage.Count), storage);
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetEnumerator(string element)
        {
            var storage = new Storage<string> {element};

            using (var enumerator = storage.GetEnumerator())
            {
                Assert.Null(enumerator.Current);
                Assert.True(enumerator.MoveNext());
                Assert.Equal(element, enumerator.Current);
                Assert.False(enumerator.MoveNext());
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldRemoveElement(
            string singleElement,
            string[] moreElements)
        {
            var storage = new Storage<string> {singleElement};

            foreach (var element in moreElements)
            {
                storage.Add(element);
            }

            storage.Remove(singleElement);

            Assert.Equal(moreElements, storage);
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetEnumeratorFromIEnumerable(string element)
        {
            IEnumerable storage = new Storage<string> {element};

            var enumerator = storage.GetEnumerator();

            enumerator.Reset();

            Assert.True(enumerator.MoveNext());
            Assert.Equal(element, enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }
    }
}
