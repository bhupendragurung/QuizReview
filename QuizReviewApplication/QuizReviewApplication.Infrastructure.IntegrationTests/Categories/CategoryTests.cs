﻿
using QuizReviewApplication.Application.Features.Categories.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.IntegrationTests.Categories
{
    public class CategoryTests : BaseIntegrationTest
    {
        public CategoryTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        //createcategrory handler folder
        // CreateCategoryCommandTest file name
        [Fact]
        public async Task Category_Should_AddNewCategoryToDatabase()
        {
            //Arrange
            var command = new CreateCategoryCommand();
            command.Name = ".net";
            command.Value = ".Net";
            //Act
            var categoryId=   await _sender.Send(command);
            //Assert
           var category= _quizReviewDbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
            Assert.NotNull(category);
        }
    }
}
