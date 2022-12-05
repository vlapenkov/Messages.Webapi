using MediatR;
using Rk.Messages.Logic.NewsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.NewsNS.Queries.GetOneNews
{
    public class GetOneNewsQuery :IRequest<NewsResponse>
    {
        public long Id { get; set; }
    }
}
