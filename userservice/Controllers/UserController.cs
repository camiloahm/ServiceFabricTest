using System;
using System.Collections.Generic;
using System.Web.Http;
using common;
using mailhandlerdomain;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using userservice.Model;
using userservice.Persistence;

namespace userservice.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private const string SendMailServiceName = "mailhandlerservice";

        public UserController()
        {
            _userService = new UserService();
        }

        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post(User user)
        {
            user.Id=Guid.NewGuid();
            _userService.CreateUser(user);
            ServiceUriBuilder builder = new ServiceUriBuilder(SendMailServiceName);
            IMailHandlerService mailHandlerService = ServiceProxy.Create<IMailHandlerService>(builder.ToUri(),new ServicePartitionKey(1));
            mailHandlerService.SendEmailUserCreated(user.UserName);
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}