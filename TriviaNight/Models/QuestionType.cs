using System;
using System.Collections.Generic;

namespace TriviaNight.Models
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            Question = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public bool HasTextField { get; set; }

        public virtual ICollection<Question> Question { get; set; }
    }
}
