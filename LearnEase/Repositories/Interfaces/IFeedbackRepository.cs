using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces.Base;

namespace LearnEase.Repositories.Interfaces
{
    public interface IFeedbackRepository :
        IGetById<Feedback, int>, ICreateAsync<Feedback>, 
        IDeleteAsync<int>, IPutAsync<Feedback>
    {
        Task<IEnumerable<Feedback>> GetAllByCourseIdAsync(int courseId);
    }
}