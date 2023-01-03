using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossKeyDoor : MonoBehaviour
{
    public bool open = false;
    public bool close = false;
    public bool horizontal = false;
    public bool sound = true;

    enum s { CLOSED, OPENING, OPENED , CLOSING};
    private s state;
    private float transition;
    private int x, z; 

    // Start is called before the first frame update
    void Start()
    {
        state = s.CLOSED;
        transition = 0.0f;
        x = 0;
        z = 0;
        if (horizontal) x = 1;
        else z = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (open && state == s.CLOSED)
        {
            state = s.OPENING;
            open = false;
        } else if (close && state == s.OPENED)
        {
            state = s.CLOSING;
            close = false;
        }
        
        if (state == s.OPENING)
        {
            if (transition < 0.3f)
            {
                GameObject doorRight = gameObject.transform.GetChild(0).gameObject;
                GameObject doorLeft = gameObject.transform.GetChild(1).gameObject;
                doorRight.transform.position = doorRight.transform.position + new Vector3(transition*x, 0f, -transition*z);
                doorLeft.transform.position = doorLeft.transform.position + new Vector3(-transition*x, 0f, transition*z);
                transition += 0.02f;
            }
            else
            {
                state = s.OPENED;
                transition = 0f;
                if (sound) gameObject.GetComponent<AudioSource>().Play();
                else sound = true;
            }
        }
        else if (state == s.CLOSING)
        {
            if (transition < 0.3f)
            {
                GameObject doorRight = gameObject.transform.GetChild(0).gameObject;
                GameObject doorLeft = gameObject.transform.GetChild(1).gameObject;
                doorRight.transform.position = doorRight.transform.position + new Vector3(-transition*x, 0f, transition*z);
                doorLeft.transform.position = doorLeft.transform.position + new Vector3(transition*x, 0f, -transition*z);
                transition += 0.02f;
            } 
            else
            {
                state = s.CLOSED;
                transition = 0f;
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
}
