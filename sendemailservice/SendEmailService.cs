using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Fabric;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using sendemaildomain;

namespace sendemailservice
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class SendEmailService : StatelessService, ISendEmailService
    {
        public SendEmailService(StatelessServiceContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        //
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[] { new ServiceInstanceListener(context =>
            this.CreateServiceRemotingListener(context)) };
        }

        public  Task<bool> SendEmail(string to, string from, string subject, string message)
        {
            StringBuilder mail = new StringBuilder();
            mail.Append("To: " + to);
            mail.Append("From: " + from);
            mail.Append("subject: " + subject);
            mail.Append("Message " + message);
            ServiceEventSource.Current.ServiceMessage(this, mail.ToString());
            return Task.FromResult(true);
        }
    }
}