using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinding
{
    private const int COST_DIAGONAL_MOVE = 14;
    private const int COST_AXIS_MOVE = 10;

    private Room<Node> room;

    public PathFinding(int width, int height, int roomIndex)
    {
        room = new Room<Node>(width, height, roomIndex,
            (room, x, y) => new Node(room, x, y)    // Constructor for Nodes
           );
    }

    /*
     * Constructor
    */
    public List<Node> getPath(Vector2 start, Vector2 end)
    {
        return findPath((int)start.x, (int)start.y, (int)end.x, (int)end.y);
    }

    /*
     * Find the path
    */
    private List<Node> findPath(int startx, int starty, int endx, int endy)
    {
        // Starting node and end node
        Node startNode = room.getValue(startx, starty);
        Node endNode = room.getValue(endx, endy);

        // Initiallize the cost for the first node, it can be set to 0
        startNode.gCost = 0;
        startNode.hCost = calcDistance(startNode, endNode);
        startNode.calcFCost();

        // Initiallize the openSet
        HashSet<Node> openSet = new HashSet<Node>() { startNode };
        // Initiallize the closedSet
        HashSet<Node> closedSet = new HashSet<Node>();

        while (openSet.Count > 0)
        {
            // Get the node with the lowest fCost, as the sort function defines the nodes to be ordered by fcost ascending, we can just get the first element
            Node q = getMinFCost(openSet);
            openSet.Remove(q);
            // If the node is the end node, we have found the path
            if (q == endNode)
            {
                return calcPath(endNode);
            }
            closedSet.Add(q);

            // Generate the successors of q
            List<Node> successors = generateSuccessors(q);
            foreach (Node n in successors)
            {
                if (closedSet.Contains(n)) continue;
                if (!n.isWalkable())
                {
                    closedSet.Add(n);
                    continue;
                }
                int gCost = q.gCost + calcDistance(q, n);

                if (gCost < n.gCost)
                {
                    n.gCost = gCost;
                    n.hCost = calcDistance(n, endNode);
                    n.calcFCost();
                    n.parent = q;
                    // Check if a node with the same position as Node n is in the openSet
                    if (!openSet.Contains(n))
                    {
                        openSet.Add(n);
                    }
                }
            }
        }
        return default;
    }

    /*
     * Generate the successors from a given node (including diagonal moves)
    */
    private List<Node> generateSuccessors(Node node)
    {
        int x = node.getX();
        int y = node.getY();

        List<Node> successors = new List<Node>();
        List<Vector2> directions = new()
        {
            new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
        };

        foreach (Vector2 direction in directions)
        {
            int dx = (int)direction.x;
            int dy = (int)direction.y;

            if (isInRoom(x + dx, y + dy))
            {
                successors.Add(room.getValue(x + dx, y + dy));
            }
        }
        return successors;
    }

    /*
     * Given two coordinades, check if the node is in the room
    */
    private bool isInRoom(int x, int y)
    {
        return x >= 0 && x < room.getWidth() && y >= 0 && y < room.getHeight();
    }

    /*
     * Calculates the distance between two nodes
    */
    private int calcDistance(Node a, Node b)
    {
        int dx = Math.Abs(a.getX() - b.getX());
        int dy = Math.Abs(a.getY() - b.getY());
        int diff = Math.Abs(dx - dy);
        return COST_DIAGONAL_MOVE * Math.Min(dx, dy) + COST_AXIS_MOVE * diff;
    }

    /*
     * Get the node with the lowest fCost
    */
    private Node getMinFCost(HashSet<Node> nodes)
    {
        Node min = nodes.First();
        foreach (Node node in nodes)
        {
            if (node.fCost < min.fCost)
            {
                min = node;
            }
        }
        return min;
    }

    /*
     * Calculate the final path given the end node
    */
    private List<Node> calcPath(Node finalNode)
    {
        List<Node> path = new List<Node>();
        path.Add(finalNode);
        while (finalNode.parent != null)
        {
            finalNode = finalNode.parent;
            path.Add(finalNode);
        }
        path.Reverse();
        return path;
    }
    public void restartRoom()
    {
        //that's only useful if this algorithm is used multiple times, as we need to restart the Nodes values
        for (int x = 0; x < room.getWidth(); x++)
        {
            for (int y = 0; y < room.getHeight(); y++)
            {
                Node node = room.getValue(x, y);
                node.gCost = int.MaxValue;
                node.calcFCost();
                node.parent = null;
            }
        }
    }
}