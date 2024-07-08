//using AssetManagement.API.Middlewares;
//using AssetManagement.Domain.Exceptions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Moq;


//namespace AssetManagement.API.Tests.Middlewares
//{
//    public class CustomAuthenticationMiddlewareTests
//    {
//        [Fact]
//        public async Task InvokeAsync_ShouldCallNextDelegate_WhenNoException()
//        {
//            // Arrange
//            var mockLogger = new Mock<ILogger<CustomAuthenticationMiddleware>>();
//            var mockRequestDelegate = new Mock<RequestDelegate>();
//            var context = new DefaultHttpContext();
//            var middleware = new CustomAuthenticationMiddleware(mockRequestDelegate.Object, mockLogger.Object);

//            // Act
//            await middleware.InvokeAsync(context);

//            // Assert
//            mockRequestDelegate.Verify(rd => rd.Invoke(context), Times.Once);
//        }

//        [Fact]
//        public async Task InvokeAsync_ShouldReturn401_WhenUnauthorizedAccessExceptionThrown()
//        {
//            // Arrange
//            var mockLogger = new Mock<ILogger<CustomAuthenticationMiddleware>>();
//            var mockRequestDelegate = new Mock<RequestDelegate>();
//            mockRequestDelegate.Setup(rd => rd.Invoke(It.IsAny<HttpContext>())).Throws(new UnauthorizedAccessException());
//            var context = new DefaultHttpContext();

//            // Reset response body stream
//            var responseBodyStream = new MemoryStream();
//            context.Response.Body = responseBodyStream;

//            var middleware = new CustomAuthenticationMiddleware(mockRequestDelegate.Object, mockLogger.Object);

//            // Act
//            await middleware.InvokeAsync(context);

//            // Assert
//            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);

//            // Reset the position of the response body stream
//            context.Response.Body.Seek(0, SeekOrigin.Begin);
//            var reader = new StreamReader(context.Response.Body);
//            var responseBody = await reader.ReadToEndAsync();
//            Assert.Contains("Unauthorized: Access is denied.", responseBody);
//        }

//        [Fact]
//        public async Task InvokeAsync_ShouldReturn403_WhenForbidExceptionThrown()
//        {
//            // Arrange
//            var mockLogger = new Mock<ILogger<CustomAuthenticationMiddleware>>();
//            var mockRequestDelegate = new Mock<RequestDelegate>();
//            mockRequestDelegate.Setup(rd => rd.Invoke(It.IsAny<HttpContext>())).Throws(new ForbidException());
//            var context = new DefaultHttpContext();

//            // Reset response body stream
//            var responseBodyStream = new MemoryStream();
//            context.Response.Body = responseBodyStream;

//            var middleware = new CustomAuthenticationMiddleware(mockRequestDelegate.Object, mockLogger.Object);

//            // Act
//            await middleware.InvokeAsync(context);

//            // Assert
//            Assert.Equal(StatusCodes.Status403Forbidden, context.Response.StatusCode);

//            // Reset the position of the response body stream
//            context.Response.Body.Seek(0, SeekOrigin.Begin);
//            var reader = new StreamReader(context.Response.Body);
//            var responseBody = await reader.ReadToEndAsync();
//            Assert.Contains("Forbidden: You do not have permission to access this resource.", responseBody);
//        }

//        [Fact]
//        public async Task InvokeAsync_ShouldReturn500_WhenGenericExceptionThrown()
//        {
//            // Arrange
//            var mockLogger = new Mock<ILogger<CustomAuthenticationMiddleware>>();
//            var mockRequestDelegate = new Mock<RequestDelegate>();
//            mockRequestDelegate.Setup(rd => rd.Invoke(It.IsAny<HttpContext>())).Throws(new Exception("Test exception"));
//            var context = new DefaultHttpContext();

//            // Reset response body stream
//            var responseBodyStream = new MemoryStream();
//            context.Response.Body = responseBodyStream;

//            var middleware = new CustomAuthenticationMiddleware(mockRequestDelegate.Object, mockLogger.Object);

//            // Act
//            await middleware.InvokeAsync(context);

//            // Assert
//            Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);

//            // Reset the position of the response body stream
//            context.Response.Body.Seek(0, SeekOrigin.Begin);
//            var reader = new StreamReader(context.Response.Body);
//            var responseBody = await reader.ReadToEndAsync();
//            Assert.Contains("Internal Server Error: Test exception", responseBody);
//        }

//    }
//}