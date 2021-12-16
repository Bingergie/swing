# Swing

"Swing" is a 2D platformer game that is made using the Unity game engine. The aim of the game is to reach the end of all 5 levels. 

## Player Movement

The player is a white square that can move left and right, jump, and grapple to grapple targets. 

**Moving Left / Right**

By pressing "a" or "d" while the player not grappling on to anything, a script applies a force that pushes the player towards the corresponding absolute direction. The absolute direction is used because the player can rotate around its center, changing the relative direction. If the player is at max speed, the force will not be applied at that frame. 

When the player is on the ground, a counter movement is applied in the opposite direction to prevent the player from sliding around after the buttons are released. This makes the controls snappier. 

**Jumping**

When the space bar is pressed while the player is on the ground or grappling to an object, an upward force is applied. The gravity on the "rigid body" component will pull it down. 

**Grappling**

The grappling mechanic works by raycasting from the player towards the direction of the mouse when left mouse button is pressed down. If there is a grappling target within a fairly large range, a spring joint is created, joining the grappling target and the player. Then, the "distance" attribute of the joint is shortened by 20% to give room for swinging. A well timed jump while grappling can create a very cool effect. 

**Time Scale**

In order to make the controls of the player snappier, I increased the time scale of the game to 2. 

## Start Screen

The start screen is the first scene that loads. On load, there is a fade in from black animation that plays. On the top of the screen, there is a title that I made. It has an animation that plays on repeat. Below the title are the buttons. There are 3 buttons that becomes larger when you hover over them. The "Quit" button quits the app. The " Controls" button takes you to the controls menu, where the controls for the game are shown. The "Play" button triggers a fade out to black animation then loads the first level. 

## Levels

There are 5 levels in the game. On load, a fade in from black animation plays, just like the start screen. Every level has a minimap on the top right corner of the screen. This was achieved by using a second camera that outputs to a render texture then adding the render texture to a raw image on the canvas. The main camera follows the player around by using cenimachine's virtual cameras. 

In a level, there can be platforms and hexagon shaped grapple targets. Each level is somewhat harder than the previous one. When the player reaches the end of a level, a fade out from black animation is played, and the next level is loaded. If there are no levels left, the victory screen is loaded. However, if, at any moment, the player falls into the void below the level, a fade out from black animation is played and the death screen is loaded. 

## Victory / Death Screen

The victory screen and the death screen are very similar. The only difference is that the former shows "You Won!" while the latter shows "You Died". This message is shown at the center of the screen. Below this is a message that says "Click anywhere to continue" in a smaller font and fades in and out. Once the left mouse button is pressed, the start screen loads. 