using MediatR;
using QuizReviewApplication.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Categories.Commands
{
    public class CreateCategoryCommand : IRequest<ApiResponse<Guid>>
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

}
