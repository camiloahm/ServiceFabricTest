using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using common;
using mailhandlerdomain;
using mailhandlerservice;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using sendemaildomain;

namespace mailhandler
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class MailHandlerService : StatefulService, IMailHandlerService
    {

        private ISendEmailService _sendmailService;
        private const string SendMailServiceName = "sendemailservice";
        private int counter=0;

        public MailHandlerService(StatefulServiceContext context)
            : base(context)
        { }

        public MailHandlerService(StatefulServiceContext context, IReliableStateManagerReplica stateManagerReplica) : base(context, stateManagerReplica)
        {
        }

        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                new ServiceReplicaListener(context => this.CreateServiceRemotingListener(context))
            };
        }

        public Task<bool> SendEmailUserCreated(string user)
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(SendMailServiceName);
            _sendmailService = ServiceProxy.Create<ISendEmailService>(builder.ToUri());
            _sendmailService.SendEmail(user, user, "User creation", "User " + user + " was created");
            counter++;
            ServiceEventSource.Current.ServiceMessage(this, "COUNTER: "+counter);
            return Task.FromResult(true);
        }

        public Task<bool> SendEmailCommentCreated(string user, string comment)
        {
            ServiceUriBuilder builder = new ServiceUriBuilder(SendMailServiceName);
            _sendmailService = ServiceProxy.Create<ISendEmailService>(builder.ToUri());
            _sendmailService.SendEmail(user, user, "Comment Created", "There is a new comment for you: "+comment);
            counter++;
            ServiceEventSource.Current.ServiceMessage(this, "COUNTER: " + counter);
            return Task.FromResult(true);
        }
    }
}
