using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MediatR;
using QuizReviewApplication.WebApi.Controllers;
using QuizReviewApplication.Domain.Entities;
using QuizReviewApplication.Application.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using QuizReviewApplication.WebApi.Controllers.Questions;
using QuizReviewApplication.Application.Features.Questions.Queries.GetQuestions;

namespace QuizReviewApplication.WebApi.UnitTests.Controllers
{

    public class QuestionControllerTests
    {
        private readonly Mock<ISender> _senderMock;
        public QuestionControllerTests()
        {
                _senderMock = new ();
        }
        //[Fact]
        //public async Task GetQuestions_Should_ReturnsGetQuestionsResponse_WhenDataFound()
        //{
        //    var questionResponse = new GetQuestionsResponse();
        //    List<QuestionDto> questions = new List<QuestionDto>()
        //    {
        //        new QuestionDto
        //        { Id=Guid.NewGuid(),
        //            Content="What is C#",
        //            SkillLevel=1,
        //            Category="First"
        //        },
        //         new QuestionDto
        //        {   Id=Guid.NewGuid(),
        //            Content="What is .NET",
        //            SkillLevel=1,
        //            Category="First"
        //        }
        //    };
        //    questionResponse.Questions = questions;
        //    questionResponse.Success = true;
        //    //Arrange
        //    var controller = new QuestionsController(_senderMock.Object);
        //    _senderMock.Setup(q => q.Send(It.IsAny<GetQuestionsQuery>(),It.IsAny<CancellationToken>())).ReturnsAsync(questionResponse);
         
        //    //Act
        //    var result = await controller.GetQuestions();
           
        //    //Assert
        //    result.Value.Questions.Should().NotBeNull();
        //    result.Value.Questions.Should().BeAssignableTo<ActionResult<GetQuestionsResponse>>();
        //    _senderMock.Verify(q=>q.Send(It.IsAny<GetQuestionsQuery>(), default),Times.Once());

        //}
        //[Fact]
        //public async Task GetQuestions_Should_ReturnsEmptyGetQuestionsResponse_WhenDataNotFound()
        //{

        //    //Arrange
        //    var questionResponse = new GetQuestionsResponse();

        //    questionResponse.Questions = new List<QuestionDto>(); ;
        //    var controller = new QuestionsController(_senderMock.Object); 
        //   _senderMock.Setup(q => q.Send(It.IsAny<GetQuestionsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(questionResponse);

        //    //Act
        //    var result = await controller.GetQuestions();

        //    //Assert
        //    result.Value.Questions.Should().BeEmpty();
        //    _senderMock.Verify(q => q.Send(It.IsAny<GetQuestionsQuery>(), default), Times.Once());

        //}
    }
}
