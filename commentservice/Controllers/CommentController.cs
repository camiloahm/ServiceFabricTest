using System;
using System.Collections.Generic;
using System.Web.Http;
using commentservice.Model;
using commentservice.Persistence;
using common;
using mailhandlerdomain;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using sendemaildomain;

namespace commentservice.Controllers
{
    public class CommentController : ApiController
    {
        private readonly ICommentService _commentService;
        private const string MailHandlerServiceName = "mailhandlerservice";
        private ISendEmailService _sendmailService;
        private const string SendMailServiceName = "sendemailservice";


        public CommentController()
        {
            _commentService=new CommentService();
        }

        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public async void Post(Comment comment)
        {
            comment.Id=Guid.NewGuid();
            _commentService.SaveComment(comment);
            ServiceUriBuilder builder = new ServiceUriBuilder(MailHandlerServiceName);
            IMailHandlerService mailHandlerService = ServiceProxy.Create<IMailHandlerService>(builder.ToUri(),new ServicePartitionKey(1));
            await mailHandlerService.SendEmailCommentCreated(comment.UserName, comment.Message);
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
