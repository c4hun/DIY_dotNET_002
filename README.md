# Ce que j'ai prises:
## - Travaille sur un système de points et logique de score
## - Utilise LINQ
## - Gestion d’interactions avec des message boxes et des boutons

---
---

# What I have taken:
## - Working on a points system and scoring logic
## - Uses LINQ
## - Handling interactions with message boxes and buttons

# Classes
```mermaid
classDiagram
class InjectaModel {
    +string UserChoix
    +string SystemeChoix
    +int Points
    +int Erreurs
    +bool IsWin
    +void SimulerTirage()
}
class HomeController {
    +IActionResult Index()
    +IActionResult Parier(string choix)
    +IActionResult Rejouer()
}
class IndexView {
    +Display(UserChoix)
    +Display(SystemeChoix)
    +Form Parier()
}

%% Add links for grouping MVC layers visually
InjectaModel <|-- HomeController : uses
HomeController <|-- IndexView : updates
```

# Flowchart
```mermaid
flowchart TD
    A["*User opens game*"] --> B["'Index()' in 'HomeController'"]
    B --> C["'InjectaModel' instance created"]
    C --> D["'IndexView' displayed with empty fields"]
    
    E["*User submits bet*"] --> F["'Parier'(string choix) in 'HomeController'"]
    F --> G["'InjectaModel.UserChoix' set"]
    G --> H["'InjectaModel.SimulerTirage()' called"]
    H --> I["'InjectaModel.IsWin' computed"]
    
    I --> J["Controller updates 'Points'/'Erreurs' in session"]
    J --> K["Pass data to 'IndexView'"]
    K --> L["View displays 'UserChoix', 'SystemeChoix', 'IsWin', 'Points', 'Erreurs'"]
    
    M["*User clicks 'Rejouer'*"] --> N["'Rejouer()' in 'HomeController'"]
    N --> O["Session cleared, points/errors reset"]
    O --> P["Redirect to 'IndexView'"]

%% Add styles to increase node width visually
style A font-size:17pt
style B font-size:12pt
style C font-size:13pt
style D font-size:12pt
style E font-size:17pt
style F font-size:12pt
style G font-size:12pt
style H font-size:12pt
style I font-size:12pt
style J font-size:12pt
style K font-size:12pt
style L font-size:13pt
style M font-size:17pt
style N font-size:13pt
style O font-size:13pt
style P font-size:12pt
```
