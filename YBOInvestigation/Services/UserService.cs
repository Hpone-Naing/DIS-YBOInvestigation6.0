using YBOInvestigation.Models;

namespace YBOInvestigation.Services
{
    public interface UserService
    {
        User FindUserById(int id);
        User FindUserByUserName(string userName);
        User FindUserByUserNameEgerLoad(string userName);

    }
}
