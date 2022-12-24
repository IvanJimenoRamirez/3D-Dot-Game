using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool move = false;
    public Vector3 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (move) transform.position = transform.position + velocity;
    }

    public void destroy()
    {
        //Destroy(gameObject, 3);
    }
}
