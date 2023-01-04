using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class archer : MonoBehaviour
{
    public GameObject arrow;
    public int position = -1; //[0]down-left  [1]down-right  [2]up-left  [3] up-right
    public float velocity = 50f;
    
    enum s { POS_1, TRANS_1_2, TRANS_2_1, POS_2 };
    s state;
    float rotate;
    float rotation;

    bool posTreated = false;
    bool archerInit = false;
    public GameObject actArrow;
    Vector3 vel1, vel2;

    [SerializeField] private float _time = 2f;


    // Start is called before the first frame update
    void Start()
    {
        state = s.TRANS_1_2;
        rotate = 0.8f; //0.1f; // rotation velocity
        rotation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!archerInit && position != -2) init();
        else
        {
            rotationUpdate();
        }
    }

    void newArrow()
    {
        Vector3 pos = transform.position;
        actArrow = Instantiate(arrow, new Vector3(pos.x, 2.6f, pos.z), transform.rotation, transform);
    }

    void init()
    {
        newArrow();

        switch (position)
        {
            case 0:
                vel1 = new Vector3(0f, 0f, velocity);
                vel2 = new Vector3(velocity, 0f, 0f);   
                break;
            case 1:
                vel1 = new Vector3(-velocity, 0f, 0f);
                vel2 = new Vector3(0f, 0f, velocity);
                transform.Rotate(new Vector3(0f, 270f, 0f));
                break;
            case 2:
                vel1 = new Vector3(velocity, 0f, 0f);
                vel2 = new Vector3(0f, 0f, -velocity);
                transform.Rotate(new Vector3(0f, 90f, 0f));
                break;
            case 3:
                vel1 = new Vector3(0f, 0f, -velocity);
                vel2 = new Vector3(-velocity, 0f, 0f);
                transform.Rotate(new Vector3(0f, 180f, 0f));
                break;
        }
        archerInit = true;
    }

    void rotationUpdate()
    {
        if (state == s.POS_1 && !posTreated)
        {
            //state = s.TRANS_1_2;
            posTreated = true;
            pos1();
            Invoke("afterPos1", _time);
        }
        else if (state == s.POS_2 && !posTreated)
        {
            //state = s.TRANS_2_1;
            posTreated = true;
            pos2();
            Invoke("afterPos2", _time);
        }
        else if (state == s.TRANS_1_2)
        {
            if (rotation >= 90f) state = s.POS_2;
            else
            {
                rotation += rotate;
                transform.Rotate(new Vector3(0f, rotate, 0f));
            }
        }
        else if (state == s.TRANS_2_1)
        {
            if (rotation <= 0f) state = s.POS_1;
            else
            {
                rotation -= rotate;
                transform.Rotate(new Vector3(0f, -rotate, 0f));
            }
        }
    }

    void pos1()
    {
        if (actArrow != null)
        {
            actArrow.GetComponent<Arrow>().velocity = vel1;
            actArrow.GetComponent<Arrow>().move = true;
        }
    }

    void pos2()
    {
        if (actArrow != null)
        {
            actArrow.GetComponent<Arrow>().velocity = vel2;
            actArrow.GetComponent<Arrow>().move = true;
        }
    }

    void afterPos1()
    {
        state = s.TRANS_1_2;
        posTreated = false;
        newArrow();
    }

    void afterPos2()
    {
        state = s.TRANS_2_1;
        posTreated = false;
        newArrow();
    }
}
