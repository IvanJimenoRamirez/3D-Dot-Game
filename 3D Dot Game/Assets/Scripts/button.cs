using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject chest;

    private enum s { NOPRESSED, TRANSITION, PRESSED }
    private s state;
    public bool pressed;
    public Material pressedMaterial;

    //position
    public int room;
    public string roomTag;
    //chest manage
    private bool hasChest;
    private bool hasKey;
    private Vector3 chestPosition;
    private Quaternion chestRotation;
    private bool chestShown;

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (state == s.NOPRESSED && collision.gameObject.tag == "PlayerP")
        {
            state = s.TRANSITION;
            pressed = true;

            //Change material color
            MeshRenderer meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
            Material[] materials = meshRenderer.materials;
            materials[0] = pressedMaterial;
            meshRenderer.materials = materials;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        state = s.NOPRESSED;
        pressed = false;
        chestShown = false;
        chestRotation = Quaternion.identity;
        switch (room)
        {
            case 1:
                hasChest = true;
                hasKey = true;
                chestPosition = new Vector3(84f, 1f, 8f);
                break;
            case 7:
                hasChest = true;
                hasKey = true;
                chestPosition = new Vector3(0f + 5f * 4f, 1f, 40f + 3f * 4f); 
                break;
            case 8:
                hasChest = false;
                hasKey = false;
                break;
            case 11:
                hasChest = false;
                hasKey = false;
                break;
            case 12:
                hasChest = false;
                hasKey = false;
                chestPosition = new Vector3(128f + 16f, 1f, 40f + 10f);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (pressed && state == s.NOPRESSED) state = s.TRANSITION;
        
        if (state == s.TRANSITION)
        {
            if (hasChest && !chestShown) //room 1 and 7
            {
                chestShown = true;
                GameObject newChest = Instantiate(chest, chestPosition, chestRotation);
                newChest.GetComponent<chest>().hasKey = hasKey;
                state = s.PRESSED;
            }
            else if (room == 8)
            {
                GameObject doorGate = GameObject.Find("door_gate_open(Clone)");
                doorGate.GetComponent<doorScript>().opened = true;
                state = s.PRESSED;
            }
            else if (room == 11)
            {
                GameObject bossKeyDoor = GameObject.FindWithTag("bossKeyDoor");
                bossKeyDoor.GetComponent<bossKeyDoor>().open = true;
                state = s.PRESSED;
            }
            else if (room == 12)
            {
                bool allPressed = true;
                GameObject[] btns = { };
                btns = GameObject.FindGameObjectsWithTag("btn");
                for (int i = 0; i < btns.Length; i++)
                {
                    GameObject btn = btns[i];
                    if (!btn.GetComponent<button>().pressed) allPressed = false;
                }
                
                if (allPressed)
                {
                    GameObject newChest = Instantiate(chest, chestPosition, chestRotation);
                    newChest.GetComponent<chest>().hasBossKey = true;
                }
                state = s.PRESSED;

            }
        }
    }
}
