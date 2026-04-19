using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QudraSaaS.Application.Interface
{
    public interface IAi
    {
        Task<string> AskAi(string userPrompt);
    }
}
