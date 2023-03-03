namespace Web.Models
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using System.Text;

    public class SigningSettings
    {
        public SigningCredentials SigningCredentials { get; }

        public SigningSettings(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var secret = configuration[$"{nameof(SigningSettings)}:Secret"] ?? string.Empty;

            SymmetricSecurityKey? symmetricKey = new(Encoding.UTF8.GetBytes(secret));
            SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
