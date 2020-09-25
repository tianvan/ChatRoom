namespace ChatRoom.Server.Services
{
    public class NicknameGeneratorOptions
    {
        public const string Position = "NicknameGenerator";

        public string[] Adjectives { get; set; }

        public string[] Nouns { get; set; }
    }
}
