using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool move = false;
    public Vector3 velocity;

    private bool audio;
    
    // Start is called before the first frame update
    void Start()
    {
        audio = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position = transform.position + velocity;
            if (!audio)
            {
                audio = true;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    public void destroy()
    {
        
    }
}
