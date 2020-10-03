using System;
using System.Collections.Generic;

namespace TriviaNight.Models
{
    public partial class GameQuestion
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int QuestionId { get; set; }
        public byte QuestionNumber { get; set; }

        public virtual TriviaGame Game { get; set; }
        public virtual Question Question { get; set; }
    }
}
