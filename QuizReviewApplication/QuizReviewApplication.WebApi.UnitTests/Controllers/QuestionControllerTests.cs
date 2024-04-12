using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MediatR;
using QuizReviewApplication.WebApi.Controllers;
using QuizReviewApplication.Application.Questions.Queries.GetQuestions;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Application.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace QuizReviewApplication.WebApi.UnitTests.Controllers
{
   
    public class QuestionControllerTests
    {
        private readonly Mock<ISender> _senderMock;
        public QuestionControllerTests()
        {
                _senderMock = new ();
        }
        [Fact]
        public async Task GetQuestions_Should_ReturnsOkResponse_WhenDataFound()
        {
            List<QuestionDto> questions = new List<QuestionDto>()
            {
                new QuestionDto
                { Id=Guid.NewGuid(),
                    Content="What is C#",
                    SkillLevel=1,
                    Category="First"
                },
                 new QuestionDto
                {   Id=Guid.NewGuid(),
                    Content="What is .NET",
                    SkillLevel=1,
                    Category="First"
                }
            };
            //Arrange
            var controller = new QuestionController(_senderMock.Object);
             _senderMock.Setup(q => q.Send(It.IsAny<GetQuestionsQuery>(),It.IsAny<CancellationToken>())).ReturnsAsync(questions);
         
            //Act
            var result = await controller.GetQuestions();
           
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<QuestionDto>>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            _senderMock.Verify(q=>q.Send(It.IsAny<GetQuestionsQuery>(), default),Times.Once());

        }
        [Fact]
        public async Task GetQuestions_Should_ReturnsNotFound_WhenDataNotFound()
        {

            //Arrange
            List<QuestionDto> questions = null;
            var controller = new QuestionController(_senderMock.Object); 
            _senderMock.Setup(q => q.Send(It.IsAny<GetQuestionsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(questions);

            //Act
            var result = await controller.GetQuestions();

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            _senderMock.Verify(q => q.Send(It.IsAny<GetQuestionsQuery>(), default), Times.Once());

        }
    }
}
