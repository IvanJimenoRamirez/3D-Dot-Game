using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    //PathFinding pathFinding;
    // Start is called before the first frame update
    private void Start()
    {
        //pathFinding = new PathFinding(16, 10, 1);
    }

    private void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            // measure the execution time of the pathfinding algorithm
            System.Diagnostics.Stopwatch stopwatch1 = new System.Diagnostics.Stopwatch();
            stopwatch1.Start();
            List<Node> path1 = pathFinding.getPath(new Vector2(8, 5), new Vector2(3, 4));
            stopwatch1.Stop();
            Debug.Log("Pathfinding algorithm 1 took " + stopwatch1.ElapsedMilliseconds + "ms");

            if (path1 != null)
            {
                Debug.Log("Path 1: " + path1.Count);
                foreach (Node node in path1)
                {
                    Debug.Log(node.getX() + ", " + node.getY());
                }
            }
            pathFinding.restartRoom();
        }
        */
    }
}
