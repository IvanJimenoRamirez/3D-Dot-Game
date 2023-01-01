using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public Vector3 velocityMask = new Vector3(0f, 0f, 0f);
    public Vector3 velocity = new Vector3(0f, 0f, 0f);
    bool impact;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(2f * velocityMask.x, 0f, 2f * velocityMask.z);
        transform.position = transform.position + velocity;
        velocity = new Vector3(0.25f * velocityMask.x, 0f, 0.25f * velocityMask.z);
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        impact = false;
        transform.position = transform.position + velocity;
        transform.Rotate(new Vector3(0f, 20f, 0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerP")
        {
            Destroy(gameObject);
        }
        else if (!impact)
        {
            velocity = new Vector3(-1f*velocity.x, 0f, -1f*velocity.z);
            impact = true;
        }
    }
}
