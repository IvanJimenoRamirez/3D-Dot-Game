using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public float growingSize = 0.01f;
    public float maxScale = 0.1f;
    float grow;
    bool growing;
    
    // Start is called before the first frame update
    void Start()
    {
        growing = true;
        grow = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (growing && grow < maxScale)
        {
            grow += growingSize;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + growingSize, transform.localScale.z);
        }
        else
        {
            growing = false;
            grow -= growingSize;
            if (grow > 0f)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - growingSize, transform.localScale.z);
            }
        }
    }

    // On collision
    void OnTriggerEnter(Collider other)
    {
        growing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        growing = false;
    }
}
