using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    public int health = 5;

    //Animator required
    public bool animatorRequired;

    // AI variables
    Transform player;
    PathFinding pathFinding;
    List<Node> path;
    
    // Movement speed
    public float speed = 2f;

    //Attack properties
    public enum AttackType { MELEE, RANGED };
    public AttackType enemyType;
    bool attacking;
    
    // Attack with bullets
    public float attackingFreq = 0.7f;
    float timeToAttack;
    public GameObject bullet;
    public float shotSpeed = 15.0f;

    

    // Start is called before the first frame update
    void Start()
    {
        // Startup the pathfinding to find the path to the player in the room
        pathFinding = new PathFinding(16, 10, getRoomIndex(transform.position));
        // Set the time to attack
        timeToAttack = 1.0f / attackingFreq;
        // At the start, the enemy is not attacking
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeToAttack -= Time.deltaTime;        
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("PlayerP");
            if (p != null) player = p.transform;
            
            else
            {
                // Player is dead
                GetComponent<Animator>().SetBool("dancing", true);
            }
        }

        if (!attacking)
        {
            // Get my position in the room
            Vector2 enemyPos = getRoomPosition(transform.position);
            // Get the player position in the room
            Vector2 playerPos = getRoomPosition(player.position);
            
            // Find path to the player
            Node nextNode;
            if (!inRangeToAttack(enemyPos, playerPos, out nextNode))
            {
                // Get the position of the next node
                Vector3 nextNodePos = new Vector3(nextNode.getX(), 0, nextNode.getY());
                // get the direction to the next node
                Vector3 direction = nextNodePos - new Vector3(enemyPos.x, 0, enemyPos.y);
                // Move to the direction represented by the direction vector
                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
                // Rotate the enemy to face the direction of the next node
                transform.rotation = Quaternion.LookRotation(direction);
                // Change the animator to start the animation in case it has not started yet
                bool enemyAlreadyMoving = GetComponent<Animator>().GetBool("moving");
                if (!enemyAlreadyMoving) GetComponent<Animator>().SetBool("moving", true);
            } else
            {
                // Rotate the enemy to face the player
                transform.rotation = Quaternion.LookRotation(player.position - transform.position);
                // Attack the player
                attackPlayer();
            }
        }
    }

    /**
     * Returns true if the enemy is in range to attack the player, otherwise false (including null path)
     */
    private bool inRangeToAttack(Vector2 enemyPos, Vector2 playerPos, out Node nextNode)
    {
        // Get the path
        path = pathFinding.getPath(enemyPos, playerPos);
        pathFinding.restartRoom();
        // In case the enemy is at the same position as the player return
        if (path == null)
        {
            nextNode = default;
            return false;
        }
        //Set the next node were the enemy will move to in case it is not in range
        nextNode = path[1];
        int pathLength = path.Count;
        // Check if the enemy is in range to attack the player
        if (enemyType == AttackType.MELEE)
        {
            return pathLength <= 2;
        }
        else
        {
            return pathLength <= 5;
        }   
    }

    /**
     * Attacks the player taking into account the type of enemy (range, melee)
     */
    void attackPlayer()
    {        
        // distance to player
        float distance = Vector3.Distance(transform.position, player.position);
        // If the enemy is a melee enemy
        if (enemyType == AttackType.MELEE && distance > 2.5f)
        {
            //Move to the player
            transform.Translate((player.position - transform.position).normalized * speed * Time.deltaTime, Space.World);
            if (animatorRequired && !GetComponent<Animator>().GetBool("moving")) GetComponent<Animator>().SetBool("moving", true);
        }

        else if (timeToAttack < 0.0f)
        {
            // Set the time to attack
            timeToAttack = 1.0f / attackingFreq;
            if (animatorRequired)
            {
                bool enemyAlreadyMoving = GetComponent<Animator>().GetBool("moving");
                if (enemyAlreadyMoving) GetComponent<Animator>().SetBool("moving", false);
            }

            switch (enemyType)
            {
                case AttackType.MELEE:
                    // Melee attack
                    if (animatorRequired) transform.GetComponent<Animator>().SetBool("attacking", true);
                    // Invoke the "StopAttacking" function after 0.3 seconds
                    Invoke("StopAttacking", 0.3f);
                    break;
                case AttackType.RANGED:
                    // Instantiate the new bullet in front of the enemy
                    GameObject newBullet = Instantiate(bullet, transform.position + transform.forward, Quaternion.identity);
                    // Set the velocity of the bullet
                    Vector3 direction = player.position - transform.position;
                    direction.y = 0;
                    direction = Vector3.Normalize(direction) * shotSpeed;
                    newBullet.GetComponent<Rigidbody>().velocity = direction;
                    break;
            }
        }
        else
        {
            if (animatorRequired && GetComponent<Animator>().GetBool("moving")) GetComponent<Animator>().SetBool("moving", false);
        }
        
    }

    private void StopAttacking()
    {
        if (animatorRequired) transform.GetComponent<Animator>().SetBool("attacking", false);
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
