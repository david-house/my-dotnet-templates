using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTemplate.DbContexts.Models
{
    public class DemoMessage
    {
        public int DemoMessageId { get; set; }
        public string Message { get; set; } = null!;
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
