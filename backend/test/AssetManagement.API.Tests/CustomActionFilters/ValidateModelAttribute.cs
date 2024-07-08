//using Xunit;
//using Moq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System.Collections.Generic;
//using AssetManagement.API.CustomActionFilters;
//using Microsoft.AspNetCore.Mvc.ModelBinding;

//namespace AssetManagement.API.Tests.CustomActionFilters
//{
//    public class ValidateModelAttributeTests
//    {
//        private readonly ValidateModelAttribute _validateModelAttribute;

//        public ValidateModelAttributeTests()
//        {
//            _validateModelAttribute = new ValidateModelAttribute();
//        }

//        [Fact]
//        public void OnActionExecuted_SetsBadRequestResult_WhenModelStateIsInvalid()
//        {
//            // Arrange
//            var actionContext = new ActionContext
//            {
//                ModelState = new ModelStateDictionary()
//            };
//            actionContext.ModelState.AddModelError("Test", "Test error");

//            var actionExecutedContext = new ActionExecutedContext(
//                actionContext,
//                new List<IFilterMetadata>(),
//                new Mock<Controller>().Object
//            );

//            // Act
//            _validateModelAttribute.OnActionExecuted(actionExecutedContext);

//            // Assert
//            Assert.IsType<BadRequestResult>(actionExecutedContext.Result);
//        }

//        [Fact]
//        public void OnActionExecuted_DoesNotSetResult_WhenModelStateIsValid()
//        {
//            // Arrange
//            var actionContext = new ActionContext
//            {
//                ModelState = new ModelStateDictionary()
//            };

//            var actionExecutedContext = new ActionExecutedContext(
//                actionContext,
//                new List<IFilterMetadata>(),
//                new Mock<Controller>().Object
//            );

//            // Act
//            _validateModelAttribute.OnActionExecuted(actionExecutedContext);

//            // Assert
//            Assert.Null(actionExecutedContext.Result);
//        }
//    }
//}
