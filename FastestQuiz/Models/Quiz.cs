namespace FastestQuiz.Models
{
    public class Quiz
    {
        public string Name { get; set; } // Nome do quiz
        public string Author { get; set; } // Autor do quiz
        public string Description { get; set; }

        public string Question1 { get; set; }
        public string Question2 { get; set; }
        public string Question3 { get; set; }
        public string Question4 { get; set; }

        public DateTime Date { get; set; } // Data de criação
        public DateTime LastUpdate { get; set; } // Data da última atualização
    }
}
