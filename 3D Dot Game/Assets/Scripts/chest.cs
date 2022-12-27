using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class chest : MonoBehaviour
{
    public GameObject key, keyCanvas;
    public bool hasKey;
    public bool hasBossKey;

    public enum s { LOCK, UNLOCK, OPEN }
    public s state = s.LOCK;

    private int transition = 0;
    
    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if ((state != s.UNLOCK) && collision.gameObject.tag == "PlayerP")
        {
            state = s.UNLOCK;
            if (hasKey)
            {
                GameObject player = GameObject.FindWithTag("PlayerP");
                player.GetComponent<PlayerBehaviour>().updateKeys(1);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = transform.position + new Vector3(0f, 1f, 0f);
        Instantiate(key, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if ((state == s.UNLOCK) && (state != s.OPEN))
        {
            if (transition < 90)
            {
                GameObject child = gameObject.transform.GetChild(0).gameObject;
                child.transform.Rotate(new Vector3(-5f, 0f, 0f));
                transition += 5;
            }
            else
            {
                state = s.OPEN;
                if (hasKey)
                {
                    Canvas canvas = GameObject.Find("HeartUI").GetComponent<Canvas>();
                    keyCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, -180);
                    Instantiate(keyCanvas, canvas.transform);
                }
            }
        }
    }
}
