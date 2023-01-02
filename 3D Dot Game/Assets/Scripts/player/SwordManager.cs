using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    // Properties form the sword animation
    float timeReamining;
    GameObject edge;
    // Enumerator to know the state of the sword (the size, initially it's XXL)
    enum swordState { S, L, XXL  };
    swordState state;

    Transform player;

    //Special attack
    bool madeSpecial;
    enum specialDirection { LEFT, RIGHT };
    specialDirection comboDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Startup the properties for the sword animation
        timeReamining = 0.0f;
        edge = transform.GetChild(0).gameObject;
        state = swordState.XXL;

        madeSpecial = false;

        // Get the transform of the player
        player = GameObject.Find("player(Clone)").transform;
    }

    // Update is called once per frame
    void Update() { 
        if (!madeSpecial) swordAnimation();
        comboAttack();
        moveSword();
    }


    /*
     * Checks if the animation can start
     */
    private void swordAnimation()
    {
        timeReamining += Time.deltaTime;
        if (timeReamining >= 0.09f)
        {
            timeReamining = 0f;
            // Get the first child of the object
            changeState();
        }
        if (state == swordState.XXL)
        {
            //transform.position = transform.position + new Vector3(0.0005f, 0, 0);
        }
        else if (state == swordState.S)
        {
            //transform.position = transform.position - new Vector3(0.0005f, 0, 0);
        }
    }

    /*
     * In case the user press the key 'W', 'S', 'D' or 'A' the sword will perform a slash to the left or to the right
     */
    private void comboAttack()
    {
        // In case the special attack is running, rotate the sword to the desired direction
        if (madeSpecial)
        {
            switch (comboDirection)
            {
                case specialDirection.LEFT:
                    transform.Rotate(0, 0, 8);
                    player.Rotate(0, -8, 0);
                    break;
                case specialDirection.RIGHT:
                    transform.Rotate(0, 0, -8);
                    player.Rotate(0, 8, 0);
                    break;
            }
        }
        // In case the special have not been made, 
        else if (state > swordState.S)
        {
            // check if the user pressed a key
            if (Input.GetKey(KeyCode.A))
            {
                // Rotate the sword to the left
                comboDirection = specialDirection.LEFT;
                madeSpecial = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                // Rotate the sword to the right
                comboDirection = specialDirection.RIGHT;
                madeSpecial = true;
            }
        }
    }

    private void moveSword()
    {
        float x, z, playerRotation = player.rotation.eulerAngles.y;
        if (playerRotation < 60f || playerRotation > 300f) z = 0.1f;
        else z = -0.1f;

        if (playerRotation < 150) x = 0.1f;
        else x = -0.1f;
        
        if ((playerRotation >= 0 && playerRotation <= 1) || (playerRotation >= 179f && playerRotation <= 181f))
        {
            x = 0f;
        }
        else if ((playerRotation >= 89f && playerRotation <= 91f) || (playerRotation >= 269 && playerRotation <= 271))
        {
            z = 0f;
        }
        else
        {
            x /= 2f;
            z /= 2f;
        }
        transform.position = transform.position + new Vector3(x, 0, z);
    }

    /*
     * Change the state of the sword (and the scale && position properties for the animation)
     */
    private void changeState ()
    {
        switch (state)
        {
            case swordState.XXL:
                state = swordState.L;
                edge.transform.localScale = new Vector3(1f, 2.5f, 1f);
                edge.transform.localPosition = new Vector3(0f, -1.35f, 0f);
                break;
            case swordState.L:
                state = swordState.S;
                edge.transform.localScale = new Vector3(1f, 1.5f, 1f);
                edge.transform.localPosition = new Vector3(0f, -0.5f, 0f);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }
}
