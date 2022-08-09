## About
This repo contains an implementation of Snake with **A*** **pathfinding**. 

### Goals
 - Implement a working and efficient **A* pathfinding** algorithm with weighed pickups.
 - Implement **linked lists**.
 - Easy to change the variables of the **simulation**.

## List of Features
| Feature| Description |
|--|--|
| Grid Generation| **Grid map** generator with neighbour checking |
| Screen Wrapping | **Screen wrapping** on map's edgess |
| Snake Linked List | Implementation of **linked list** used in snakes |
| A* Pathfinding | **A*** **pathfinding**.  for snake's AI with weighed pickups |
| Snake Slicing | Break snake's linked lists in **runtime** |

### Grid Generation


## Keybindings
| Key| Description |
|--|--|
| W| Move up|
| A | Move left|
| S | Move down|
| D | Move right |

## Issues & Possible fixes
**Issue:** Snake slicing isn't part of the AI decision making so snakes won't attack when expected.
**Possible solution:** Snakes will account for other's size and attack when big enough to eat. Let this weigh in on the A* decision making. Having a slider to control the snake's aggressiveness is also a nice-to-have for simulation purposes.

**Issue:** Snakes calculate full path on every tick (currently set at 0.1s) when they end up not following most of it. Snakes also account for all objects on the grid for their decision making, even the ones that are very far away and that will practically never be targets.
**Possible solution:** Calculate new path only if there's a changes to the current path or if new objects are spawned inside a certain range.

## Sources & Inspiration
