using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int health = 9;
    public int maxHealth = 10;
    public bool hasKey = false;
    public bool hasBoomerang = true;
    private GameObject activeBoomerang;

    public GameObject rightHand, littleSword, bigSword, boomerang;

    // Public attributes for the player's UI
    public Canvas heartIcons;
    public GameObject emptyHeart, filledHeart;

    //Ticks per second
    public float ticksPerSecond = 1f;
    float timeToTick;

    // Start is called before the first frame update
    void Start()
    {
        // Find the canvas from the scene hierarchy
        heartIcons = GameObject.Find("HeartUI").GetComponent<Canvas>();
        // Add the bigSword to the rightHand as the first child
        //Instantiate(bigSword, rightHand.transform);
        // Instantiate the heart icons
        playerUI();
        
        timeToTick = 1f / ticksPerSecond;
    }

    private void setUpSwordColliders()
    {
        rightHand.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (timeToTick > 0) timeToTick -= Time.deltaTime;

        boomerangManage();

        // If the player is dead then destroy the player
        if (health <= 0)
        {
            Invoke("destroyPlayer", 1.5f);
        }

        if (transform.position.y > 1f || transform.position.y < .99f)
        {
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }
        /*
        // Change the sword to the little one
        if (health < maxHealth && rightHand.transform.GetChild(0).gameObject.name != "littleSword(Clone)")
        {
            Destroy(rightHand.transform.GetChild(0).gameObject);
            Instantiate(littleSword, rightHand.transform);
            setUpSwordColliders();
        }
        // If the player has the maxHealth && the sword in the right hand is the little one, then replace it with the big one
        if (health == maxHealth && rightHand.transform.GetChild(0).gameObject.name != "bigSword(Clone)")
        {
            Destroy(rightHand.transform.GetChild(0).gameObject);
            Instantiate(bigSword, rightHand.transform);
            setUpSwordColliders();
        }
        */
    }
    void destroyPlayer()
    {
        Destroy(gameObject);
    }

    void playerUI()
    {
        // Update the canva to show the player's health
        heartIcons.GetComponent<Canvas>().enabled = true;
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < health)
            {
                filledHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(28 * (i + 1), -28);
                Instantiate(filledHeart, heartIcons.transform);
            }
            else
            {
                emptyHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(28 * (i + 1), -28);
                Instantiate(emptyHeart, heartIcons.transform);
            }
        }
    }

    /*
     * This method is called when the player collides with an enemy
     */
    private void OnCollisionEnter(Collision collision)
    {
        // If the player collides with an enemy or a enemy bullet, then take damage
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "bossBullet") && health > 0)
        {
            if (timeToTick <= 0f)
            {
                health--;
                changeHeartSprite(true);
                timeToTick = 1f / ticksPerSecond;

                // TODO: start the "TakeDamage" animation
            }

            if (health == 0)
            {
                GetComponent<Animator>().SetBool("dead", true);
            }
            if (collision.gameObject.tag == "EnemyBullet")
            {
                collision.gameObject.GetComponent<Explosion>().exploded = true;
                //Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "EnemyBullet")
            {
                Destroy(collision.gameObject);
            }
        }
        // If the player collides with a health pickup, then heal
        if (collision.gameObject.tag == "HealthPickup")
        {
            health++;
            changeHeartSprite(false);
            // Change the heart icon in the "health" position to the filled heart
            Destroy(collision.gameObject);
        }

        // If the player collides with a wall, then stop moving
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Collided with a wall");
            //Refactor
            GetComponent<PlayerMovement>().stopMoving();
        }

    }

    public void changeHeartSprite(bool takeDmg)
    {
        // Change the heart icon in the "health" position to the empty heart
        if (takeDmg) heartIcons.transform.GetChild(health + 1).GetComponent<UnityEngine.UI.Image>().sprite = emptyHeart.GetComponent<UnityEngine.UI.Image>().sprite;
        else heartIcons.transform.GetChild(health).GetComponent<UnityEngine.UI.Image>().sprite = filledHeart.GetComponent<UnityEngine.UI.Image>().sprite;
    }

    private void boomerangManage()
    {
        if ((Input.GetKeyDown("space")) && hasBoomerang && activeBoomerang==null)
        {

            float rotY = transform.rotation.eulerAngles.y;
            Vector3 velocityMask = new Vector3(0f, 0f, 0f);

            if ((337.5 <= rotY && rotY < 360)|| (0 <= rotY && rotY < 22.5)) velocityMask = new Vector3(0f, 0f, 1f);
            else if (22.5 <= rotY && rotY < 67.5) velocityMask = new Vector3(1f, 0f, 1f);
            else if (67.5 <= rotY && rotY < 112.5) velocityMask = new Vector3(1f, 0f, 0f);
            else if (112.5 <= rotY && rotY < 157.5) velocityMask = new Vector3(1f, 0f, -1f);
            else if (157.5 <= rotY && rotY < 202.5) velocityMask = new Vector3(0f, 0f, -1f);
            else if (202.5 <= rotY && rotY < 247.5) velocityMask = new Vector3(-1f, 0f, -1f);
            else if (247.5 <= rotY && rotY < 292.5) velocityMask = new Vector3(-1f, 0f, 0f);
            else if (292.5 <= rotY && rotY < 337.5) velocityMask = new Vector3(-1f, 0f, 1f);
            
            Quaternion rot = transform.rotation;
            Vector3 pos = transform.position;
            pos.y -= 1.5f;
            activeBoomerang = Instantiate(boomerang, pos, Quaternion.identity);
            activeBoomerang.GetComponent<Boomerang>().velocityMask = velocityMask;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidBody = hit.collider.attachedRigidbody;

        if (rigidBody != null)
        {
            Vector3 forceDir = hit.gameObject.transform.position - transform.position;
            forceDir.y = 0;
            forceDir.Normalize();
            rigidBody.AddForceAtPosition(forceDir * 1, transform.position, ForceMode.Impulse);
        }
    }
}
