﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Domain.Identity
{
    public class AppRole: IdentityRole<string>
    {
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
