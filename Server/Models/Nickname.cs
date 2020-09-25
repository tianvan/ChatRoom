using System;

namespace ChatRoom.Server.Models
{
    public class Nickname
    {
        public Nickname(string adjective, string noun)
        {
            if (string.IsNullOrWhiteSpace(noun))
            {
                throw new ArgumentException($"'{nameof(noun)}' cannot be null or whitespace", nameof(noun));
            }

            Adjective = adjective;
            Noun = noun;
        }

        public string Adjective { get; }

        public string Noun { get; }

        public override bool Equals(object obj) => obj is Nickname nickname && Adjective == nickname.Adjective && Noun == nickname.Noun;

        public override int GetHashCode() => HashCode.Combine(Adjective, Noun);

        public override string ToString() => $"{Adjective}的{Noun}";
    }
}
