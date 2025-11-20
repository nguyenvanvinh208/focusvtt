using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an.Models
{
    public class User
    {
        public string Uid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
        public bool IsOnline { get; set; }
    }
}