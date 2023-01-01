using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag != "PlayerP" && collision.gameObject.tag != "Enemy")
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "bossBody" || collision.gameObject.tag == "PlayerP")
        {
            // if the bullet have the Explosion script
            if (GetComponent<Explosion>() != null)
            {
                GetComponent<Explosion>().exploded = true;
            }
            else Destroy(gameObject);
        }
    }
}
