using System;
using System.Collections.Generic;
using System.Text;

namespace Hypermedia.AspNetCore.ApiExport.Tests
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Internal;
    using Moq;

    public static class Util
    {
        public static Mock<ILogger<T>> Verify<T>(this Mock<ILogger<T>> mock, LogLevel logLevel, string message, Times times) where T : class
        {
            mock.Verify(l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<FormattedLogValues>(v => v.ToString().Contains(message)),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>()
            ), times);

            return mock;
        }
    }
}
