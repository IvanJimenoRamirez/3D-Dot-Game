using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float scaleSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("timeOut", 2f);   
    }

    private void Update()
    {
        // Scale the bullet
        transform.localScale += new Vector3(scaleSpeed * Time.deltaTime, scaleSpeed * Time.deltaTime, scaleSpeed * Time.deltaTime);
    }

    private void timeOut()
    {
        Destroy(gameObject);
    }
}
