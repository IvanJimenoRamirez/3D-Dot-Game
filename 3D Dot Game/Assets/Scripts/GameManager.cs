using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject floor, wall, corner, split, wallEnd, wallDoor, button;
    private int roomX = 8;
    private int roomZ = 5;
    float q = 4.0f;

    bool wallInScopeX(float x, float z)
    {
        switch (x)
        {
            case 0f:
            case 40f:
                return z >= 10f && z <= 15f;
                break;
            case 8f:
            case 32f:
                return z >= 5f && z <= 20f;
                break;
            case 16f:
            case 24f:
                return z >= 0f && z < 20f;
                break;
        }
        return false;
    }

    bool wallInScopeZ(float x, float z)
    {
        switch (z)
        {
            case 0.0f:
                return x >= 16f && x <= 24f;
                break;
            case 5.0f:
            case 20.0f:
                return x >= 8f && x <= 32f;
                break;
            case 10.0f:
            case 15.0f:
                return x >= 0f && x <= 40f;
                break;
        }
        return false;
    }

    bool floorInScope(float x, float z)
    {
        if ((x >= 8f && x < 32f) && (z >= 5f && z < 20f)) return true;
        if ((x >= 0f && x < 40f) && (z >= 10f && z < 15f)) return true;
        if ((x >= 16f && x < 24f) && (z >= 0f && z <= 5f)) return true;
        return false;
    }

    bool addDoors(float x, float z)
    {

        //horizontal opened
        bool horizontalOpened = false;

        if (z == 10 && (x >= 19 && x <= 21)) horizontalOpened = true;
        else if (z == 15 && (x >= 11 && x <= 13)) horizontalOpened = true;
        else if (z == 15 && (x >= 27 && x <= 29)) horizontalOpened = true;

        if (horizontalOpened)
        {
            bool left = (x == 19 || x == 11 || x == 27);
            bool right = (x == 21 || x == 13 || x == 29);
            if (left) Instantiate(wallEnd, new Vector3(q * x - 2f, 0.0f, q * z), Quaternion.Euler(0f, 180f, 0f));
            else if (right) Instantiate(wallEnd, new Vector3(q * x + 2f, 0.0f, q * z), Quaternion.identity);
        }


        //vertical opened
        bool verticalOpened = false;

        if (x == 8 && (z >= 12 && z <= 13)) verticalOpened = true;
        else if (x == 16 && (z >= 7 && z <= 8)) verticalOpened = true;
        else if (x == 24 && (z >= 7 && z <= 8)) verticalOpened = true;
        else if (x == 24 && (z >= 17 && z <= 18)) verticalOpened = true;
        else if (x == 16 && (z >= 12 && z <= 13)) verticalOpened = true; //vertical closed
        else if (x == 32 && (z >= 12 && z <= 13)) verticalOpened = true; //vertical closed

        if (verticalOpened)
        {
            bool bottom = (z == 12 || z == 7 || z == 17);
            bool top = (z == 13 || z == 8 || z == 18);
            if (bottom) Instantiate(wallEnd, new Vector3(q * x, 0.0f, q * z - 0.2f), Quaternion.Euler(0f, 270f, 0f));
            else if (top) Instantiate(wallEnd, new Vector3(q * x, 0.0f, q * z + 2f), Quaternion.Euler(0f, -90f, 0f));
        }


        //horizontal closed
        bool horizontalClosed = false;
        if (z == 5 && x == 20) horizontalClosed = true;
        else if (z == 20 && x == 20) horizontalClosed = true;
        else if (z == 10 && x == 28) horizontalClosed = true;

        if (horizontalClosed)
        {
            GameObject obj = Instantiate(wallDoor, new Vector3(q * x - 2.0f, 1.0f, q * z), Quaternion.Euler(0f, 180f, 0f));
            obj.transform.localScale += new Vector3(1f, 0.3f, 0f);
        }

        //vertical closed
        bool verticalClosed = false;
        if (x == 16 && z == 12) verticalClosed = true;
        else if (x == 32 && z == 12) verticalClosed = true;

        if (verticalClosed)
        {

            if (x == 16)
            {
                GameObject obj = Instantiate(wallDoor, new Vector3(q * x, 1.0f, q * z - 0.2f), Quaternion.Euler(0f, 90f, 0f));
                obj.transform.localScale += new Vector3(1f, 0.3f, 0f);
            }
            else if (x == 32)
            {
                GameObject obj = Instantiate(wallDoor, new Vector3(q * x, 1.0f, q * z + 3.8f), Quaternion.Euler(0f, 270f, 0f));
                obj.transform.localScale += new Vector3(1f, 0.3f, 0f);
            }
        }

        return horizontalOpened || verticalOpened || horizontalClosed || verticalClosed;
    }

    void floorAndWalls()
    {
        for (float x = 0; x <= 40; x++)
        {
            for (float z = 0; z <= 20; z++)
            {
                //Corner
                if ((x == 0 && z == 10) || (x == 8 && z == 5) || (x == 16 && z == 0)) Instantiate(corner, new Vector3(q * x, 0.0f, q * z), Quaternion.identity);//left-bottom
                else if ((x == 0 && z == 15) || (x == 8 && z == 20)) Instantiate(corner, new Vector3(q * x, 0.0f, q * z), Quaternion.Euler(0f, 90, 0f)); //left-up
                else if ((x == 40 && z == 10) || (x == 32 && z == 5) || (x == 24 && z == 0)) Instantiate(corner, new Vector3(q * x, 0.0f, q * z), Quaternion.Euler(0f, -90, 0f)); //right-bottom
                else if ((x == 40 && z == 15) || (x == 32 && z == 20)) Instantiate(corner, new Vector3(q * x, 0.0f, q * z), Quaternion.Euler(0f, 180, 0f)); //right-up
                else
                {
                    //Doors
                    if (!addDoors(x, z))
                    {
                        //Walls
                        if (z % 5 == 0 && wallInScopeZ(x, z)) Instantiate(wall, new Vector3(q * x, 0.0f, q * z), Quaternion.identity); //horizontal                                                                                                                               
                        if (x % 8 == 0 && wallInScopeX(x, z)) Instantiate(wall, new Vector3(q * x, 0.0f, q * z), Quaternion.Euler(0f, 90f, 0f)); //vertical
                        if (z == 20 && (x == 16 || x == 24)) Instantiate(split, new Vector3(q * x, 0.0f, q * z), Quaternion.Euler(0f, 180f, 0f)); //split                                                                                                                               
                    }
                }

                //Floor
                if (floorInScope(x, z)) Instantiate(floor, new Vector3(q * x + 1.4f, 0.0f, q * z + 1.4f), Quaternion.identity); // floor
            }
        }
    }

    void initScene1()
    {
        for (float x = 16; x < 24; ++x)
        {
            for (float z = 0; z < 5; ++z)
            {
                if (x == 18 && z == 2)
                {
                    Instantiate(button, new Vector3(q * x, 1.0f, q * z), Quaternion.identity);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        floorAndWalls();
        initScene1();

    }

    // Update is called once per frame
    void Update()
    {

    }

}
