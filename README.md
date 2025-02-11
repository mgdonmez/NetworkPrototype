# NetworkPrototype
 A network prototype project made for evaluation

# How to play
When you run the project, it will authomatically run a shared server room, connect to the server and spawn a player.
If you run multiple instances, a new instance will connect to the created room if available.

You can use WASD to walk and mouse to rotate the camera and interact with objects.
To grab the ball, aim and press left mouse button and drag wile holding the button.
To release the ball, release the left mouse button.
To interact with 3D button (orange cube), aim and click left mouse button. It will change the ball state.
While the ball is in BallStateActive, the ball will not be interactive.
To interact with the ball, click the 3D button to change the state to BallStateIdle.

# Project Structure
## Environment
The models and materials are created by the developer using Unity's basic 3D objects or Blender.

## Network Solution
Photon Fusion 2 Shared Mode is used for this project.
Shared room is created authomatically when the project is run.

## Interactivity
First person camera and input is used to move and rotate. 
WASD keyboard, mouse movement and MouseButton(0) are used to interact with the world.
With the MouseButton(0), raycast is used to interact with Grabbable Object (Ball) and Switch Button (3D Button).

When the ball is grabbed, color is changed from red to green, a particle effect is played and it is move to the holding position.
When released, color is changed from green to red, and it returns to the default position.There are two states for the ball. The state is toggled between idle and active by the 3D Button.

Triggering the 3D button will change the BallState.

While in idle (BallIdleState), the ball is interactable, at the default position.
While in active (BallActiveState), the ball is not interactable and follows an invisible transform that moves around. For follow movement, TweenTrack and TweenClip are used in a Timeline.

## UI Integration

All texts use TMP_Pro and under a canvas with Fusion Basic Billboard component.

A random name from a list in the GameManager is given to the player before spawning.
Player name is shown in a canvas inside PlayerCharacter prefab via PlayerStats script.
Names are synched with RPC calls.

Current ball state is shown with BallStateViewer script under SwitchButton prefab.

# Known Issues
* The first interaction can be glitchy with wrong color, stutter on the movement or late response.
* If an instance is ended and a new instance is created with the existing room, interactions may brake.
* Player name can be repeated on multiple players because the name pool is small.