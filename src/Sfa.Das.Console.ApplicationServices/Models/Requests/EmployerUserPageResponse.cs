using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DAS.Support.Shared;

namespace Sfa.Das.Console.ApplicationServices.Models.Requests
{
    public class EmployerUserPageResponse
    {
        public ResultPage<SearchItem> Page { get; set; }
    }
}
