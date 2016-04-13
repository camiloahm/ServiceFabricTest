using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace commentdomain
{
    public interface ICommentService:IService
    {
        Task<bool> SaveComment(Comm);
    }
}
