# Circuitry Part 1 (Assignment 4) Description
The first iteration of this game is simple 2D platformer where the player must avoid a virus that chases them around the circuit board. The player has to collect pieces of themselves in order to increase the score. Getting hit means resetting the score and restarting from the beginning!

# How to Run?
This project can be opened in the editor after downloading and be run from there.

# Controls
- Left Arrow - Move left
- Right Arrow - Move right
- Spacebar - Jump 

# Assets
All art assets were designed and implemented by me, there is no external work here.

# Requirements
- Entire map is made using a TileSet, this is done with a TileMapLayer node with custom tiles used for the background and platforms.
- Player and Enemy characters have proper movement physics with collisions. There is collision between characters, the environment, and the various circuits that are scattered around the map.
- Player and Enemy both have animated sprites. The player has animations for idle and walking animations, which are flipped based on movement. The enemy also has a walking animation that plays as it chases the player.
- Enemy will continuously pathfind towards player, this is done by creating navigation layers over the background tiles which allows the enemy full access over the map. The enemy is also placed on a separate mask layer to make collisions with the environment smoother.
- Particle effects are shown when a circuit is collected and when the player is hit. The particles are shown as small bursts of either electricity or smoke. This is accomplished with the spread and one-shot properties of the particle material.
- Collectible circuit boards are shown around the map with animations. Players can pick these up to increment the score by colliding with them. Getting hit by the enemy will cause all circuit locations to reset.
- UI feedback for score that increments after each circuit is picked up. Getting hit will reset the score to 0.
- All art and assets used for this project are entirely original. This paired with some camera smoothing effects and borders helps the game to feel more polished.
