using System;
using System.Collections.Generic;

namespace TriviaNight.Models
{
    public partial class ChatMessage
    {
        public int Id { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public string Contents { get; set; }

        public virtual User UserFromNavigation { get; set; }
        public virtual User UserToNavigation { get; set; }
    }
}
