using MediatR;
using Microsoft.Extensions.Logging;
using QuizReviewApplication.Application.Dtos;
using QuizReviewApplication.Application.Helper;
using QuizReviewApplication.Application.Repositories;
using QuizReviewApplication.Domain.Entities;
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
        private readonly ILogger<CreateCategoryHandler> _logger;

        public CreateCategoryHandler(ICategoryRepository categoryRepository,ILogger<CreateCategoryHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }
        public async Task<ApiResponse<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryId = await _categoryRepository.CreateCategoryAsync(request.Name, request.Value);

            if (categoryId == Guid.Empty)
            {
                _logger.LogWarning("Failed to create category: {Name}", request.Name);
                return ApiResponse<Guid>.FailureResponse("Failed to create category.");
            }
            _logger.LogInformation("Category created: {Id}", categoryId);
            return ApiResponse<Guid>.SuccessResponse( "Category created successfully.", categoryId);

        }
    }
}
