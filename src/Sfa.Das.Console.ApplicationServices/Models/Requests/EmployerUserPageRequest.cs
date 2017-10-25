using MediatR;

namespace Sfa.Das.Console.ApplicationServices.Models.Requests
{
    public class EmployerUserPageRequest : IAsyncRequest<EmployerUserPageResponse>, IAsyncRequest
    {
        public int Limit { get; set; }
        public int Start { get; set; }
    }
}