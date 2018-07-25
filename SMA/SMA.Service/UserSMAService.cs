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
    public interface IUserSMAService : IService<UserSMA>
    {
        UserSMA Login(string Login, string PWD);
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class UserSMAService : Service<UserSMA>, IUserSMAService
    {
        private readonly IRepositoryAsync<UserSMA> _repository;


        public UserSMAService(IRepositoryAsync<UserSMA> repository)
            : base(repository)
        {
            _repository = repository;
        }


        public UserSMA Login(string Login, string PWD)
        {
            return _repository.Queryable().Where(s => s.Login == Login && s.Password== PWD).FirstOrDefault();
        }
    }
}