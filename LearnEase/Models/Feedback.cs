namespace LearnEase.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int? Rating { get; set; }

        public int CourseId { get; set; }

        public DateTime CreationDate { get; set; }

        public long UserId { get; set; }

        public User? User { get; set; }
    }
}