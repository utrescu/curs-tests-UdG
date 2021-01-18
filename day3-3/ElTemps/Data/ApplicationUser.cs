using Microsoft.AspNetCore.Identity;

namespace ElTemps.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Nom { get; set; }
        public string Cognoms { get; set; }
        public string Poblacio { get; set; }

    }
}