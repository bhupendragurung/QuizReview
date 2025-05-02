
using QuizReviewApplication.Application.Features.Questions.Commands.CreateQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.IntegrationTests.Questions
{
    public class QuestionTests:BaseIntegrationTest
    {
        public QuestionTests(IntegrationTestWebAppFactory factory):base(factory)
        {
                
        }
        //[Fact]
        //public async Task Question_Should_ReturnQuestionList_WhenDataFound()
        //{
        //    //Arrange

        //    //Act
        //    //Assert

        //}
        [Fact]
        public async Task Question_Should_AddNewQuestionToDatabase()
        {
            //Arrange
            var command = new CreateQuestionCommand();
            command.Content = "What is .net";
            command.SkillLevel = 1;
            command.QuestionLevel = 1;
            command.CategoryId = Guid.NewGuid();

            //Act
            await _sender.Send(command);
            //Assert

        }
    }
}
