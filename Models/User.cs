using System;

namespace Do_an.Models
{
    public class User
    {
        public string Uid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public bool IsOnline { get; set; }


        public string LastActive { get; set; }

        public string AvatarBase64 { get; set; }
        public string LocalIP { get; set; }
        public UserProfile Info { get; set; } = new UserProfile();
    }

    public class UserProfile
    {
        public int Level { get; set; } = 1;
        public int XP { get; set; } = 0;
        public int XPToNextLevel { get; set; } = 100;
        public string Bio { get; set; } = "Nhập mục tiêu...";


        public int CurrentStreak { get; set; } = 0;
        public int BestStreak { get; set; } = 0;


        public string LastActive { get; set; }

        public double TotalHours { get; set; } = 0;
        public double AvgDailyHours { get; set; } = 0;
        public int WeeklyRank { get; set; } = 0;
        public int HighestRank { get; set; } = 0;
        public int TotalTopReach { get; set; } = 0;
        public int TotalCoins { get; set; } = 0;
    }
}