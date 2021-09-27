# Prototype Hero: A Game

## What is it?

This is a Unity project that creates a simple adventure game where a Prototype Hero must brave the wilds outside their home town in order to face a great evil lurking there. The resolve of the hero and the strength of their steel will be tested.

## MVP
- Player can walk around town & buy items from a shopkeeper
-surrounding battling enemies & collecting items that can be traded in the town. In the third scene there is a final battle with a boss.
- 2D Game(side view)
- 3 Scenes
- medieval theme

## Scenes

### Scene #1: Title Screen
Has a pleasing aesthetic and allow the player to begin the game, adjust game settings, exit the game, or view the credits.
Particle effects and some pretty incredible fonts have been used to create a better ambience for the player.

### Scene #2: Town
UI elements to show the inventory of the hero displayed in the top left corner of the screen.
A shopkeeper with interactive dialogue boxes which will encourage the player
Shop in which the hero can purchase potions, weapon power ups, and key items to help you progress through the game.
Progression from this scene into the Wilds outside of the Town scene.
Dynamic backgrounds that add to the aesthetic of the game to improve.
Grid and Sprite backgrounds combined to create a fully realized world

### Scene 3#: Wilds
Parallax scrolling used to add a depth of field to the background image.
Enemies added which can challenge the hero with varrying attack timings and ranges.
The ability for the hero to quickly dodge away from attacks and parry them if timed correctly to receive no damage.
The player will have a final battle with a boss. It will be dangerous and high potential for death
The boss battle will have a second stage with different attack patterns to add diversity to the fight.

## Game Play Controls

High Attacks W
Run Left A
Crouch S
Run Right D
Jump Spacebar
Interact Q
Attack Mouse Button 1
Parry Mouse Button 2
Drink Potion E


## Further Improvements

### Pets
- Having tha bility to purchase a pet with a custom sprite set and animator that would follow the hero around. The idea would be to give it the same path finding ability to the pet that the bandits have, and implement a very small hit box centered on the pet so it could melee attack any enemies that is approaches.

### Weapon and Armor Upgrades
- Have the ability to buy new weapons in the shop which can scale the base player damage. We would also like to add in additional particle effects and maybe change the sprite of the PrototypeHero
-Have the ability to purchase armor which would decrease the amount of damage dealt to the PrototypeHero on each hit.
-Different types of weapons to be purchased at the shop. They would need to have sprites created for them and be integrated in the animator.

### Smoother Animation
- Clean up the transition between different animation states for the Bandits, Necromancer, and PrototypeHero. Things like having better logic to decide if an enemy gets hit while starting an attack animation how to decide which takes precedence.

#Technologies

##Unity

##C#

##GitHub

# Changelog

## 9/13/2021 Moday
- Asset Gathering
- Game shaping

## 9/14/2021 Tuesday
- Placing bandits in scene and having them detect the player

## 9/15/2021 Wednesday
- Bandit animations smoothed out to allow better transitions
- Detection radius and further logic added to Bandit behaviors

## 9/16/2021 Thursday
- Two stage boss battle added
- Health bars integrated

## Group Members

Charles Bofferding

Qaalid Hashi

Joel Connell

Miriam Silva

Jona Brown

Ben Arno

Benjamin Ibarra

Joshua Haddock

Steven Boston

