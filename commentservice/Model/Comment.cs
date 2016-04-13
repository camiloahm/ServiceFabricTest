using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commentservice.Model
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}
