using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    public int health = 5;

    // AI variables
    public Transform player;
    PathFinding pathFinding;
    List<Node> path;
    

    // Movement speed
    public float speed = 2f;

    // Attack with bullets
    public float shootingFreq = 0.7f;
    float timeToShoot;
    public GameObject bullet;
    public float shotSpeed = 15.0f;

    

    // Start is called before the first frame update
    void Start()
    {
        pathFinding = new PathFinding(16, 10, getRoomIndex(transform.position));
        timeToShoot = 1.0f / shootingFreq;
    }

    // Update is called once per frame
    void Update()
    {
        timeToShoot -= Time.deltaTime;        
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("PlayerP");
            if (p != null) player = p.transform;
            else return;
        }

        // Get my position in the room
        Vector2 enemyPos = getRoomPosition(transform.position);
        // Get the player position in the room
        Vector2 playerPos = getRoomPosition(player.position);

        // Get the path to the player
        path = pathFinding.getPath(enemyPos, playerPos);
        pathFinding.restartRoom();

        // Move towards the next node in the path (taking into account the node 0 is the current position)
        if (path != null && path.Count > 5)
        {
            // Get the next node in the path
            Node nextNode = path[1];
            // Get the position of the next node
            Vector3 nextNodePos = new Vector3(nextNode.getX(), 0, nextNode.getY());
            // get the direction to the next node
            Vector3 direction = nextNodePos - new Vector3(enemyPos.x, 0, enemyPos.y);
            // Move to the direction represented by the direction vector
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            // Rotate the enemy to face the direction of the next node
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            // Rotate the enemy to face the player
            transform.rotation = Quaternion.LookRotation(player.position - transform.position);
            
            // If the path is null or the path is greater than 5, then shot the player a bullet
            if (timeToShoot < 0.0f)
            {
                
                timeToShoot = 1.0f / shootingFreq;
                // Instantiate the new bullet in front of the enemy
                GameObject newBullet = Instantiate(bullet, transform.position + transform.forward, Quaternion.identity);
                // Set the velocity of the bullet
                Vector3 direction = player.position - transform.position;
                direction.y = 0;
                direction = Vector3.Normalize(direction) * shotSpeed;
                newBullet.GetComponent<Rigidbody>().velocity = direction;
            }
        }

    }


    /**
     * Given two positions in the world, returns a vector2 with the position in the room
     */
    private Vector2 getRoomPosition(Vector3 worldPosition)
    {         
        int roomX = (int) Mathf.Floor(Mathf.Floor(worldPosition.x % 32f) / 2f);
        int roomZ = (int)Mathf.Floor(Mathf.Floor(worldPosition.z % 20f) / 2f);
        return new Vector2(roomX, roomZ);
    }

    // On collide
    private void OnCollisionEnter(Collision collision)
    {
        // If the enemy collides with the sword, then destroy the enemy
        if (health > 0)
        {
            if (collision.gameObject.tag == "BigSwordP")
            {
                health -= 2;
            }
            if (collision.gameObject.tag == "LittleSwordP")
            {
                health--;
            }
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
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
}
