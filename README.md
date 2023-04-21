# Pedestrian-Tool-Unity
Pedestrian/Traffic tool to easily create a simple pedestrian type system
Full credit goes to Game Dev Guide on YouTube: https://www.youtube.com/watch?v=MXCZ-n5VyJc
I created this repository so I could easily implement it into any of my other projects where I need this behavior

1. Drag and drop the entire Pedestrian Folder into your Project
2. Open the Tools --> Waypoint Editor Window
3. Create an empty object in the scene and call it Waypoints
4. Add the PedestrianSpawner script this GameObject
5. Assign this GameObject as the Waypoint Root
6. Use the Create Waypoint button to start building your path
7. You can use the before and after to explicity state the direction of the path (IE if you need to build it backwards at some parts)
8. Use branches to connect 2 or more paths together

9. Create a prefab of a character with the following
  i. Animator with the PedestrianWalking controller
  ii. Pedestrian Controller Script
  iii. Pedestrian Navigator Script
10. Change the animations to Humanoid
11. Click the animations in the controller - add the animations under the Motion variable if they are not there
12. Uncheck exit times for the transitions if not already
13. Make sure the bool IsWalking is set up correctly in the transitions
14. Make sure both animations loop
15. Assign the prefabbed character to the Pedestrian Spawner script and set a number of pedestrians to spawn

16. Create another Empty GameObject and call it Pedestrians - this will be the parents for all spawned pedestrians
17. Add a new tag and label it "PedParent" (You can change this if you want in the Pedestrian Spawner script)

18. Play the game - you should have pedestrians following your path
