using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    public float rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        rotation = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, rotation, 0f));
    }
}
