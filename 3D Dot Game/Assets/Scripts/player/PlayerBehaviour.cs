using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int health = 9;
    public int maxHealth = 10;
    public int keys = 0, bossKeys = 0, coins = 0;
    public bool hasBoomerang = false;
    private GameObject activeBoomerang, UIbigSword, UIlittleSword;

    public GameObject rightHand, littleSword, bigSword, boomerang;

    // Public attributes for the player's UI
    public Canvas UIcanvas;
    public GameObject emptyHeart, filledHeart;

    //Ticks per second
    public float ticksPerSecond = 1f;
    float timeToTick;

    // Start is called before the first frame update
    void Start()
    {
        //Find the GameManager and the mainCamera to instantiate player instance
        GameObject.Find("GameManager").GetComponent<GameManager>().gamePlayer = gameObject;
        GameObject.Find("Main Camera").GetComponent<mainCamera>().gamePlayer = gameObject;

        // Find the canvas from the scene hierarchy
        UIcanvas = GameObject.Find("HeartUI").GetComponent<Canvas>();
        // Add the bigSword to the rightHand as the first child
        //Instantiate(bigSword, rightHand.transform);
        // Instantiate the heart icons
        playerUI();
        
        timeToTick = 1f / ticksPerSecond;
    }


    // Update is called once per frame
    void Update()
    {

        // Time to tick update
        if (timeToTick > 0) timeToTick -= Time.deltaTime;
        
        // If the player is dead then destroy the player
        if (health <= 0) Invoke("destroyPlayer", 1.5f);

        GodModeKeys();

        boomerangManage();

        lockY();
    }

    /* COLLISIONS */

    // This method is called when the player collides
    private void OnCollisionEnter(Collision collision)
    {
        // If the player collides with an enemy or a enemy bullet, then take damage
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "bossBullet") && health > 0)
        {
            if (timeToTick <= 0f)
            {
                health--;
                if (health == 9) {
                    UIbigSword.SetActive(false);
                    UIlittleSword.SetActive(true);
                }
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
            if (health >= 10)
            {
                health = 10;
                UIbigSword.SetActive(true);
                UIlittleSword.SetActive(false);
            }
            changeHeartSprite(false);
            // Change the heart icon in the "health" position to the filled heart
            Destroy(collision.gameObject);
        }

        // If the player collides with a coin
        if (collision.gameObject.tag == "Coin")
        {
            updateCoins(1);
            Destroy(collision.gameObject);
        }

        // If the player collides with a wall, then stop moving
        if (collision.gameObject.tag == "Wall")
        {
            //Refactor
            GetComponent<PlayerMovement>().stopMoving();
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

    /* PLAYER */

    private void setUpSwordColliders()
    {
        rightHand.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void destroyPlayer()
    {
        Destroy(gameObject);
    }

    private void lockY()
    {
        if (transform.position.y > 1f || transform.position.y < .99f)
        {
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }
    }
    
    private void GodModeKeys()
    {
        // K -> get key
        if (Input.GetKeyDown(KeyCode.K)) updateKeys(1);

        // B -> get Boss key
        if (Input.GetKeyDown(KeyCode.B)) updateBossKeys(1);

        // G -> invulnerability
    }

    /* UI */

    // Initialization
    private void playerUI()
    {
        // Update the canva to show the player's health
        UIcanvas.GetComponent<Canvas>().enabled = true;
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < health)
            {
                filledHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(28 * (i + 1), -28);
                Instantiate(filledHeart, UIcanvas.transform.Find("Hearts").transform);
            }
            else
            {
                emptyHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(28 * (i + 1), -28);
                Instantiate(emptyHeart, UIcanvas.transform.Find("Hearts").transform);
            }
        }

        // Hide Boomerang
        GameObject boomerangCanva = UIcanvas.transform.Find("Boomerang").gameObject;
        GameObject boomerang = boomerangCanva.transform.Find("UIboomerang").gameObject;
        boomerang.SetActive(false);

        //Find UI Swords
        UIbigSword = UIcanvas.transform.Find("Sword").gameObject.transform.Find("UIbigSword").gameObject;
        UIlittleSword = UIcanvas.transform.Find("Sword").gameObject.transform.Find("UIlittleSword").gameObject;
        UIlittleSword.SetActive(false);
    }

    // Update UI hearts
    public void changeHeartSprite(bool takeDmg)
    {
        // Change the heart icon in the "health" position to the empty heart
        if (takeDmg) UIcanvas.transform.Find("Hearts").transform.GetChild(health).GetComponent<UnityEngine.UI.Image>().sprite = emptyHeart.GetComponent<UnityEngine.UI.Image>().sprite;
        else UIcanvas.transform.Find("Hearts").transform.GetChild(health).GetComponent<UnityEngine.UI.Image>().sprite = filledHeart.GetComponent<UnityEngine.UI.Image>().sprite;
    }

    // Update UI Keys
    public void updateKeys(int change)
    {
        keys += change;
        if (keys < 0) keys = 0;
        GameObject keysCanva = UIcanvas.transform.Find("Keys").gameObject;
        TextMeshProUGUI text = keysCanva.transform.Find("Keys-counter").GetComponent<TextMeshProUGUI>();
        text.SetText("x"+keys.ToString());
    }

    // Update UI Boss Keys
    public void updateBossKeys(int change)
    {
        bossKeys += change;
        if (keys < 0) keys = 0;
        GameObject keysCanva = UIcanvas.transform.Find("BossKey").gameObject;
        TextMeshProUGUI text = keysCanva.transform.Find("Keys-counter").GetComponent<TextMeshProUGUI>();
        text.SetText("x" + bossKeys.ToString());
    }

    // Update UI Coins
    public void updateCoins(int change)
    {
        coins += change;
        if (keys < 0) keys = 0;
        GameObject coinsCanva = UIcanvas.transform.Find("Coins").gameObject;
        TextMeshProUGUI text = coinsCanva.transform.Find("Coins-counter").GetComponent<TextMeshProUGUI>();
        text.SetText("x" + coins.ToString());
    }

    // Update UI Boomerang
    private void updateBoomerang()
    {
        // Show Boomerang
        GameObject boomerangCanva = UIcanvas.transform.Find("Boomerang").gameObject;
        GameObject boomerang = boomerangCanva.transform.Find("UIboomerang").gameObject;
        boomerang.SetActive(true);
    }

    /* BOOMERANG */

    public void getBoomerang()
    {
        hasBoomerang = true;

        // Add boomerang in the UI
        updateBoomerang();
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
        else if (Input.GetKeyDown("space") && !hasBoomerang)
        {
            getBoomerang();
        }
    }

}
