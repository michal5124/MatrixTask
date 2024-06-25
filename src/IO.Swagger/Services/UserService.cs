using IO.Swagger.Models;
using IO.Swagger.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IO.Swagger.Services
{
    public interface IUserService
    {
        void addUserInfo(User userInfo);
        List<User> Get();
        Task<User> getUserInfoByEmail(string email);
        void removeUserInfo(User userInfo);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userInfoRepository;
        public UserService(IUserRepository userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        public async Task<User> getUserInfoByEmail(string email)
        {
            var res = await _userInfoRepository.getUserInfoByEmail(email);
            return res;
        }

        public void addUserInfo(User userInfo)
        {
            _userInfoRepository.Add(userInfo);
        }

        public void removeUserInfo(User userInfo)
        {
            _userInfoRepository.Remove(userInfo);
        }
        public List<User> Get()
        {
            return _userInfoRepository.Get().Result;
        }
    }
}
