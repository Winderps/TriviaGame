using System;
using System.Collections.Generic;

namespace TriviaNight.Models
{
    public partial class User
    {
        public User()
        {
            ChatMessageUserFromNavigation = new HashSet<ChatMessage>();
            ChatMessageUserToNavigation = new HashSet<ChatMessage>();
            GameInvite = new HashSet<GameInvite>();
            TriviaGame = new HashSet<TriviaGame>();
            UserFriendFirstUserNavigation = new HashSet<UserFriend>();
            UserFriendSecondUserNavigation = new HashSet<UserFriend>();
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpires { get; set; }
        public string LastIp { get; set; }
        public bool Banned { get; set; }
        public string AvatarImage { get; set; }
        public int CorrectAnswers { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLogon { get; set; }
        public DateTime Birthday { get; set; }

        public virtual ICollection<ChatMessage> ChatMessageUserFromNavigation { get; set; }
        public virtual ICollection<ChatMessage> ChatMessageUserToNavigation { get; set; }
        public virtual ICollection<GameInvite> GameInvite { get; set; }
        public virtual ICollection<TriviaGame> TriviaGame { get; set; }
        public virtual ICollection<UserFriend> UserFriendFirstUserNavigation { get; set; }
        public virtual ICollection<UserFriend> UserFriendSecondUserNavigation { get; set; }
    }
}
