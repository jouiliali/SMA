#region

using System.Collections.Generic;
using SMA.Entities.Models;
using SMA.Repository.Repositories;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System.Linq;
using System;
using System.Data.Entity;

#endregion

namespace SMA.Service
{
    /// <summary>
    ///     Add any custom business logic (methods) here
    /// </summary>
    public interface ISubjectLevelService : IService<SubjectLevel>
    {
        IQueryable<SubjectLevel> GetSubjectLevelsWithSubLevels();
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class SubjectLevelService : Service<SubjectLevel>, ISubjectLevelService
    {
        private readonly IRepositoryAsync<SubjectLevel> _repository;


        public SubjectLevelService(IRepositoryAsync<SubjectLevel> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IQueryable<SubjectLevel> GetSubjectLevelsWithSubLevels()
        {
            return _repository.Queryable().Include(s => s.Subject)
                                          .Include(p => p.Level)
                                          .Include(p => p.Schedules)

                ;
        }
    }
}