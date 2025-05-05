using JeuInjecta.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;

namespace JeuInjecta.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new InjectaModel());
        }

        // Action POST pour traiter le pari de l'utilisateur
        [HttpPost]
        public IActionResult Parier(string choix)
        {
            if (string.IsNullOrEmpty(choix) || (choix != "recto" && choix != "verso"))
            {
                TempData["Avertissement"] = "Veuillez sélectionner un choix valide (Recto ou Verso).";
                return RedirectToAction("Index");
            }

            #region EstRecto
            // Création d'une instance de modèle
            var injecta = new InjectaModel();
            injecta.UserChoix = choix;
            injecta.SimulerTirage();

            bool win = injecta.IsWin;

            // Récupérer les points et erreurs depuis la session
            int points = HttpContext.Session.GetInt32("Points") ?? 0;
            int erreurs = HttpContext.Session.GetInt32("Erreurs") ?? 0;

            // LINQ: Traitement points/erreurs compact
            (points, erreurs) = win
                ? (points + 1, erreurs)
                : (points, erreurs + 1);
            #endregion EstRecto

            #region Inuallable
            // Si aucune option n'est sélectionnée
            if (string.IsNullOrEmpty(choix) || (choix != "recto" && choix != "verso"))
            {
                TempData["Avertissement"] = "Veuillez sélectionner un choix valide (Recto ou Verso).";
                return RedirectToAction("Index");
            }


            #endregion Inuallable

            #region Messageries
            // Messages logiques
            if (points >= 2)
            {
                TempData["Message"] = "Félicitations ! Vous avez atteint 2 points !";
                points = erreurs = 0;
            }
            else if (erreurs == 2)
            {
                TempData["Avertissement"] = "Attention ! Encore une erreur et vous perdrez !";
            }
            else if (erreurs >= 3)
            {
                TempData["Message"] = "Vous avez perdu !";
                points = erreurs = 0;
            }

            // Sauvegarder dans la session
            HttpContext.Session.SetInt32("Points", points);
            HttpContext.Session.SetInt32("Erreurs", erreurs);

            // Utilisation de ViewData comme dictionnaire
            ViewData["UserChoix"] = injecta.UserChoix;
            ViewData["SystemeChoix"] = injecta.SystemeChoix;
            ViewData["IsWin"] = injecta.IsWin;
            ViewData["Points"] = HttpContext.Session.GetInt32("Points") ?? 0;
            ViewData["Erreurs"] = HttpContext.Session.GetInt32("Erreurs") ?? 0;
            #endregion Messages

            #region Rediriger
            // Rediriger vers l'action Index pour afficher les résultats
            return View("Index", injecta);  // Rediriger vers Index après le traitement
            #endregion
        }

        [HttpPost]
        public IActionResult Rejouer()
        {
            #region Return
            // Supprimer toutes les données de session
            HttpContext.Session.Clear();

            // Ajouter un message temporaire de confirmation
            TempData["Message"] = "Le jeu a été réinitialisé.";

            // Rediriger vers la vue Index
            return RedirectToAction("Index");
            #endregion
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
