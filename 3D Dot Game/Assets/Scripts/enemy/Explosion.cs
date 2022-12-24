using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public bool exploded = false;
    public Material explosionMat;
    public GameObject coin, potion;
    public bool getReward = true;
    public float time = 3;
    
    public float cubeSize = 0.15f;
    public int cubesInRow = 5;
    public float explosionForce = 50f;
    public float explsionRadius = 4f;
    public float explosionUpward = 0.4f;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    ArrayList pieces = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        //Calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use thos value to create pivot vector
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (exploded) explode();
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Plane") explode();
    }
    */

    public void explode()
    {
        // Disappear box
        gameObject.SetActive(false);

        if (getReward) reward();

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++)
            for (int y = 0; y < cubesInRow; y++)
                for (int z = 0; z < cubesInRow; z++)
                    createPiece(x, y, z);

        //add explosion force to all colliders in that overlap sphere
        foreach(GameObject piece in pieces)
        {
            //get rigid body from collider object
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explsionRadius, explosionUpward);

            }
            Destroy(piece.gameObject, time);
        }

        Destroy(gameObject);
    }

    void createPiece(int x, int y, int z)
    {
        //Create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigid boy and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;

        //set material
        piece.GetComponent<MeshRenderer>().material = explosionMat;

        pieces.Add(piece);
    }

    void reward()
    {
        // 40% of probability to get a reward
        bool reward = Random.Range(0.0f, 100.0f) <= 40f ? true: false;

        if (reward)
        {
            // 50% of propability to get a coin or a potion
            bool coinReward = Random.Range(0.0f, 100.0f) <= 50;

            // get object position
            Vector3 pos = transform.position;

            //Instantiate reward
            if (coinReward) Instantiate(coin, new Vector3(pos.x, 2.5f, pos.z), Quaternion.identity);
            else Instantiate(potion, new Vector3(pos.x, 1.5f, pos.z), Quaternion.identity);
        }
    }
}
