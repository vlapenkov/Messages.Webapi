using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.ProductsNS.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Queries.GetProductsQuery
{
    /// <summary>
    /// Получить список продукции
    /// </summary>
    public class GetProductAttributesQueryHandler : IRequestHandler<GetProductAttributesQuery, IReadOnlyCollection<AttributeDto>>
    {
        private readonly IAppDbContext _appDbContext;

        private readonly IMapper _mapper;

        public GetProductAttributesQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<AttributeDto>> Handle(GetProductAttributesQuery request, CancellationToken cancellationToken)
        {

            var result = await _appDbContext.Attributes
                .OrderBy(attr=> attr.Id)
                .ProjectTo<AttributeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return result;

        }
    }
}
