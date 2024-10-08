﻿using AutoMapper;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using Rk.Messages.Logic.OrdersNS.Dto;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using Rk.Messages.Common.Helpers;

namespace Rk.Messages.Logic.OrdersNS.Mappings
{
    public class OrganizationsMappingProfile : Profile
    {
        public OrganizationsMappingProfile()
        {
            CreateMap(typeof(IPagedList<Organization>), typeof(PagedResponse<OrganizationDto>));

            CreateMap<Organization, OrganizationDto>()
                 .ForMember(dest => dest.StatusText, opt => opt.MapFrom(src => src.Status.GetDescription()))
               .ReverseMap();

        }
    }
}
