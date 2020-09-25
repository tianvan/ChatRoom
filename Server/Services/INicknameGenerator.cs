using ChatRoom.Server.Models;

namespace ChatRoom.Server.Services
{
    public interface INicknameGenerator
    {
        Nickname Generate(string id);

        void Remove(string id);
    }
}
