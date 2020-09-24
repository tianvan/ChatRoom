
using Microsoft.Extensions.Configuration;

namespace ChatRoom.Client.Services
{
    public class AvatarGenerator : IAvatarGenerator
    {
        private readonly IConfiguration _configuration;

        public AvatarGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generate(string id) => $"{_configuration["AvatarAPIAddress"]}{id}.svg";
    }
}
