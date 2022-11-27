using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public bool closed;
    bool opened;
    float transition;
    
    // Start is called before the first frame update
    void Start()
    {
        closed = true;
        opened = false;
        transition = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!closed && !opened)
        {
            if (transition < 90f)
            {
                transition += 10f;
                transform.Rotate(new Vector3(0f, -10f, 0f));
            }
            else opened = true;
        }
    }
}
