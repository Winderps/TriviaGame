using System;
using System.Collections.Generic;

namespace TriviaNight.Models
{
    public partial class GameInvite
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int TargetUser { get; set; }
        public byte Status { get; set; }

        public virtual TriviaGame Game { get; set; }
        public virtual User TargetUserNavigation { get; set; }
    }
}
