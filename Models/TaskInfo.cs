using System;

namespace Do_an.Models
{
    public class TaskInfo
    {
        
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsDone { get; set; }
    }
}
