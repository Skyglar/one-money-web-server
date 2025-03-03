using Microsoft.AspNetCore.Identity;
using System;

namespace domain.Entities {
    public class User : IdentityUser {
        public Guid NetId { get; set; }
    }
}
