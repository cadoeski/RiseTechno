using AutoMapper;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Features.Users.Queries.GetContacsById
{
    public class ReadContacsReportHandler : IRequestHandler<ReadContacsReport, User>
    {
        private readonly IUsersReadRepository userReadRepository;
        private readonly IMapper mapper;

        public ReadContacsReportHandler(IUsersReadRepository userReadRepository, IMapper mapper)
        {
            this.userReadRepository = userReadRepository ?? throw new ArgumentNullException(nameof(userReadRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<User> Handle(ReadContacsReport request, CancellationToken cancellationToken)
        {
            var musteri = await userReadRepository.GetByIdAsync(request.Id);
           

            var musteriVm = mapper.Map<User>(musteri);

            return musteriVm;
        }
    }
}
