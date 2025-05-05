using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Features.Users.Queries
{
    public class GetTokenQuery:IRequest<string>
    {
    }
}
