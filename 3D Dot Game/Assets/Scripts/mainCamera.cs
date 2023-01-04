using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCamera : MonoBehaviour
{
    public GameObject gamePlayer, gameManager;
    public int roomActual;
    int roomPrevious;
    int transition;
    enum mov { LEFT, RIGHT, UP, DOWN, NONE};
    mov movement;
    bool upBoss, downBoss;

    void key()
    {
        for (int i = 0; i < 10; ++i)
        {
            if (Input.GetKeyDown("" + i))
            {
                roomActual = i;
            }
        }
    }

    void auxKey()
    {
        mov m = mov.NONE;

        if (Input.GetKey(KeyCode.UpArrow)) m = mov.UP;
        else if (Input.GetKey(KeyCode.DownArrow)) m = mov.DOWN;
        else if (Input.GetKey(KeyCode.RightArrow)) m = mov.RIGHT;
        else if (Input.GetKey(KeyCode.LeftArrow)) m = mov.LEFT;

        switch (roomPrevious)
        {
            case 1:
                if (m == mov.UP) roomActual = 2;
                break;
            case 2:
                if (m == mov.DOWN) roomActual = 1;
                else if (m == mov.LEFT) roomActual = 3;
                else if (m == mov.UP) roomActual = 4;
                else if (m == mov.RIGHT) roomActual = 8;
                break;
            case 3:
                if (m == mov.RIGHT) roomActual = 2;
                break;
            case 4:
                if (m == mov.DOWN) roomActual = 2;
                else if (m == mov.LEFT) roomActual = 5;
                break;
            case 5:
                if (m == mov.RIGHT) roomActual = 4;
                else if (m == mov.UP) roomActual = 6;
                else if (m == mov.LEFT) roomActual = 7;
                break;
            case 6:
                if (m == mov.DOWN) roomActual = 5;
                break;
            case 7:
                if (m == mov.RIGHT) roomActual = 5;
                break;
            case 8:
                if (m == mov.LEFT) roomActual = 2;
                else if (m == mov.UP) roomActual = 9;
                break;
            case 9:
                if (m == mov.DOWN) roomActual = 8;
                else if (m == mov.UP) roomActual = 10;
                else if (m == mov.RIGHT) roomActual = 12;
                break;
            case 10:
                if (m == mov.DOWN) roomActual = 9;
                else if (m == mov.LEFT) roomActual = 11;
                break;
            case 11:
                if (m == mov.RIGHT) roomActual = 10;
                else if (m == mov.UP) roomActual = 13;
                break;
            case 12:
                if (m == mov.LEFT) roomActual = 9;
                break;
            case 13:
                if (m == mov.DOWN) roomActual = 11;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Find the GameManager to instantiate mainCamera instance
        gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<GameManager>().gameMainCamera = gameObject;

        roomActual = roomPrevious = 1;
        transition = 0;

        upBoss = false;
        downBoss = false;
    }

    // Update is called once per frame
    void Update()
    {
        //key(); //TODO erase (testing)
        //auxKey(); //TODO erase (testing)


        if (roomActual != roomPrevious)
        {
            if (transition > 0)
            {
                if ((transition == 21 && (movement == mov.UP || movement == mov.DOWN)) || (transition == 33 && (movement == mov.LEFT || movement == mov.RIGHT)))
                {
                    //transition finishes
                    transition = 0;
                    roomPrevious = roomActual;
                    if (roomActual == 6)
                    {
                        GameObject roomDoor = GameObject.FindGameObjectWithTag("roomDoor");
                        roomDoor.GetComponent<bossKeyDoor>().close = true;
                    }
                    gamePlayer.GetComponent<PlayerMovement>().stop = false;
                    upBoss = false;
                    downBoss = false;
                }
                else
                {
                    switch (movement)
                    {
                        case mov.LEFT:
                            transform.Translate(new Vector3(-1f, 0f, 0f), Space.World);
                            break;
                        case mov.RIGHT:
                            transform.Translate(new Vector3(1f, 0f, 0f), Space.World);
                            break;
                        case mov.UP:
                            if (!upBoss)
                            {
                                transform.Translate(new Vector3(0f, 0f, 1f), Space.World);
                                if (roomActual == 6) gamePlayer.transform.Translate(new Vector3(0f, 0f, 0.05f), Space.World);
                            }
                            else
                            {
                                transform.Rotate(new Vector3(10f / 21f, 0f, 0f), Space.World);
                                transform.Translate(new Vector3(0f, 14f / 21f, 25f / 21f), Space.World);
                            }
                            break;
                        case mov.DOWN:
                            if (!downBoss) transform.Translate(new Vector3(0f, 0f, -1f), Space.World);
                            else
                            {
                                transform.Rotate(new Vector3(-10f / 21f, 0f, 0f), Space.World);
                                transform.Translate(new Vector3(0f, -14f / 21f, -25f / 21f), Space.World);
                            }
                            break;
                    }
                    transition++;
                }
            }
            else
            {
                movement = nextMovement();
                if (movement != mov.NONE)
                {
                    //transition starts
                    transition++;
                    gamePlayer.GetComponent<PlayerMovement>().stop = true;
                }
                else
                {
                    roomActual = roomPrevious;
                }
            }
        }
    }

    mov nextMovement()
    {
        switch (roomPrevious)
        {
            case 1:
                if (roomActual == 2) return mov.UP;
                break;
            case 2:
                if (roomActual == 1) return mov.DOWN;
                else if (roomActual == 3) return mov.LEFT;
                else if (roomActual == 4) return mov.UP;
                else if (roomActual == 8) return mov.RIGHT;
                break;
            case 3:
                if (roomActual == 2) return mov.RIGHT;
                break;
            case 4:
                if (roomActual == 2) return mov.DOWN;
                else if (roomActual == 5) return mov.LEFT;
                break;
            case 5:
                if (roomActual == 4) return mov.RIGHT;
                else if (roomActual == 6) return mov.UP;
                else if (roomActual == 7) return mov.LEFT;
                break;
            case 6:
                if (roomActual == 5) return mov.DOWN;
                break;
            case 7:
                if (roomActual == 5) return mov.RIGHT;
                break;
            case 8:
                if (roomActual == 2) return mov.LEFT;
                else if (roomActual == 9) return mov.UP;
                break;
            case 9:
                if (roomActual == 8) return mov.DOWN;
                else if (roomActual == 10) return mov.UP;
                else if (roomActual == 12) return mov.RIGHT;
                break;
            case 10:
                if (roomActual == 9) return mov.DOWN;
                else if (roomActual == 11) return mov.LEFT;
                break;
            case 11:
                if (roomActual == 10) return mov.RIGHT;
                else if (roomActual == 13)
                {
                    upBoss = true;
                    return mov.UP;
                }
                break;
            case 12:
                if (roomActual == 9) return mov.LEFT;
                break;
            case 13:
                if (roomActual == 11)
                {
                    downBoss = true;
                    return mov.DOWN;
                }
                break;
        }
        return mov.NONE;
    }
}
