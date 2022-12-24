using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Room<Node> room;
    private int x, y;

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
        return isWalkable(room.getRoomIndex());
    }

    private bool isWalkable(int roomIndex)
    {
        // Get the room collisions from the collisions list
        List<Vector2> roomCollisions = GameObject.Find("GameManager").GetComponent<GameManager>().collisions[roomIndex];
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