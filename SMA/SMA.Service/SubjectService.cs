#region

using System.Collections.Generic;
using SMA.Entities.Models;
using SMA.Repository.Repositories;
using Repository.Pattern.Repositories;
using Service.Pattern;

using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace SMA.Service
{
    /// <summary>
    ///     Add any custom business logic (methods) here
    /// </summary>
    public interface ISubjectService : IService<Subject>
    {
        IQueryable<Subject> GetSubjectsWithSubLevel();
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class SubjectService : Service<Subject>, ISubjectService
    {
        private readonly IRepositoryAsync<Subject> _repository;


        public SubjectService(IRepositoryAsync<Subject> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IQueryable<Subject> GetSubjectsWithSubLevel()
        {
            return _repository.Queryable().Include(s=>s.SubjectLevels);
            //return _repository.Queryable();
        }

    }
}