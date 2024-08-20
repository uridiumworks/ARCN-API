
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.Identity;
using AutoMapper;

namespace ARCN.Application.AutoMaps.Admin
{
    public class AdminEntityAutoMap:Profile
    {
        public AdminEntityAutoMap()
        {
            CreateMap<ApplicationRoleClaim, RoleClaimDataModel>().ReverseMap();
        }
    }
}
