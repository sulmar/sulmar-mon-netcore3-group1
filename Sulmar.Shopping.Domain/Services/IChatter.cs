using Sulmar.Shopping.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sulmar.Shopping.Domain.Services
{
    public interface IChatter
    {
        Task HaveGotMessage(ChatMessage message);
    }
}
