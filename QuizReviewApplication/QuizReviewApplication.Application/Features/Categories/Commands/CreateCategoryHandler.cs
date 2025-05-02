using MediatR;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Categories.Commands
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<Guid>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<ApiResponse<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryId = await _categoryRepository.CreateCategoryAsync(request.Name, request.Value);

            if (categoryId == Guid.Empty)
            {
                return ApiResponse<Guid>.FailureResponse("Failed to create category.");
            }

            return ApiResponse<Guid>.SuccessResponse( "Category created successfully.", categoryId);

        }
    }
}
