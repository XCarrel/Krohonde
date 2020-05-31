
# Krohonde V4

## Introduction
La fourmilière le lieu dans lequel se trouvent la nourriture, les larves en gestation et la Reine.
La colonie de fourmi est constituée d'une reine et de quatre classes de fourmis:

* La reine. C'est à travers elle uniquement que l'on peut interagir avec le jardin. Elle peut:
  - Se déplacer
  - Pondre une larve
  - Déposer une phéromone indiquant un endroit où elle désire que soit construit une case case d'extension de la fourmilière.
* Des scouts qui repèrent la nourriture et les matériaux de construction à l'extérieur de la fourmilière, ainsi que les menaces (l'araignée). Elles se contentent de déposer des indicateurs (phéromones) à l'intention des autres.
* Des fermières qui vont chercher la nourriture dans le jardin et la ramène à la fourmilière.
* Des ouvrières qui utilisent les matériaux de construction (trouvés dans le jardin) pour étendre la fourmilière.
* Des soldats qui combattent les dangers. Ils sont soit dans une posture de défense de la fourmilière, soit dans une posture d'attaque à aller chercher les ennemis à l'extérieur. 

L'ensemble est sous le contrôle de Mère Nature, qui est responsable de faire vivre et mourir les créatures. Au fil du temps, elle va:

- Faire croître les larves de la fourmilière ainsi que la nourriture et les matériaux présents dans le jardin
- Fatiguer et faire vieillir chaque créature
- Eliminer les cadavres de créatures mortes
- Atténuer les phéromones

## Spécifications
Cette section regroupe des spécifications plus détaillées concernant les diverses classes et types de fourmis.
### Cycle de Vie
Un cycle de vie représente une seconde dans la vie du jardin.
Durant un cycle de vie, chaque créature (fourmi, reine ou araignée) peut récolter autant d'information qu'elle veut/peut sur l'environnement et effectuer une et une seule action.
### Déplacements
Toutes les fourmis ne sont pas égales dans leur capacité de déplacement. Les fermières et les ouvrières qui peuvent porter de la nourriture ou des matériaux sont moins agiles que les scouts et les soldats.
Cela se traduit ainsi:
Les scouts et les soldats peuvent se déplacer dans n'importe laquelle des huit cases adjacentes
Les fermières et les ouvrières ne peuvent se déplacer que horizontalement ou verticalement
### La Reine
La Reine est le seul moyen qu'a l'utilisateur d'influencer la vie de la fourmilière.
C'est l'utilisateur (et non le code de la Reine) qui décide de l'endroit où un morceau de fourmilière doit tre construit.
De la mme manière, c'est l'utilisateur qui décide du type de larve que la Reine va pondre.
Pondre une larve coéte de l'énergie. La Reine puise dans les réserves de nourriture de la fourmilière pour le faire (mais cela ne compte pas comme une action "Manger").
La Reine se situe toujours dans la fourmilière. Elle peut se déplacer (pour poser une phéromone par exemple), mais toujours à l'intérieur de la fourmilière.
La mort de la Reine entraîne l'arrt immédiat de la "partie".
### Croissance des ressources
Le jardin contient certains éléments qui sont considérés comme des ressources. Chacun de ces éléments évolue avec le temps grâce à mère nature:

- Les larves. Stockées dans la fourmilière. Lorsqu'une larve est pondue, elle est de niveau 1; tout les "RythmeDeCroissance" cycles (voir lois de la nature), une larve gagne un niveau. Une larve niveau 8 éclot: elle devient une fourmi qui part vivre sa vie.
- La nourriture. Une case du jardin peut contenir une quantité de nourriture variant du niveau 1 (le moins) au niveau 8 (le plus). De type végétal, la nourriture "pousse".
- Les matériaux. Similaire à la nourriture.
- Les phéromones. Lorsqu'elle est posée, une phéromone a une intensité (un niveau) de 8. Tout les "DuréePhéromone" cycles, elle s'atténue de un niveau et finit par disparaître.

### Lois de la Nature
Les lois de la nature dictent diverses caractéristiques du monde et de ses créatures. Ces lois sont stockées dans le jardin. Chaque (type de) fourmi peut interroger le jardin au sujet des lois qui le concerne.
Liste des lois de la nature:

 | Nom de la loi | Valeur | Description |
 |---|---|---|
 | ChampDeVisionScout | 3 | La distance en nombre de cases jusqu'où un Scout peut voir
 | ChampDeVisionSoldat | 2 | La distance en nombre de cases jusqu'où un Soldat peut voir
 | ChampDeVisionFermière | 1 | La distance en nombre de cases jusqu'où un Fermière peut voir
 | ChampDeVisionOuvrière | 1 | La distance en nombre de cases jusqu'où un Ouvrière peut voir
 | DuréePhéromone | 15 | Durée en nombre de cycles de vie d'un niveau de phéromone
 | EnergiePhéromone | 20 | L'énergie (en nombre de points de vie) que coéte la pose d'une phéromone
 | MaxPointsVieFourmi | 500 | Le nombre maximum de points de vie d'une fourmi à la naissance.
 | MaxPointsVieAraignée | 1500 | Le nombre maximum de points de vie de l'araignée à sa naissance.
 | DégâtsInfligés | 8 | Donne le nombre de points de dégâts maximum qu'une créature peut infliger lors d'un combat. Cette valeur est exprimée en pourcentage de ses propres points de vie. Exemple : si DégâtsInfligés = 10, une fourmi qui a 74 points de vie peut infliger au maximum 7 points de dégâts et une autre qui a 25 points de vie peut en infliger 3
 | DégâtsInfligésSoldat | 16 | Comme ci-dessus, mais pour les soldats (qui sont plus puissants)
 | DégâtsInfligésAraignée | 4 | Pareil pour l'araignée
 | PointsDeFatigue | 1 | Le nombre de points que chaque fourmi perd à chaque cycle de vie
 | SensibilitéOdorat | 8 | Le rapport entre la distance max fourmi-phéromone (détectée) et le niveau de la phéromone. Une phéromone de niveau 5 peut tre détectée par une fourmi qui est à une distance de 40 (=8x5), mais pas plus
 | PointsDePonte | 10 | Le nombre de points de nourriture que la Reine doit prélever de la fourmilière pour pondre une larve. Il s'agit également du nombre de points que coéte le passage d'une larve d'un niveau à l'autre.
 | Récupération | 5 | Nombre de points de vie qu'une fourmi récupère quand elle mange (à la fourmilière ou dans la nature)
 | PtsDeVieParNiveau | 5 | Nombre de points de vie que contient un niveau d'intensité de nourriture
 | TailleSac | 50 | Quantité de nourriture/matériau que peut transporter une fermière/ouvrière
 | RisqueCatastrophe | 2 | Risque en pour mille qu'une catastrophe naturelle ait lieur
 | RythmeDeCroissance | 5 | Les larves, la nourriture et les matériaux croissent tous les "RythmeDeCroissance" cycles de vie

### Phéromones
Les fourmis peuvent déposer des traces odorantes (phéromones) détectables par les autres fourmis.
Il y a trois types de phéromones:

 | Type | Posé par | Utilité
 |---|---|---|
 | PhéroOuvrière | Reine | Marque l'endroit où la Reine aimerait que soit construit un bout de fourmilière
 | PhéroScout | Scouts | Indique la proximité de matériau de construction
 | PhéroSoldat | Scouts | Indique la proximité d'un ennemi
 | PhéroFermière | Scouts | Indique la proximité de nourriture

Les phéromones ont 8 niveaux d'intensité. Lorsqu'une phéromone est posée elle est de niveau 8 et faiblit petit à petit jusqu'à arriver au niveau 1 et ensuite disparaître.
Chaque fourmi peut "sentir" les phéromones. Plus elles sont puissantes (fraîches), plus elles sont détectées de loin.
La distance jusqu'à laquelle une fourmi peut sentir une phéromone dépend de la sensibilité de l'odorat (qui est la mme pour tous le types de fourmi). Voir lois de la nature.
### Fatigue, Récupération et Vieillissement
Une fourmi qui est hors de la fourmilière fatigue: à chaque cycle de vie, elle perd `PointsDeFatigue` (voir lois de la nature) points de vie. Et bien sér: elle meurt si ses points de vie arrivent à 0.
Une fourmi récupère des points de vie en mangeant de la nourriture qu'elle trouve soit dans la fourmilière (on dira qu'elle "Déjeune") soit dans la nature (elle "Pique-nique"). A chaque déjeuner ou pique-nique, elle gagne `PointsDeRécupération` points de vie (voir lois de la nature) . Le stock de nourriture est diminué d'autant de points. En d'autre termes, la fourmi transforme la nourriture en points de vie pour elle.
La fatigue s'applique également à l'araignée. Cette dernière se ressource en mangeant le cadavre de ses victimes.
Avec le temps, les fourmis perdent petit à petit de leur capacité de récupération. Cela se traduit par le fait que le nombre maximum de points de vie de la fourmi diminue d'une unité tout les 5 cycles de vie.
Dès lors, le niveau de vie d'une fourmi peut se représenter comme suit:
### Combat
Si une créature détecte un ennemi dans les huit cases qui l'entourent, elle peut décider d'engager le combat ou de fuir. Dans le premier ca, elle inflige X points de dégât (=la créature ennemie perd X points de vie). Bien entendu le mme choix est offert à la créature ennemie; il faut donc s'attendre à ce que cette dernière riposte.
Le nombre X est calculé comme suit:
- On calcule M le maximum de points de dégât infligeable(voir paramètre `DégâtsInfligés` dans la section (voir lois de la nature)
- X est calculé aléatoirement et vaut entre 80% et 100% de M

### Réflexions en cours

Plus une fourmi va vite, plus son champ de vision se rétrécit.

vitesse max par type de fourmi.

Fermière/ouvrières accélèrent vite, scouts/soldats lentement. Pas d'arrêt brusque.

Mère Nature détient les attributs de chaque fourmi (pour éviter la triche)

La nourriture peut être transformée en:
- Energie (vie)
- Force (fermière transporte plus, ouvrière dépense moins d'énergie à la construction, soldat fait plus de dégâts)
- Résistance
