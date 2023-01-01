using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public bool opened = false;
    public bool needKey = true;
    public bool needBossKey = false;

    GameObject player;

    enum s { CLOSED, OPENING, OPENED };
    private s state;
    private float transition;
    
    // Start is called before the first frame update
    void Start()
    {
        state = s.CLOSED;
        transition = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (opened && state == s.CLOSED) state = s.OPENING;

        if (state == s.OPENING)
        {
            if (transition < 90f)
            {
                transition += 2f;
                transform.Rotate(new Vector3(0f, -2f, 0f));
            }
            else
            {
                state = s.OPENED;
                if (player != null) player.GetComponent<PlayerMovement>().stop = false;
            }
        }
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (state == s.CLOSED && collision.gameObject.tag == "PlayerP" && needKey)
        {
            player = GameObject.FindWithTag("PlayerP");
            if (needBossKey)
            {
                if (player.GetComponent<PlayerBehaviour>().bossKeys > 0)
                {
                    state = s.OPENING;
                    player.GetComponent<PlayerBehaviour>().updateBossKeys(-1);
                    player.GetComponent<PlayerMovement>().stop = true;
                    GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                if (player.GetComponent<PlayerBehaviour>().keys > 0)
                {
                    state = s.OPENING;
                    player.GetComponent<PlayerBehaviour>().updateKeys(-1);
                    player.GetComponent<PlayerMovement>().stop = true;
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}
