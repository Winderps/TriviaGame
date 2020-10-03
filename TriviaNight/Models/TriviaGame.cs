using System;
using System.Collections.Generic;

namespace TriviaNight.Models
{
    public partial class TriviaGame
    {
        public TriviaGame()
        {
            GameInvite = new HashSet<GameInvite>();
            GamePlayer = new HashSet<GamePlayer>();
            GameQuestion = new HashSet<GameQuestion>();
        }

        public int Id { get; set; }
        public int Host { get; set; }
        public bool IsPrivate { get; set; }
        public bool HasStarted { get; set; }
        public int CurrentQuestion { get; set; }
        public string Name { get; set; }
        public byte MaxPlayers { get; set; }

        public virtual User HostNavigation { get; set; }
        public virtual ICollection<GameInvite> GameInvite { get; set; }
        public virtual ICollection<GamePlayer> GamePlayer { get; set; }
        public virtual ICollection<GameQuestion> GameQuestion { get; set; }
    }
}
