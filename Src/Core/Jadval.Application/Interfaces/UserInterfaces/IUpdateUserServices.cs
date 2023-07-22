using Jadval.Application.Wrappers;
using System.Threading.Tasks;

namespace Jadval.Application.Interfaces.UserInterfaces
{
    public interface IUpdateUserServices
    {
        Task<Result<long>> ChangeUserCoins(long coins);
        Task<Result<long>> LevelUp(long level);
    }
}
