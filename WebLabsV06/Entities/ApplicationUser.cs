using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLabsV06.DAL.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public byte[] AvatarImage { get; set; }
    }

    public class UserRole : IdentityRole<int>
    {

    }
}
