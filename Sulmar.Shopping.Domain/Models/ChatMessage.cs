using System;
using System.Collections.Generic;
using System.Text;

namespace Sulmar.Shopping.Domain.Models
{
    public class ChatMessage
    {
        public string Content { get; set; }
        public bool IsHighPriority { get; set; }
    }
}
