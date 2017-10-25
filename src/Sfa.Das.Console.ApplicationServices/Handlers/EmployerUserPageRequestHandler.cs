using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Sfa.Das.Console.ApplicationServices.Models.Requests;

namespace Sfa.Das.Console.ApplicationServices.Handlers
{
    public class EmployerUserPageRequestHandler : IAsyncRequestHandler<EmployerUserPageRequest, EmployerUserPageResponse>
    {
        private readonly IEmployerUserRepository _repository;
        public EmployerUserPageRequestHandler(IEmployerUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<EmployerUserPageResponse> Handle(EmployerUserPageRequest message)
        {
            return new EmployerUserPageResponse
            {
                Page = await _repository.FindPage(message.Limit, message.Start)
            };
        }
    }
}
