using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    
    public void stopMoving()
    {
        GetComponent<Animator>().SetBool("moving", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isAttacking = GetComponent<Animator>().GetBool("attacking");
        bool isDead = GetComponent<Animator>().GetBool("dead");
        // If the player is not attacking and the animation attack has ended then move
        if (!isAttacking && GetComponent<PlayerAttacking>().getTimeToAttack() <= 0 && !isDead)
        {
            bool isMoving = false;
            // Move the player in the direction of the input and rotate it to face the direction of the input. Taking into account the diagonal character rotation
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (!isMoving) transform.Translate(Vector3.forward * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (!isMoving) transform.Translate(Vector3.forward * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 270, 0);
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (!isMoving) transform.Translate(Vector3.forward * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                isMoving = true;
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(0, 315, 0);
            }
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.Euler(0, 45, 0);
            }
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Euler(0, 225, 0);
            }
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.Euler(0, 135, 0);
            }

            

            // modify the animator to set the "moving" parameter to true if the player is moving and false otherwise
            // Obtain the moving property
            bool playerAlreadyMoving = GetComponent<Animator>().GetBool("moving");
            if (isMoving && !playerAlreadyMoving) GetComponent<Animator>().SetBool("moving", isMoving);
            if (!isMoving && playerAlreadyMoving) GetComponent<Animator>().SetBool("moving", isMoving);
        }
        else
        {
            // modify the animator to set the "moving" parameter to false
            GetComponent<Animator>().SetBool("moving", false);
        }
    }
}
