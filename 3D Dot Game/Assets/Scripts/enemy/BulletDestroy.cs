using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    float timeReamining = 2f, timeElapsed;
    public bool destroyByTime = true;

    private void Start()
    {
        timeElapsed = 0f;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeReamining && destroyByTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag != "PlayerP" && collision.gameObject.tag != "Enemy")
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "bossBody" || collision.gameObject.tag == "PlayerP")
        {
            // if the bullet have the Explosion script
            if (gameObject.GetComponent<Explosion>() != null)
            {
                gameObject.GetComponent<Explosion>().exploded = true;
            }
            else Destroy(gameObject);
        }
    }
}
