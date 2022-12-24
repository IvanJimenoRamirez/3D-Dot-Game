using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion : MonoBehaviour
{
    public float maxHeight;
    public float speed;
    
    public float pos;
    private enum s { UP, DOWN };
    private s state;


    // Start is called before the first frame update
    void Start()
    {
        maxHeight = 1f;
        speed = 0.01f;

        pos = 0f;
        state = s.UP;        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        if (pos <= 0f)
        {
            state = s.UP;
        }
        else if (pos >= maxHeight)
        {
            state = s.DOWN;
        }

        if (state == s.UP)
        {
            pos += speed;
            transform.position = transform.position + new Vector3(0f, speed, 0f);
        }
        else if (state == s.DOWN)
        {
            pos -= speed;
            transform.position = transform.position - new Vector3(0f, speed, 0f);
        }
    }
}
