using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace sendemaildomain
{
    public interface ISendEmailService: IService
    {
         Task<bool> SendEmail(string to, string from, string subject,string message);
    }
}