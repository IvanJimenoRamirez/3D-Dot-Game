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

    //Materials
    public Material mat;
    public Material vulnerableMat;

    // Lateral movement
    int lateralMovement = 0;
    public bool right = true;
    bool lateralReached = false;

    //Game manager of the game
    GameManager manager;

    //Taking damage properties
    public bool lastBody = false;
    public int health = 5;
    bool vulnerable = false;
    float vulnerableTime = 0.0f;

    private void Start()
    {
        // Get the game manager of the game
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        //Set up basic properties
        position = transform.position;
        head = GameObject.Find("Boss(Clone)").transform.GetChild(0).gameObject;
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
        lateralAnimation();
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
        if (lastBody)
        {
            if (!vulnerable)
            {
                // If the boss is not vulnerable, check if it should be with a 0.5%
                if (Random.Range(0, 200) == 28)
                {
                    vulnerable = true;
                    vulnerableTime = 0.0f;
                    // Change the material of the boss
                    GetComponent<Renderer>().material = vulnerableMat;
                }
            }
            else
            {
                // If the boss is vulnerable, check if it should stop being vulnerable
                vulnerableTime += Time.deltaTime;
                if (vulnerableTime > 2.0f)
                {
                    vulnerable = false;
                    //change the material of the body
                    GetComponent<Renderer>().material = mat;
                }
            }
        }
    }

    private void lateralAnimation()
    {
        if (moving)
        {
            if (lateralReached && lateralMovement == 0)
            {
                right = !right;
                lateralMovement = 0;
                lateralReached = false;
            }
            else
            {
                if (lateralReached)
                {
                    transform.position +=  transform.TransformDirection(new Vector3(((right) ? -0.01f : 0.01f), 0, 0));
                    --lateralMovement;
                }
                else
                {
                    transform.position += transform.TransformDirection(new Vector3(((right) ? 0.01f : -0.01f), 0, 0));
                    ++lateralMovement;
                }

                if (lateralMovement == 20) lateralReached = true;
            }
        }
        
    }

    private void move() 
    {
        float distance = calcDistanceToParent();
        if (distance > 1.3f)
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
            new List<int> { -1, 3, 5, 6, 13, 13 },  // -1 just to make easier the return
            new List<int> { 1, 2, 4, 11, 13, 13 },
            new List<int> { -1, 8, 9, 10, 13, 13}, // -1 just to make easier the return
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
    private void OnCollisionEnter(Collision collision)
    {
        if (lastBody && vulnerable)
        {
            if (collision.gameObject.tag == "BigSwordP")
            {
                health -= 5;
            }
            if (collision.gameObject.tag == "LittleSwordP")
            {
                health -= 2;
            }
            if (health <= 0)
            {
                //Startup new properties
                float actualSpeed = head.GetComponent<EnemyManager>().speed;
                actualSpeed += 0.5f;
                if (parent != null)
                {
                    parent.lastBody = true;
                    parent.changeSpeed(actualSpeed);
                } else
                {
                    head.GetComponent<BoxCollider>().enabled = true;
                    changeSpeed(actualSpeed);
                }
                // Explode
                transform.GetComponent<Explosion>().exploded = true;
            }
        }
    }

    public void changeSpeed (float newSpeed)
    {
        if (parent != null) parent.changeSpeed(newSpeed);
        else head.GetComponent<EnemyManager>().bodyDestroyed(newSpeed);
        speed = newSpeed;
    }
}
