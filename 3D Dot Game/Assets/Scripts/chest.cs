using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public GameObject key, keyCanvas, bossKey, boomerang;
    public bool hasKey;
    public bool hasBossKey;
    public bool hasBoomerang;

    public AudioClip appear;
    public bool appearSound = true;

    public enum s { LOCK, UNLOCK, OPEN, OPEN_FINISHED }
    public s state = s.LOCK;

    private float transition = 0f, gainTransition = 0f;
    private GameObject gain;

    
    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (state == s.LOCK && collision.gameObject.tag == "PlayerP")
        {
            state = s.UNLOCK;
            GetComponent<AudioSource>().Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (appearSound) GetComponent<AudioSource>().PlayOneShot(appear);

        Vector3 position = transform.position + new Vector3(0f, 1f, 0f);
        if (hasKey) gain = Instantiate(key, position, Quaternion.identity);
        else if (hasBossKey)
        {
            position.y += 0.12f;
            gain = Instantiate(bossKey, position, Quaternion.identity);
        }
        else if (hasBoomerang)
        {
            position.y -= 2f;
            gain = Instantiate(boomerang, position, Quaternion.identity);
            gain.GetComponent<AudioSource>().volume = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == s.UNLOCK)
        {
            if (transition < 90f)
            {
                GameObject child = gameObject.transform.GetChild(0).gameObject;
                child.transform.Rotate(new Vector3(-100f * Time.deltaTime, 0f, 0f));
                transition += 100f * Time.deltaTime;
            }
            else
            {
                state = s.OPEN;
            }
        }
        else if (state == s.OPEN)
        {
            if (gainTransition < 420f) gainAnimation();
            else
            {
                Destroy(gain);
                GameObject player = GameObject.FindWithTag("PlayerP");
                if (hasKey) player.GetComponent<PlayerBehaviour>().updateKeys(1);
                else if (hasBossKey) player.GetComponent<PlayerBehaviour>().updateBossKeys(1);
                else if (hasBoomerang) player.GetComponent<PlayerBehaviour>().getBoomerang();
                state = s.OPEN_FINISHED;
            }
        }
    }

    void gainAnimation()
    {
        // Pujada
        if (gainTransition < 0.1f)
        {
            Vector3 newPos = gain.transform.position;
            newPos.y = newPos.y + 0.5f * Time.deltaTime;
            gain.transform.position = newPos;
            gainTransition += 0.5f * Time.deltaTime;
        }
        //Gir
        else if (gainTransition >= 0.1f && gainTransition < 400f)
        {
            gain.transform.Rotate(new Vector3(0f, 360f * Time.deltaTime, 0f));
            gainTransition += Time.deltaTime * 360f;
        }
        //Pujada + gir
        else { 
            Vector3 newPos = gain.transform.position;
            newPos.y = newPos.y + 10f * Time.deltaTime;
            gain.transform.position = newPos;
            gain.transform.Rotate(new Vector3(0f, 360f * Time.deltaTime, 0f));
            gainTransition += Time.deltaTime * 10;
        }
    }
}
