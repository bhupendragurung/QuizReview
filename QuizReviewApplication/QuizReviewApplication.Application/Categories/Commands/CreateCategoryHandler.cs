using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Questions.Commands.CreateQuestion;
using QuizReviewApplication.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Categories.Commands
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
         return await  _categoryRepository.CreateCategoryAsync(request.Name, request.Value);
          
        }
    }
}
