using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public bool pressed = false;
    public GameObject chest;
    
    //chest manage
    private Vector3 chestPosition = new Vector3(84f, 1f, 8f);
    private Quaternion chestRotation = Quaternion.identity;
    private bool chestShown = false;



    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
              //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (!pressed && collision.gameObject.tag == "Player")
        {
            pressed = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (pressed == true)
        {
            if (!chestShown)
            {
                chestShown = true;
                Instantiate(chest, chestPosition, chestRotation);
            }
        }
    }
}
