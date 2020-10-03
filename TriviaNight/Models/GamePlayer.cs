using System;
using System.Collections.Generic;

namespace TriviaNight.Models
{
    public partial class GamePlayer
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int Points { get; set; }
        public int CorrectAnswers { get; set; }

        public virtual TriviaGame Game { get; set; }
    }
}
