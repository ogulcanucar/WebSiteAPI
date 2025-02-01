using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteAPI.Application.DTOs.User
{
    public class UserDto
    {
        public string Id { get; set; } // Kullanıcı ID (GUID)
        public string UserName { get; set; } // Kullanıcı Adı
    }
}
