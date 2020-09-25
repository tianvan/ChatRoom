using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using ChatRoom.Server.Models;

using Microsoft.Extensions.Options;

namespace ChatRoom.Server.Services
{
    public class NicknameGenerator : INicknameGenerator
    {
        private readonly IOptions<NicknameGeneratorOptions> _optionsAccessor;

        private readonly Dictionary<string, Nickname> _beUsing = new Dictionary<string, Nickname>();

        public NicknameGenerator(IOptions<NicknameGeneratorOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Nickname Generate(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace", nameof(id));
            }

            if (_beUsing.ContainsKey(id))
            {
                return _beUsing[id];
            }

            Nickname nickName = GenerateInternal();
            _beUsing.Add(id, nickName);

            return nickName;
        }

        private Nickname GenerateInternal()
        {
            var adjective = _optionsAccessor.Value.Adjectives[new Random().Next(_optionsAccessor.Value.Adjectives.Length - 1)];
            var noun = _optionsAccessor.Value.Nouns[new Random().Next(_optionsAccessor.Value.Nouns.Length - 1)];

            return new Nickname(adjective, noun);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Remove(string id) => _beUsing.Remove(id);
    }
}
