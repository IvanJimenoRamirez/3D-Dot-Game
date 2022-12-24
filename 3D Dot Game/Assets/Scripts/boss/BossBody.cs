using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBody : MonoBehaviour
{


    // Movement speed
    public float speed = 2f;

    // Basic properties
    Vector3 position;
    public BossBody parent = null;
    public bool moving = true;
    GameObject head;
    Vector2 roomPosition;
    int roomIndex;

    //Game manager of the game
    GameManager manager;

    private void Start()
    {
        // Get the game manager of the game
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        //Set up basic properties
        position = transform.position;
        head = GameObject.Find("Boss").transform.GetChild(0).gameObject;
        roomPosition = getRoomPosition(transform.position);
        roomIndex = getRoomIndex(transform.position);

        //Set up the moving variable
        if (parent != null)
        {
            moving = parent.moving;
        }
        else
        {
            moving = head.GetComponent<EnemyManager>().moving;
        }
    }

    private void Update()
    {
        updateProperties();
        if (moving) move();
    }

    private void updateProperties()
    {
        Vector2 newRoomPosition = getRoomPosition(transform.position);
        if (newRoomPosition != roomPosition)
        { 
            manager.setCollision(roomIndex, newRoomPosition);
            manager.removeCollision(roomIndex, roomPosition);
            roomPosition = newRoomPosition;
        }
        if (parent != null)
        {
            moving = parent.moving;
        }
        else
        {
            moving = head.GetComponent<EnemyManager>().moving;
        }
    }

    private void move() 
    {
        float distance = calcDistanceToParent();
        if (distance > 0.9f)
        {
            // Get the position of the next node
            Vector3 nextNodePos = (parent != null) ? parent.position : head.transform.position;
            // get the direction to the next move
            Vector3 direction = nextNodePos - position;

            // Rotate the enemy to face the direction of the next move
            transform.rotation = Quaternion.LookRotation(direction);

            // Move to the direction represented by the direction vector
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            position = transform.position;
        }
    }

    /**
     * Given two positions in the world, returns a vector2 with the position in the room
     */
    private Vector2 getRoomPosition(Vector3 worldPosition)
    {
        int roomX = (int)Mathf.Floor(Mathf.Floor(worldPosition.x % 32f) / 2f);
        int roomZ = (int)Mathf.Floor(Mathf.Floor(worldPosition.z % 20f) / 2f);
        return new Vector2(roomX, roomZ);
    }

    /**
     * Given a position in the world, returns the index of the room
     */
    private int getRoomIndex(Vector3 worldPosition)
    {
        List<List<int>> roomIndexes = new List<List<int>>()
        {
            new List<int> { 7 },
            new List<int> { -1, 3, 5, 6 },  // -1 just to make easier the return
            new List<int> { 1, 2, 4, 11 },
            new List<int> { -1, 8, 9, 10 }, // -1 just to make easier the return
            new List<int> { 12 }
        };

        int roomX = (int)Mathf.Floor(worldPosition.x / 32f);
        if (roomX == 0 || roomX == 4) return roomIndexes[roomX][0];

        int roomZ = (int)Mathf.Floor(worldPosition.z / 20f);
        return roomIndexes[roomX][roomZ];
    }

    /**
     * Returns the distance to the parent (if the parent equals null, then it returns the distance to the head)
     */
    private float calcDistanceToParent()
    {
        if (parent == null)
        {
            return Vector3.Distance(transform.position, head.transform.position);
        }
        else
        {
            return Vector3.Distance(transform.position, parent.transform.position);
        }
    }
}
