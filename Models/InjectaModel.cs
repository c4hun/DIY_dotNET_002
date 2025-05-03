namespace JeuInjecta.Models
{
    public class InjectaModel
    {
        // Type of the properties boolean for showing the status of the currency piece
        public bool EstRecto { get; set; }

        // Point
        public int Points { get; set; }

        // Constructor
        public InjectaModel()
        {
            // Initialisation of the boolean condition
            EstRecto = new Random().Next(2) == 0; // Randomly set to true or false
            Points = 0; // Initialize points to 0
        }

        // Method to incretement the points
        public void AjouterPoints(int points)
        {
            Points += points;
        }
    }
}
