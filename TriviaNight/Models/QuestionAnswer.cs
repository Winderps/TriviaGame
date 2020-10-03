using System;
using System.Collections.Generic;

namespace TriviaNight.Models
{
    public partial class QuestionAnswer
    {
        public int Id { get; set; }
        public string DisplayText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
