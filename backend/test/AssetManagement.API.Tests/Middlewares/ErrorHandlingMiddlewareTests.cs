using AssetManagement.API.Middlewares;
using AssetManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
namespace AssetManagement.API.Tests.Middlewares
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_ShouldReturn404_WhenNotFoundExceptionThrown()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var mockRequestDelegate = new Mock<RequestDelegate>();
            mockRequestDelegate.Setup(rd => rd.Invoke(It.IsAny<HttpContext>())).Throws(new NotFoundException("Ex","Not found"));
            var context = new DefaultHttpContext();
            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            var middleware = new ErrorHandlingMiddleware(mockLogger.Object);

            // Act
            await middleware.InvokeAsync(context, mockRequestDelegate.Object);

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, context.Response.StatusCode);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var responseBody = await reader.ReadToEndAsync();
            Assert.Contains("Not found", responseBody);
        }

        [Fact]
        public async Task InvokeAsync_ShouldReturn401_WhenUnauthorizedAccessExceptionThrown()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var mockRequestDelegate = new Mock<RequestDelegate>();
            mockRequestDelegate.Setup(rd => rd.Invoke(It.IsAny<HttpContext>())).Throws(new UnauthorizedException());
            var context = new DefaultHttpContext();
            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            var middleware = new ErrorHandlingMiddleware(mockLogger.Object);

            // Act
            await middleware.InvokeAsync(context, mockRequestDelegate.Object);

            // Assert
            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var responseBody = await reader.ReadToEndAsync();
            Assert.Contains("Unauthorized", responseBody);
        }

        [Fact]
        public async Task InvokeAsync_ShouldReturn403_WhenForbidExceptionThrown()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var mockRequestDelegate = new Mock<RequestDelegate>();
            mockRequestDelegate.Setup(rd => rd.Invoke(It.IsAny<HttpContext>())).Throws(new ForbidException("Forbidden"));
            var context = new DefaultHttpContext();
            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            var middleware = new ErrorHandlingMiddleware(mockLogger.Object);

            // Act
            await middleware.InvokeAsync(context, mockRequestDelegate.Object);

            // Assert
            Assert.Equal(StatusCodes.Status403Forbidden, context.Response.StatusCode);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var responseBody = await reader.ReadToEndAsync();
            Assert.Contains("Forbidden", responseBody);
        }

        [Fact]
        public async Task InvokeAsync_ShouldReturn500_WhenGenericExceptionThrown()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var mockRequestDelegate = new Mock<RequestDelegate>();
            mockRequestDelegate.Setup(rd => rd.Invoke(It.IsAny<HttpContext>())).Throws(new Exception("Test exception"));
            var context = new DefaultHttpContext();
            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            var middleware = new ErrorHandlingMiddleware(mockLogger.Object);

            // Act
            await middleware.InvokeAsync(context, mockRequestDelegate.Object);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var responseBody = await reader.ReadToEndAsync();
            Assert.Contains("Internal Server Error", responseBody);

            // Verify the logger was called with the error
            mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}