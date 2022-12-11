using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag != "PlayerP" && collision.gameObject.tag != "Enemy")
        if (collision.gameObject.tag == "Wall")
        {
            GetComponent<Explosion>().exploded = true;
            //Destroy(gameObject);
        }
    }
}
