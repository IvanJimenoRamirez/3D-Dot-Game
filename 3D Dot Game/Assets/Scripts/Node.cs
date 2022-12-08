using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Room<Node> room;
    private int x, y;

    // Collisions
    private List<List<Vector2>> collisions = new List<List<Vector2>>()
    {
        new List<Vector2>(){},  // 0 - No collisions at the moment
        new List<Vector2>(){    // 1 - Room 1 collisions
            new Vector2(3, 3),
            new Vector2(4, 3),
            new Vector2(5, 3),
            new Vector2(6, 3),
            new Vector2(7, 3),
            new Vector2(8, 3),
            new Vector2(9, 3),
            new Vector2(10, 3),
            new Vector2(11, 3),
            new Vector2(12, 3),

            new Vector2(3, 4),
            new Vector2(4, 4),
            new Vector2(5, 4),
            new Vector2(6, 4),
            new Vector2(7, 4),
            new Vector2(8, 4),
            new Vector2(9, 4),
            new Vector2(10, 4),
            new Vector2(11, 4),
            new Vector2(12, 4),

            new Vector2(5, 5),
        },
        new List<Vector2>(){},  // 2 - No collisions at the moment
        new List<Vector2>(){},  // 3 - No collisions at the moment
        new List<Vector2>(){},  // 4 - No collisions at the moment
        new List<Vector2>(){},  // 5 - No collisions at the moment
        new List<Vector2>(){},  // 6 - No collisions at the moment
        new List<Vector2>(){},  // 7 - No collisions at the moment
        new List<Vector2>(){},  // 8 - No collisions at the moment
        new List<Vector2>(){},  // 9 - No collisions at the moment
        new List<Vector2>(){},  // 10 - No collisions at the moment
        new List<Vector2>(){},  // 11 - No collisions at the moment
        new List<Vector2>(){},  // 12 - No collisions at the moment
    };
    public int gCost = int.MaxValue, hCost = 0, fCost;
    
    public Node parent;

    public Node(Room<Node> room, int x, int y)
    {
        this.room = room;
        this.x = x;
        this.y = y;
        this.calcFCost();
        this.parent = null;
    }

    public void calcFCost ()
    {
        fCost = gCost + hCost;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }
    public bool isWalkable()
    {
        // TODO: Check if the node is walkable (there is any obstacle)
        return isWalkable(room.getRoomIndex());
    }

    private bool isWalkable(int roomIndex)
    {
        // Get the room collisions from the collisions list
        List<Vector2> roomCollisions = collisions[roomIndex];
        // Check if the node is in the list of collisions
        foreach (Vector2 collision in roomCollisions)
        {
            if (collision.x == x && collision.y == y)
            {
                return false;
            }
        }
        return true;
    }
}