using System.Text.Json.Serialization;

namespace RealTimeTasks.Data
{
    public class TaskItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int? UserID { get; set; }
        [JsonIgnore]
        public User User { get; set; } 
        public bool IsCompleted { get; set; }
    }
}
