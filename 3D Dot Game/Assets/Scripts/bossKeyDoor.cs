using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossKeyDoor : MonoBehaviour
{
    public bool open = false;
    
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
        if (open && state == s.CLOSED) state = s.OPENING;
        
        if (state == s.OPENING)
        {
            if (transition < 1f)
            {
                GameObject doorRight = gameObject.transform.GetChild(0).gameObject;
                GameObject doorLeft = gameObject.transform.GetChild(1).gameObject;
                doorRight.transform.position = doorRight.transform.position + new Vector3(0f, 0f, -transition);
                doorLeft.transform.position = doorLeft.transform.position + new Vector3(0f, 0f, transition);
                transition += 0.02f;
            }
            else state = s.OPENED;
        }
    }
}
