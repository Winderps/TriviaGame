using System;
using System.Collections.Generic;

namespace TriviaNight.Models
{
    public partial class Question
    {
        public Question()
        {
            GameQuestion = new HashSet<GameQuestion>();
        }

        public int Id { get; set; }
        public string Prompt { get; set; }
        public int QuestionType { get; set; }
        public byte TimeLimit { get; set; }
        public int MaxPointValue { get; set; }
        public string Answer { get; set; }

        public virtual QuestionType QuestionTypeNavigation { get; set; }
        public virtual ICollection<GameQuestion> GameQuestion { get; set; }
    }
}
