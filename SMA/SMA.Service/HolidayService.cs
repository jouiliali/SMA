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
    public interface IHolidayService : IService<Holiday>
    {
       
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class HolidayService : Service<Holiday>, IHolidayService
    {
        private readonly IRepositoryAsync<Holiday> _repository;


        public HolidayService(IRepositoryAsync<Holiday> repository)
            : base(repository)
        {
            _repository = repository;
        }

    }
}