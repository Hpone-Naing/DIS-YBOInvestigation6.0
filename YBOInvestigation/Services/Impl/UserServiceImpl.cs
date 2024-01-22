using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class UserServiceImpl : AbstractServiceImpl<User>, UserService
    {
        private readonly ILogger<UserServiceImpl> _logger;

        public UserServiceImpl(YBOInvestigationDBContext context, ILogger<UserServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public User FindUserById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [UserServiceImpl][FindUserById] Find User by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find User by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding User by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public User FindUserByUserName(string userName)
        {
            _logger.LogInformation(">>>>>>>>>> [UserServiceImpl][FindUserByUserName] Find User by userName. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find User by userName. <<<<<<<<<<");
                return FindByString("UserID", userName);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding User by userName. <<<<<<<<<<" + e);
                throw;
            }
        }

        public User FindUserByUserNameEgerLoad(string userName)
        {
            _logger.LogInformation(">>>>>>>>>> [UserServiceImpl][FindUserByUserNameEgerLoad] Find User by userName with eger load. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find User by userName with eger load. <<<<<<<<<<");
                return _context.Users
                           .Include(user => user.UserType)
                           .FirstOrDefault(user => user.UserID == userName);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding User by userName with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}
