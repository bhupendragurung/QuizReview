using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizReviewApplication.Application.Services
{
    public interface IAiServices
    {
        public Task<string> GetResponseFromAi(string prompt);
    }
}
