using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace bMovieTracker.Identity
{
    public class JwtAuthOptions
    {
        public const string ISSUER = "bMovieTracker";
        public const string AUDIENCE = "kinoMan";
        const string KEY = "secret0takoy0secret";
        public const int LIFETIME = 5; // minutes

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
