using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Interfaces.Services;
using Rk.Messages.Logic.ShoppingCartNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ShoppingCartNS.Queries.GetCartItems
{
    public class GetCartItemsQueryHandler : IRequestHandler<GetCartItemsQuery, IReadOnlyCollection<ShoppingCartItemDto>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
                

        public GetCartItemsQueryHandler(IAppDbContext appDbContext, IUserService userService, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<ShoppingCartItemDto>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
        {
            if (!_userService.IsAuthenticated) throw new RkErrorException("Пользователь не авторизован");

            var userName = _userService.UserName;

           var cartItems = await _appDbContext.ShoppingCartItems
                 .Include(product => product.Product)
                        .ThenInclude(product => product.Organization)
                .Include(self=>self.Product)
                    .ThenInclude(product => product.ProductDocuments)
                        .ThenInclude(pd => pd.Document)
                .Where(self => self.UserName == userName)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = _mapper.Map<IReadOnlyCollection<ShoppingCartItemDto>>(cartItems);

            return result;
        }
    }
}
