using System;
using System.Collections.Generic;

namespace TriviaNight.Models
{
    public partial class UserFriend
    {
        public int Id { get; set; }
        public int FirstUser { get; set; }
        public int SecondUser { get; set; }

        public virtual User FirstUserNavigation { get; set; }
        public virtual User SecondUserNavigation { get; set; }
    }
}
