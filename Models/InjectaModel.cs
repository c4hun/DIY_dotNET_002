namespace JeuInjecta.Models
{
    public class InjectaModel
    {
        private static readonly string[] Options = { "recto", "verso" };
        private static readonly Random rand = new Random();

        // Propriété tirée aléatoirement
        public string? SystemeChoix { get; private set; }

        // Propriété utilisateur (liée au formulaire)
        public string? UserChoix { get; set; }

        // Résultat du tirage
        public bool IsWin => UserChoix == SystemeChoix;

        // Propriétés de score (tu peux les gérer via la session ensuite)
        public int Points { get; set; }
        public int Erreurs { get; set; }

        // Méthode: tirage au hasard
        public void SimulerTirage()
        {
            SystemeChoix = Options[rand.Next(Options.Length)];
        }

        // Propriété utile si tu veux booléifier la logique
        public bool EstRecto => SystemeChoix == "recto";
    }
}

