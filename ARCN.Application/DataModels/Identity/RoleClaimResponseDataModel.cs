﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Application.DataModels.Identity
{
    public class RoleClaimResponseDataModel
    {
        public RoleDataModel Role { get; set; }
        public List<RoleClaimDataModel> RoleClaims { get; set; }
    }
}