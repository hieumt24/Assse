//using AssetManagement.Domain.Common.Settings;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using Moq;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace AssetManagement.API.Middlewares.Tests
//{
//    public class TokenValidationMiddlewareTests
//    {
//        [Fact]
//        public async Task InvokeAsync_ShouldReturn401_WhenNoAuthorizationHeader()
//        {
//            // Arrange
//            var mockLogger = new Mock<ILogger<ResponseTimeMiddleware>>();
//            var mockRequestDelegate = new Mock<RequestDelegate>();
//            var jwtSettings = Options.Create(new JWTSettings { Key = "test_key", Issuer = "test_issuer", Audience = "test_audience" });

//            var context = new DefaultHttpContext();
//            var responseBodyStream = new MemoryStream();
//            context.Response.Body = responseBodyStream;

//            var middleware = new TokenValidationMiddleware(mockRequestDelegate.Object, jwtSettings, mockLogger.Object);

//            // Act
//            await middleware.InvokeAsync(context);

//            // Assert
//            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
//            context.Response.Body.Seek(0, SeekOrigin.Begin);
//            var reader = new StreamReader(context.Response.Body);
//            var responseBody = await reader.ReadToEndAsync();
//            Assert.Contains("Unauthorized request", responseBody);

//            // Verify log
//            mockLogger.Verify(
//                logger => logger.Log(
//                    LogLevel.Information,
//                    It.IsAny<EventId>(),
//                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unauthorized request")),
//                    null,
//                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
//                Times.Once);
//        }

//        [Fact]
//        public async Task InvokeAsync_ShouldReturn401_WhenInvalidToken()
//        {
//            // Arrange
//            var mockLogger = new Mock<ILogger<ResponseTimeMiddleware>>();
//            var mockRequestDelegate = new Mock<RequestDelegate>();
//            var jwtSettings = Options.Create(new JWTSettings { Key = "test_key", Issuer = "test_issuer", Audience = "test_audience" });

//            var context = new DefaultHttpContext();
//            context.Request.Headers["Authorization"] = "Bearer invalid_token";
//            var responseBodyStream = new MemoryStream();
//            context.Response.Body = responseBodyStream;

//            var middleware = new TokenValidationMiddleware(mockRequestDelegate.Object, jwtSettings, mockLogger.Object);

//            // Act
//            await middleware.InvokeAsync(context);

//            // Assert
//            Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
//            context.Response.Body.Seek(0, SeekOrigin.Begin);
//            var reader = new StreamReader(context.Response.Body);
//            var responseBody = await reader.ReadToEndAsync();
//            Assert.Contains("Unauthorized request", responseBody);

//            // Verify log
//            mockLogger.Verify(
//                logger => logger.Log(
//                    LogLevel.Information,
//                    It.IsAny<EventId>(),
//                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Unauthorized request")),
//                    null,
//                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
//                Times.Once);
//        }



//        [Fact]
//        public async Task InvokeAsync_ShouldCallNext_WhenValidToken()
//        {
//            // Arrange
//            var mockLogger = new Mock<ILogger<ResponseTimeMiddleware>>();
//            var mockRequestDelegate = new Mock<RequestDelegate>();
//            mockRequestDelegate
//                .Setup(rd => rd(It.IsAny<HttpContext>()))
//                .Returns(Task.CompletedTask);
//            var jwtSettings = Options.Create(new JWTSettings { Key = "test_key", Issuer = "test_issuer", Audience = "test_audience" });

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.UTF8.GetBytes(jwtSettings.Value.Key);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new System.Security.Claims.ClaimsIdentity(),
//                Expires = DateTime.UtcNow.AddMinutes(5),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
//                Issuer = jwtSettings.Value.Issuer,
//                Audience = jwtSettings.Value.Audience
//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            var tokenString = tokenHandler.WriteToken(token);

//            var context = new DefaultHttpContext();
//            context.Request.Headers["Authorization"] = $"Bearer {tokenString}";

//            var middleware = new TokenValidationMiddleware(mockRequestDelegate.Object, jwtSettings, mockLogger.Object);

//            // Act
//            await middleware.InvokeAsync(context);

//            // Assert
//            mockRequestDelegate.Verify(rd => rd(context), Times.Once);
//        }
//    }
//}
