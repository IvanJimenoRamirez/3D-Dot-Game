using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel.Design;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    public int health = 5;
    
    // Die properties
    public bool died;
    public Material mat;

    //Animator required
    public bool animatorRequired;

    // AI variables
    Transform player;
    PathFinding pathFinding;
    List<Node> path;

    // Movement speed
    public float speed = 2f;

    //Attack properties
    public enum AttackType { MELEE, RANGED, BOSS, NONE };
    public AttackType enemyType;
    float timeToAttack;
    bool attacking;

    // Attack with bullets
    public float attackingFreq = 0.7f;
    public GameObject bullet;
    public float shotSpeed = 15.0f;

    //Boss properties
    bool isBoss = false;
    BossBody[] body;
    public Vector2 myPosition;
    public bool moving = true;
    bool performingSpecial = false;
    public GameObject frontalShotHolder;
    public GameObject leftShotHolder;
    public GameObject rightShotHolder;
    public GameObject right2ShotHolder;
    public GameObject right3ShotHolder;
    public GameObject left2ShotHolder;
    public GameObject left3ShotHolder;

    // Start is called before the first frame update
    void Start()
    {
        // Set the time to attack
        timeToAttack = 1.0f / attackingFreq;
        // At the start, the enemy is not attacking
        attacking = false;
        // Boss properties
        if (enemyType == AttackType.BOSS) {
            isBoss = true;
            myPosition = getRoomPosition(transform.position);
            GetComponent<BoxCollider>().enabled = false;
            // Startup the pathfinding to find the path to the player in the room
            pathFinding = new PathFinding(32, 18, 13);
        } else
        {
            // Startup the pathfinding to find the path to the player in the room
            pathFinding = new PathFinding(16, 10, getRoomIndex(transform.position));
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeToAttack -= Time.deltaTime;
        if (died)
        {
            GetComponent<Explosion>().explosionMat = mat;
            GetComponent<Explosion>().exploded = true;
        }
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("PlayerP");
            if (p != null) player = p.transform;
            else
            {
                // Player is dead
                if (animatorRequired) GetComponent<Animator>().SetBool("dancing", true);
            }
        }

        if (!attacking && enemyType != AttackType.NONE && !performingSpecial)
        {
            // If the enemy is not attacking, it will move or attack
            moveAndAttack();
        }
    }

    /**
     * Move || attack to the player
     */
    private void moveAndAttack()
    {
        // Get my position in the room
        Vector2 enemyPos = getRoomPosition(transform.position);
        // Get the player position in the room
        Vector2 playerPos = getRoomPosition(player.position);

        // Find path to the player
        Node nextNode;
        if (!inRangeToAttack(enemyPos, playerPos, out nextNode))
        {
            float nextX, nextY;

            if (nextNode == null)
            {
                // Move to a random direction, taking into account the obstacles in the room
                List<Vector2> roomCollisions = GameObject.Find("GameManager").GetComponent<GameManager>().collisions[getRoomIndex(transform.position)];
                List<Vector2> directions = new List<Vector2>() {
                    new Vector2(0, 1),
                    new Vector2(0, -1),
                    new Vector2(1, 0),
                    new Vector2(-1, 0),
                    new Vector2(1, 1),
                    new Vector2(1, -1),
                    new Vector2(-1, 1),
                    new Vector2(-1, -1)
                };
                List<Vector2> randomMovement = new List<Vector2>();
                foreach (Vector2 move in directions)
                {
                    if (!roomCollisions.Contains((enemyPos + move))) randomMovement.Add((enemyPos + move));
                }
                Vector2 position = randomMovement[UnityEngine.Random.Range(0, randomMovement.Count)];
                nextX = position.x;
                nextY = position.y;
            } else
            {
                nextX = nextNode.getX();
                nextY = nextNode.getY();
            }

            // If the enemy is a boss, update the moving value to make the childs move
            if (isBoss) moving = true;

            // Get the position of the next node
            Vector3 nextNodePos = new Vector3(nextX, 0, nextY);
            // get the direction to the next node
            Vector3 direction = nextNodePos - new Vector3(enemyPos.x, 0, enemyPos.y);
            // Move to the direction represented by the direction vector
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            // Rotate the enemy to face the direction of the next node
            transform.rotation = Quaternion.LookRotation(direction);


            // Change the animator to start the animation in case it has not started yet
            if (animatorRequired)
            {
                bool enemyAlreadyMoving = GetComponent<Animator>().GetBool("moving");
                if (!enemyAlreadyMoving) GetComponent<Animator>().SetBool("moving", true);
            }
        }
        else
        {
            // Rotate the enemy to face the player
            transform.rotation = Quaternion.LookRotation(player.position - transform.position);
            if (enemyType != AttackType.NONE)
            {
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
        if (path == null || path.Count == 1)
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
        else if (enemyType == AttackType.RANGED)
        {
            return pathLength <= 5;
        }
        else
        {
            // BOSS
            moving = false;
            return pathLength <= 6;
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
                case AttackType.BOSS:
                    // Instantiate the new bullet in front of the enemy, with velocity defined to go in front of the enemy (not in the direction of the player)
                    GameObject newBulletBoss = Instantiate(bullet, frontalShotHolder.transform.position, Quaternion.identity);
                    newBulletBoss.GetComponent<Rigidbody>().velocity = transform.forward * shotSpeed;

                    GameObject newBulletBossLeft = Instantiate(bullet, leftShotHolder.transform.position, Quaternion.identity);
                    newBulletBossLeft.GetComponent<Rigidbody>().velocity = (-transform.right + transform.forward * 4).normalized * shotSpeed;

                    GameObject newBulletBossRight = Instantiate(bullet, rightShotHolder.transform.position, Quaternion.identity);
                    newBulletBossRight.GetComponent<Rigidbody>().velocity = (transform.right + transform.forward * 4).normalized * shotSpeed;
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
        if (enemyType != AttackType.BOSS)
        {
            int roomX = (int)Mathf.Floor(Mathf.Floor(worldPosition.x % 32f) / 2f);
            int roomZ = (int)Mathf.Floor(Mathf.Floor(worldPosition.z % 20f) / 2f);
            return new Vector2(roomX, roomZ);
        } else
        {
            int roomX = (int)Mathf.Floor(Mathf.Floor((worldPosition.x - 48f) % 64f) / 2f);
            int roomZ = (int)Mathf.Floor(Mathf.Floor((worldPosition.z - 80f) % 40f) / 2f);
            return new Vector2(roomX, roomZ);
        }
    }

    // On collide
    private void OnCollisionEnter(Collision collision)
    {
        // If the enemy collides with the sword, then destroy the enemy
        if (health > 0)
        {
            if (collision.gameObject.tag == "BigSwordP")
            {
                Debug.Log("He recibido da�o");
                health -= 2;
            }
            if (collision.gameObject.tag == "LittleSwordP")
            {
                Debug.Log("He recibido da�o");
                health--;
            }
            if (health <= 0)
            {
                died = true;
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
            new List<int> { -1, 3, 5, 6, 13, 13 },  // -1 just to make easier the return
            new List<int> { 1, 2, 4, 11, 13, 13 },
            new List<int> { -1, 8, 9, 10, 13, 13 }, // -1 just to make easier the return
            new List<int> { 12 }
        };

        int roomX = (int)Mathf.Floor(worldPosition.x / 32f);
        if (roomX == 0 || roomX == 4) return roomIndexes[roomX][0];

        int roomZ = (int)Mathf.Floor(worldPosition.z / 20f);
        return roomIndexes[roomX][roomZ];
    }

    /**
     * When the player destroys part of the body of the boss, increase the speed of the boss && performs a special attack
     */
    public void bodyDestroyed(float newSpeed)
    {
        speed = newSpeed;
        shotSpeed += 0.25f;
        attackingFreq += 0.05f;
        // Special attack
        specialAttack();
    }
    /**
     * Stops the boss from moving (and it's body) and invokes the "performSpecialAttack" function after 1 second
     */
    private void specialAttack()
    {
        moving = false;
        performingSpecial = true;
        Invoke("performSpecialAttack", 1f);
        
    }
    /**
     * Instantiate one bullet for each direction and set the velocity of the bullet. Also invokes the "stopSpecialAttack" function after 0.3 seconds
     */
    private void performSpecialAttack()
    {
        GameObject newBulletBoss = Instantiate(bullet, frontalShotHolder.transform.position, Quaternion.identity);
        newBulletBoss.GetComponent<Rigidbody>().velocity = transform.forward * shotSpeed;

        GameObject newBulletBossLeft = Instantiate(bullet, leftShotHolder.transform.position, Quaternion.identity);
        newBulletBossLeft.GetComponent<Rigidbody>().velocity = (-transform.right + transform.forward * 4).normalized * shotSpeed;
        
        GameObject newBulletBossLeft2 = Instantiate(bullet, left2ShotHolder.transform.position, Quaternion.identity);
        newBulletBossLeft2.GetComponent<Rigidbody>().velocity = (-transform.right + transform.forward * 2).normalized * shotSpeed;

        GameObject newBulletBossLeft3 = Instantiate(bullet, left3ShotHolder.transform.position, Quaternion.identity);
        newBulletBossLeft3.GetComponent<Rigidbody>().velocity = (-transform.right).normalized * shotSpeed;

        GameObject newBulletBossRight = Instantiate(bullet, rightShotHolder.transform.position, Quaternion.identity);
        newBulletBossRight.GetComponent<Rigidbody>().velocity = (transform.right + transform.forward * 4).normalized * shotSpeed;

        GameObject newBulletBossRight2 = Instantiate(bullet, right2ShotHolder.transform.position, Quaternion.identity);
        newBulletBossRight2.GetComponent<Rigidbody>().velocity = (transform.right + transform.forward * 2).normalized * shotSpeed;

        GameObject newBulletBossRight3 = Instantiate(bullet, right3ShotHolder.transform.position, Quaternion.identity);
        newBulletBossRight3.GetComponent<Rigidbody>().velocity = (transform.right).normalized * shotSpeed;

        Invoke("stopSpecialAttack", 0.3f);
    }


    /**
     * Allows the boss and it's body parts to move again against the player
     */
    private void stopSpecialAttack()
    {
        performingSpecial = false;
        moving = true;
    }
}