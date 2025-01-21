using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

namespace FastestQuiz.Models
{
    public class FirebaseService
    {
        private readonly FirestoreDb firestoreDb;

        public FirebaseService(IConfiguration configuration)
        {
            var projectId = configuration["Firebase:ProjectId"];
            if (string.IsNullOrEmpty(projectId))
            {
                throw new Exception("ProjectId não está configurado no appsettings.json.");
            }

            // Caminho para o arquivo JSON de credenciais
            string credentialsPath = "firestore-credential.json"; // Caminho do seu arquivo JSON

            // Carregar as credenciais do arquivo
            var credentials = GoogleCredential.FromFile(credentialsPath);

            // Criar o FirestoreClient com as credenciais usando FirestoreClientBuilder
            var firestoreClient = new FirestoreClientBuilder
            {
                CredentialsPath = credentialsPath
            }.Build();

            // Agora o FirestoreDb é inicializado com o FirestoreClient
            firestoreDb = FirestoreDb.Create(projectId, firestoreClient);
        }


        // Responsável por receber a coleção e transforma em um objeto
        public async Task<List<Quiz>> ListQuizzesAsync()
        {
            var quizzesCollection = firestoreDb.Collection("quizzes");
            var snapshot = await quizzesCollection.GetSnapshotAsync();

            var quizzes = new List<Quiz>();

            if (snapshot.Count == 0)
            {
                return quizzes;// Retorna uma lista vazia se não houver quizzes
            }
            else
            {
                foreach (var document in snapshot.Documents)
                {
                    // Mapeia os documentos para objetos Quiz
                    var quiz = new Quiz
                    {
                        Name = document.GetValue<string>("name") ?? "Sem nome",
                        Author = document.GetValue<string>("author") ?? "Sem autor",
                        Date = document.GetValue<DateTime>("date"),
                        LastUpdate = document.GetValue<DateTime>("lastUpdate")
                    };
                    quizzes.Add(quiz); // adiciona o quiz na lista
                }
            }
            return quizzes; // Retorna a lista de quizzes
        }

        public async Task AddQuizzesAsync(IFormCollection formCollection)
        {
            var quizzesCollection = firestoreDb.Collection("quizzes");

            // Capture field values
            var name = formCollection["name"].FirstOrDefault();
            var description = formCollection["description"].FirstOrDefault();
            var question1 = formCollection["question1"].FirstOrDefault();
            var question2 = formCollection["question2"].FirstOrDefault();
            var question3 = formCollection["question3"].FirstOrDefault();
            var question4 = formCollection["question4"].FirstOrDefault();
            var author = "user";
            var date = Timestamp.FromDateTime(DateTime.UtcNow); // Converte para Timestamp
            var lastUpdate = date;

            // Create dictionary with the data to send to firestore
            var newUser = new Dictionary<string, object>
            {
                { "name", name },
                { "description", description},
                { "question1", question1 },
                { "question2", question2 },
                { "question3", question3 },
                { "question4", question4 },
                { "author", author },
                { "date", date },
                { "lastUpdate", lastUpdate}
            };

            // Add the new user to firestore
            await quizzesCollection.AddAsync(newUser);
        }
    }
}