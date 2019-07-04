# One Piece Battle

## Programmers
Gianluca Gambari, Matteo Marre’ Brunenghi

## Concept
The game is a mobile 2D 1vs1 fighting game, based on characters of the famous Manga One Piece.
The player chooses one character and has to defeat all the others in a sequence of 1 vs 1 battles. Each character can move to the left and right, can jump and can use a standard move repeatedly to attack the enemy. Each time it hits successfully, an energy bar is filled a bit. When the bar is full, the corresponding character can use a special powerful move, that is different depending on the character.
The enemy tries randomly to hit the player and to dodge the hits, and can perform the special move in the same way.

## Rules
The player wins a stage when the enemy’s life reaches 0, instead he loses if the enemy defeat him or if the timer is finished.
The player wins the whole game if he manages to defeat all the enemies.
The battle is performed in a simple 2D arena, no character can exit the rectangular box.

## Requirements
* Some rectangular images for the background
* Some sprites for each character:
  * Motion
  * Standard move
  * Special move
* A virtual joystick
* Two buttons for hit and jump
* One virtual button for the special move
* Two bars for life and and energy
* A timer
