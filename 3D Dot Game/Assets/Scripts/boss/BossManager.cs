using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    float timeToAttack;
    public float attackingFreq = 0.7f;

    // AI variables
    Transform player;
    PathFinding pathFinding;
    List<Node> path;

    BossBody head;
    BossBody[] tail;

    // Start is called before the first frame update
    void Start()
    {
        // Set the time to attack
        timeToAttack = 1.0f / attackingFreq;
        // Set the player transform
        GameObject p = GameObject.FindGameObjectWithTag("PlayerP");
        if (p != null) player = p.transform;

        //Set up the bossbody
        GameObject head = GameObject.Find("Boss").GetComponent<Transform>().GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
