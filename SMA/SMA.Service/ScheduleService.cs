#region

using System.Collections.Generic;
using SMA.Entities.Models;
using SMA.Repository.Repositories;
using Repository.Pattern.Repositories;
using Service.Pattern;

#endregion

namespace SMA.Service
{
    /// <summary>
    ///     Add any custom business logic (methods) here
    /// </summary>
    public interface IScheduleService : IService<Schedule>
    {
       
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class ScheduleService : Service<Schedule>, IScheduleService
    {
        private readonly IRepositoryAsync<Schedule> _repository;


        public ScheduleService(IRepositoryAsync<Schedule> repository)
            : base(repository)
        {
            _repository = repository;
        }

    }
}