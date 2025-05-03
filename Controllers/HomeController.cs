using JeuInjecta.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JeuInjecta.Controllers
{
    public class HomeController : Controller
    {
        // Action GET pour afficher la page de jeu
        public IActionResult Index()
        {
            // R�cup�rer les points et erreurs depuis la session
            ViewBag.Points = HttpContext.Session.GetInt32("Points") ?? 0;
            ViewBag.Erreurs = HttpContext.Session.GetInt32("Erreurs") ?? 0;

            return View(); // Retourner la vue Index
        }

        // Action POST pour traiter le pari de l'utilisateur
        [HttpPost]
        public IActionResult Parier(bool? pari)
        {
            // Si aucune option n'est s�lectionn�e
            if (!pari.HasValue)
            {
                TempData["Avertissement"] = "Veuillez s�lectionner un type de pari (Recto ou Verso).";
                return RedirectToAction("Index"); // Rediriger vers Index avec l'avertissement
            }

            // R�cup�rer les points et erreurs depuis la session
            int points = HttpContext.Session.GetInt32("Points") ?? 0;
            int erreurs = HttpContext.Session.GetInt32("Erreurs") ?? 0;

            // Simuler un tirage al�atoire pour Recto ou Verso
            bool estRecto = new InjectaModel().EstRecto;  // Simule le tirage de la pi�ce

            // Logique de tirage et v�rification du pari
            if (pari == estRecto)  // Si l'utilisateur a gagn�
            {
                points += 1;  // Ajouter 1 point � chaque victoire
                erreurs = 0;  // R�initialiser les erreurs � 0 apr�s une victoire
                TempData["Message"] = "Bravo ! Vous avez gagn� 1 point !";
            }
            else  // Si l'utilisateur a perdu
            {
                erreurs += 1;  // Ajouter 1 erreur � chaque d�faite
                TempData["Message"] = "Dommage ! Ressayez-le !";
            }

            // Si les points atteignent 10, on montre un message sp�cial
            if (points >= 2)
            {
                TempData["Message"] = "F�licitations ! Vous avez atteint 2 points !";
                points = 0;  // R�initialiser les points apr�s 3 erreurs
                erreurs = 0;  // R�initialiser les erreurs apr�s la perte
            }

            // V�rifier le nombre d'erreurs pour afficher un avertissement ou une perte
            if (erreurs == 2)
            {
                TempData["Avertissement"] = "Attention ! Vous avez 2 erreurs. Encore une erreur et vous perdrez d�finitivement !";
            }

            if (erreurs >= 3)
            {
                TempData["Message"] = "Vous avez perdu ! Vous avez atteint 3 erreurs.";
                points = 0;  // R�initialiser les points apr�s 3 erreurs
                erreurs = 0;  // R�initialiser les erreurs apr�s la perte
            }

            // Stocker les points et erreurs dans la session pour qu'ils soient persist�s entre les requ�tes
            HttpContext.Session.SetInt32("Points", points);
            HttpContext.Session.SetInt32("Erreurs", erreurs);

            // Rediriger vers l'action Index pour afficher les r�sultats
            return RedirectToAction("Index");  // Rediriger vers Index apr�s le traitement
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
