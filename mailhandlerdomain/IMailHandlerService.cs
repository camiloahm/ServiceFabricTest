using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace mailhandlerdomain
{
    public interface IMailHandlerService:IService
    {
        Task<bool> SendEmailUserCreated(string user);
        Task<bool> SendEmailCommentCreated(string user,string comment);
    }
}
