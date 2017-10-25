using System.Collections.Generic;
using Sfa.Das.Console.Core.Domain.Model;

namespace Sfa.Das.Console.ApplicationServices.Models
{
    public class EmployerUserSearchResults
    {
        public int Page { get; set; }

        public int LastPage { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<EmployerUserSummary> Results { get; set; }
    }
}