# UAA13
Projet UAA13 (6TTI) Année 2024-2025

# Notes d'usage

## Déplacement en interface

Pour la plupart des interfaces on peut soit interagir avec la souris, soit avec un clavier ou encore une manette

Flêches directionelles / D-pad / Stick gauche / Déplacement de la souris -> Déplacement du choix de sélection
Enter / Bouton A (position du bas à droite) / Bouton Trigger sur un joystick / Click gauche souris -> Confirmation de sélection (click)

Dans le menu de combat et l'inventaire, seul le contrôle souris est implementé.

Pour quitter une interface (sauf menu game over et menu principal)
    -> Escape / touche "E" / Shift (Droit) / Bouton B (position de droite dans les boutons de droite)

## Déplacement/contrôle du personnage
ZQSD / flêches directionelles / Stick gauche -> Déplacement du personnage
Shift gauche / Numpad 0 / LB -> Sprint, Courir

touche "E" / Shift (Droit) / Bouton B (position de droite dans les boutons de droite) -> Ouverture de l'inventaire
touche "=" / RB | touche "A" -> ouverture du livre (quand applicable)

Escape / Backspace / Bouton Start -> Ouverture du menu pause


### Interaction avec le monde

Contact avec des portes -> changement de zone
Contact avec des parchemins / autres items -> Ramassage d'items
Contact avec des ennemis -> Ouverture de l'interface de combat.

### Interface de combat

Appui sur des touches -> passage du tour,

Si la vie du personnage principal atteint zero, Game Over (et ouverture du menu adéquat)
Si la vie de l'adversaire atteint zero, fin du combat

### Autres notes

Aide pour certaines touches sur le côté gauche en bas de l'écran (en jeu)

Position sur la mini-carte sur le côté droit en haut de l'écran (aussi en jeu et seulement dans "l'overworld")

Quand l'indicateur de sauvegarde apparait dans le bas à droite de l'écran il n'est pas recommandé de fermer le jeu étant donné que ça peut bousiller la sauvegarde.

### Bugs connus

Double click dans l'inventaire peut causer de la duplication ou suppression d'items

Il est possible que les positions dans la sauvegarde soit en desync et crée un softlock dû à l'incapacité de la caméra à voir le joueur. (problème lié au changement de zone)