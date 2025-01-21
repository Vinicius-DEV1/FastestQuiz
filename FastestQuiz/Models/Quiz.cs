namespace FastestQuiz.Models
{
    public class Quiz
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; } 
        public string Description { get; set; }

        public string Question1 { get; set; }
        public string Question2 { get; set; }
        public string Question3 { get; set; }
        public string Question4 { get; set; }

        public DateTime Date { get; set; } // Data de criação
        public DateTime LastUpdate { get; set; }
    }
}
