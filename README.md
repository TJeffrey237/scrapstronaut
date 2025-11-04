# Assignment 5 Description
This is a simple demo game where a player (CharacterBody2D) can look at and interact with some different properties.

## Controls
- Left Arrow - Move left
- Right Arrow - Move right
- Up Arrow - Move Up
- Down Arrow - Move Down

## How Does the Shader Work?
The canvas item shader itself applies two different effects to the particles: wave distortion and color gradient calculations. The particle itself is a simple texture of an outlined circle.

The particle controller script allows us to edit the parameters for this shader which allows us to use time as a way to change the wave intensity and rotate through various colors.

## Physics Properties
The physics chain cs script will create a chain of linked segments. To do this, it will first define an anchor point and then iteratively instantiating the RigidBody2D segments that are vertically offset to form a straight line.

In order to connect each segment, I utilize PinJoint2D to connect them at a given point and allow them to freely rotate around that point. The very first segment created will be attached to the static anchor point but each segment afterwards will have it's joint connected at the halfwaypoint between the two segments.

The player is able to freely interact with this chain by colliding with any of it's segments. The collision is primarily handled automatically through Godot but the ApplyForceToSegment method allows us to apply a force randomly to the chain segments if needed.

## Raycast Detection
The raycast script is a way to detect when a player is colliding with a line, much like the LOS for an enemy NPC. The first part of the script initializes the ray, the visual effects, and the alarm timer. Setting up the raycast involves creating a RayCast2D and setting a range for it's maximum length. The visual effects use a Line2D which is colored red or green depending on the alarm. The alarm timer is meant as a way to reset the alarm's state back to default after a given time.

Detecting collision with the way in the script is accomplished by consistently updating it's physics frame and checking for a collision. If a collision occurs, then the laser is visually updated to reflect the collision point. Furthermore, if the collision is the player, then the alarm is triggered and the console prints out the location of the collision and the laser turns red. Afterwards, the laser will not be reset until the alarm timer times out and the player is no longer in range.
