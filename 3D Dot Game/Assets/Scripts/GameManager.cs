using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    public GameObject floor, wall, corner, split, wallEnd, wallDoor, button, player, column, crate, crateDark, chest, table, tableMedium, tableSmall, chair, barrel, mug, bookcase, bookcaseBroken, book, bookOpen;
    public GameObject wallGate, wallGateDoor, doorGate, scaffold, scaffoldLeft, scaffoldRight, scaffoldLowLeft, scaffoldLowRight, columnBroken, bossKeyDoor;
    public GameObject crab;
    private int roomX = 8;
    private int roomZ = 5;
    float q = 4.0f;


    struct Info
    {
        public Vector3 position;
        public Quaternion rotation;

        public Info (Vector3 pos, Quaternion rot)
        {
            this.position = pos;
            this.rotation = rot;
        }
    }
    private Dictionary<int, Dictionary<GameObject, ArrayList>> objects = new Dictionary<int, Dictionary<GameObject, ArrayList>>();
    private Dictionary<int, Dictionary<GameObject, ArrayList>> enemies = new Dictionary<int, Dictionary<GameObject, ArrayList>>();

    void objectPositions()
    {
        Dictionary<GameObject, ArrayList> room;
        ArrayList infos;
        float x, z;

        //SALA 1
        x = 16f * q; z = 0f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(1, room);


        //SALA 2
        x = 16f * q; z = 5f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //walls
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 2.7f, 0f, z + q * 1.5f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 6.7f, 0f, z + q * 1.5f + 4f), Quaternion.Euler(0f, 90f, 0f)));

        infos.Add(new Info(new Vector3(x + 2.7f + 26.7f, 0f, z + q * 1.5f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 4.7f + 20.7f, 0f, z + q * 1.5f + 4f), Quaternion.Euler(0f, 90f, 0f)));
        room.Add(wall, infos);

        //corners
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 6.7f, 0f, z + q * 1.5f), Quaternion.Euler(0f, -90f, 0f)));
        infos.Add(new Info(new Vector3(x + 4.7f + 20.7f, 0f, z + q * 1.5f), Quaternion.Euler(0f, 0f, 0f)));
        room.Add(corner, infos);

        objects.Add(2, room);


        //SALA 3
        x = 8f * q; z = 5f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //cratesDark
        infos = new ArrayList();
        for (int i = 0; i < 12; i++)
        {
            if (i != 5f && i != 6f) infos.Add(new Info(new Vector3(x + 5f + 2f * i, 2f, z + 5f), Quaternion.identity)); //horizontal down
        }
        for (int i = 0; i < 12; i++) infos.Add(new Info(new Vector3(x + 5f + 2f * i, 2f, z + 5f + 2f * 5f), Quaternion.identity)); //horizontal up
        for (int i = 0; i < 4; i++) infos.Add(new Info(new Vector3(x + 5f, 2f, z + 7f + 2f * i), Quaternion.identity)); //vertical left
        for (int i = 0; i < 4; i++) infos.Add(new Info(new Vector3(x + 5f + 2f * 11f, 2f, z + 7f + 2f * i), Quaternion.identity)); //vertical right
        room.Add(crateDark, infos);

        //crates
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 5f + 2f * 5, 2f, z + 5f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 5f + 2f * 6, 2f, z + 5f), Quaternion.identity));
        room.Add(crate, infos);

        //chest
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 5f + 2f * 5.5f, 1f, z + 5f + 2f * 2.5f), Quaternion.identity));
        room.Add(chest, infos);

        objects.Add(3, room);


        //SALA 4
        x = 16f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //tables
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 9f, 1f, z + 6.5f), Quaternion.identity)); //left - down
        infos.Add(new Info(new Vector3(x + 9f, 1f, z + 13.5f), Quaternion.identity)); //left - up
        infos.Add(new Info(new Vector3(x + 23f, 1f, z + 6.5f), Quaternion.identity)); //right - down
        infos.Add(new Info(new Vector3(x + 23f, 1f, z + 13.5f), Quaternion.identity)); //right - up
        room.Add(table, infos);

        objects.Add(4, room);


        //SALA 5
        x = 8f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //barrels
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 7f, 2f, z + 15f), Quaternion.Euler(-90f, 0f, 0f))); //left - up
        infos.Add(new Info(new Vector3(x + 9f, 2f, z + 15f), Quaternion.Euler(-90f, 0f, 0f)));
        infos.Add(new Info(new Vector3(x + 8f, 3.6f, z + 15f), Quaternion.Euler(-90f, 0f, 0f)));
        infos.Add(new Info(new Vector3(x + 8f, 2f, z + 10f), Quaternion.Euler(0f, 0f, 0f))); //left - mid
        infos.Add(new Info(new Vector3(x + 23f, 2f, z + 10f), Quaternion.Euler(-90f, -20f, 0f))); //right - mid
        infos.Add(new Info(new Vector3(x + 25f, 2f, z + 10f), Quaternion.Euler(0f, 0f, 0f)));
        room.Add(barrel, infos);

        //crates Dark
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 24f, 2f, z + 15f), Quaternion.Euler(0f, 0f, 0f))); //right - up
        infos.Add(new Info(new Vector3(x + 25f, 4f, z + 15f), Quaternion.Euler(0f, 20f, 0f)));
        room.Add(crateDark, infos);

        //tables
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 8f, 1f, z + 5f), Quaternion.Euler(0f, -1f, 0f))); //left - down
        infos.Add(new Info(new Vector3(x + 24f, 1f, z + 5f), Quaternion.Euler(0f, 0f, 0f))); //right - down
        room.Add(tableMedium, infos);

        //mugs
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 7f, 2.8f, z + 4.8f), Quaternion.Euler(0f, -8f, 0f))); //left table
        infos.Add(new Info(new Vector3(x + 9f, 2.7f, z + 5.2f), Quaternion.Euler(90f, -8f, 0f))); 
        infos.Add(new Info(new Vector3(x + 24f, 2.8f, z + 5f), Quaternion.Euler(0f, -90f, 0f))); //right table
        room.Add(mug, infos);

        objects.Add(5, room);


        //SALA 6
        x = 8f * q; z = 15f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //tables
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 12f, 1f, z + 6f), Quaternion.Euler(0f, 0f, 0f))); //left - down
        infos.Add(new Info(new Vector3(x + 6f, 1f, z + 12f), Quaternion.Euler(0f, -90f, 0f))); //left - up
        infos.Add(new Info(new Vector3(x + 20f, 1f, z + 6f), Quaternion.Euler(0f, -90f, 0f))); //right - down
        infos.Add(new Info(new Vector3(x + 26f, 1f, z + 12f), Quaternion.Euler(0f, 0f, 0f))); //right - up
        room.Add(tableSmall, infos);

        //bookcase
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 4f, 1f, z + 18f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 12f, 1f, z + 18f), Quaternion.Euler(0f, 180f, 0f)));
        infos.Add(new Info(new Vector3(x + 16f, 1f, z + 18f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 24f, 1f, z + 18f), Quaternion.Euler(0f, 180f, 0f)));
        infos.Add(new Info(new Vector3(x + 28f, 1f, z + 18f), Quaternion.identity));
        room.Add(bookcase, infos);

        //bookcaseBroken
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 8f, 1f, z + 18f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 20f, 1f, z + 18f), Quaternion.Euler(0f, 180f, 0f)));
        room.Add(bookcaseBroken, infos);

        //chairs
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 10f, 1f, z + 6f), Quaternion.Euler(0f, -90f, 0f))); //left - down
        infos.Add(new Info(new Vector3(x + 6f, 1f, z + 10f), Quaternion.Euler(0f, 200f, 0f))); //left - up
        infos.Add(new Info(new Vector3(x + 21.3f, 2f, z + 6f), Quaternion.Euler(90f, 100f, 0f))); //right - down
        infos.Add(new Info(new Vector3(x + 24f, 1f, z + 12f), Quaternion.Euler(0f, -60f, 0f))); //right - up
        room.Add(chair, infos);

        //book
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 6f, 2.6f, z + 12f), Quaternion.Euler(90f, 20f, 0f))); //left - up
        infos.Add(new Info(new Vector3(x + 6f, 2.9f, z + 12f), Quaternion.Euler(90f, 90f, 0f))); //left - up

        room.Add(book, infos);

        //bookOpen
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 20f, 2.4f, z + 6f), Quaternion.Euler(0f, 30f, 0f))); //right - down
        room.Add(bookOpen, infos);

        //chest
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 16f, 1f, z + 12f), Quaternion.identity));
        room.Add(chest, infos);

        objects.Add(6, room);


        //SALA 7
        x = 0f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //cratesDark
        infos = new ArrayList();
        for (int i = 0; i < 10; i++) infos.Add(new Info(new Vector3(x + 8f, 2f, z + 0.5f + 2f * i), Quaternion.identity));
        room.Add(crateDark, infos);

        objects.Add(7, room);

        //SALA 8
        x = 24f * q; z = 5f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //crates Dark
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 30f, 2f, z + 8f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 28f, 2f, z + 8f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 26f, 2f, z + 8f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 26f, 2f, z + 6f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 26f, 2f, z + 4f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 26f, 2f, z + 2f), Quaternion.identity));
        room.Add(crateDark, infos);

        //wallGateDoor
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 16f, 1f, z + 12.5f), Quaternion.identity));
        room.Add(wallGateDoor, infos);

        //doorGate
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 15f, 2.1f, z + 12.5f), Quaternion.Euler(0f, 180f, 0f)));
        room.Add(doorGate, infos);

        //wallGate
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 12f, 1f, z + 12.5f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 8f, 1f, z + 12.5f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 4f, 1f, z + 12.5f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 2f, 1f, z + 12.5f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 20f, 1f, z + 12.5f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 24f, 1f, z + 12.5f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 28f, 1f, z + 12.5f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 30f, 1f, z + 12.5f), Quaternion.identity));
        room.Add(wallGate, infos);


        objects.Add(8, room);


        //SALA 9
        x = 24f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //scaffold
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 8f, 1f, z + 8f), Quaternion.Euler(0f, -90f, 0f))); //left -vertical
        infos.Add(new Info(new Vector3(x + 8f, 1f, z + 10f), Quaternion.Euler(0f, -90f, 0f)));
        infos.Add(new Info(new Vector3(x + 8f, 1f, z + 12f), Quaternion.Euler(0f, -90f, 0f)));
        infos.Add(new Info(new Vector3(x + 10f, 1f, z + 14f), Quaternion.Euler(0f, 0f, 0f))); //left - up
        infos.Add(new Info(new Vector3(x + 10f, 1f, z + 6f), Quaternion.Euler(0f, 180f, 0f))); //left - down
        infos.Add(new Info(new Vector3(x + 24f, 1f, z + 8f), Quaternion.Euler(0f, 90f, 0f))); //right -vertical
        infos.Add(new Info(new Vector3(x + 24f, 1f, z + 10f), Quaternion.Euler(0f, 90f, 0f)));
        infos.Add(new Info(new Vector3(x + 24f, 1f, z + 12f), Quaternion.Euler(0f, 90f, 0f)));
        infos.Add(new Info(new Vector3(x + 22f, 1f, z + 14f), Quaternion.Euler(0f, 0f, 0f))); //right - up
        infos.Add(new Info(new Vector3(x + 22f, 1f, z + 6f), Quaternion.Euler(0f, 180f, 0f))); //right - down
        room.Add(scaffold, infos);

        //scaffold left
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 8f, 1f, z + 6f), Quaternion.Euler(0f, -90f, 0f))); //left - down
        infos.Add(new Info(new Vector3(x + 24f, 1f, z + 14f), Quaternion.Euler(0f, 90f, 0f))); //right - up
        room.Add(scaffoldLeft, infos);

        //scaffold right
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 8f, 1f, z + 14f), Quaternion.Euler(0f, -90f, 0f))); //left - up
        infos.Add(new Info(new Vector3(x + 24f, 1f, z + 6f), Quaternion.Euler(0f, 90f, 0f))); //right - down
        room.Add(scaffoldRight, infos);

        //crates Dark
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 23.5f, 4f, z + 6.5f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 16f, 2f, z + 11f), Quaternion.Euler(0f, 20f, 0f)));
        infos.Add(new Info(new Vector3(x + 15f, 2f, z + 9f), Quaternion.identity));
        room.Add(crateDark, infos);


        //barrels
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 17f, 2f, z + 9f), Quaternion.Euler(0f, 0f, 0f)));
        infos.Add(new Info(new Vector3(x + 8.5f, 4f, z + 13.5f), Quaternion.Euler(-90f, 20f, 0f)));
        room.Add(barrel, infos);


        objects.Add(9, room);


        //SALA 10
        x = 24f * q; z = 15f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //scaffoldLowLeft
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 7f, 1f, z + 12f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 11f, 1f, z + 8f), Quaternion.Euler(0f, 180f, 0f)));
        infos.Add(new Info(new Vector3(x + 21f, 1f, z + 12f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 25f, 1f, z + 8f), Quaternion.Euler(0f, 180f, 0f)));
        room.Add(scaffoldLowLeft, infos);

        //scaffoldLowRight
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 11f, 1f, z + 12f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 7f, 1f, z + 8f), Quaternion.Euler(0f, 180f, 0f)));
        infos.Add(new Info(new Vector3(x + 25f, 1f, z + 12f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 21f, 1f, z + 8f), Quaternion.Euler(0f, 180f, 0f)));
        room.Add(scaffoldLowRight, infos);

        //barrels
        infos = new ArrayList();
        for (int i = 0; i < 3; i++)
            for (int j  = 0; j < 3; j++)
                if (!(i == 2 && j == 2)) infos.Add(new Info(new Vector3(x + 7f + i*2f, 2f, z + 12f - j*2f), Quaternion.Euler(0f, 0f, 0f)));
        infos.Add(new Info(new Vector3(x + 7f + 4f, 2f, z + 12f - 4f), Quaternion.Euler(0f, 20f, -90f)));
        room.Add(barrel, infos);

        //crates Dark
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 21f, 2f, z + 11f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 24f, 2f, z + 11.5f), Quaternion.Euler(0f, 30f, 0f)));
        infos.Add(new Info(new Vector3(x + 21f, 2f, z + 8f), Quaternion.Euler(0f, -20f, 0f)));
        infos.Add(new Info(new Vector3(x + 24f, 2f, z + 8.5f), Quaternion.Euler(0f, 100f, 0f)));
        room.Add(crateDark, infos);

        objects.Add(10, room);


        //SALA 11
        x = 16f * q; z = 15f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(11, room);


        //SALA 12
        x = 32f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //columnBroken
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 5f, 1f, z + 3f), Quaternion.identity)); //left
        infos.Add(new Info(new Vector3(x + 5f, 1f, z + 17f), Quaternion.Euler(0f, 90f, 0f)));
        infos.Add(new Info(new Vector3(x + 27f, 1f, z + 3f), Quaternion.Euler(0f, -90f, 0f))); //right
        infos.Add(new Info(new Vector3(x + 27f, 1f, z + 17f), Quaternion.Euler(0f, 180f, 0f)));
        room.Add(columnBroken, infos);

        //tables
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 5f, 1f, z + 10f), Quaternion.Euler(0f, 90f, 0f))); //left
        infos.Add(new Info(new Vector3(x + 27f, 1f, z + 10f), Quaternion.Euler(0f,  90f, 0f))); //right
        infos.Add(new Info(new Vector3(x + 16f, 1f, z + 15f), Quaternion.identity)); //up
        infos.Add(new Info(new Vector3(x + 16f, 1f, z + 5f), Quaternion.identity)); //down
        room.Add(table, infos);

        objects.Add(12, room);


    }

    void enemiesPosition()
    {
        Dictionary<GameObject, ArrayList> room;
        ArrayList infos;
        float x, z;

        //SALA 1
        x = 16f * q; z = 0f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //crab
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 16f, 1f, z + 10f), Quaternion.Euler(0f, 90f, 0f))); //left
        room.Add(table, infos);

        objects.Add(1, room);


        //SALA 2
        x = 16f * q; z = 5f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(2, room);


        //SALA 3
        x = 8f * q; z = 5f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(3, room);


        //SALA 4
        x = 16f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(4, room);


        //SALA 5
        x = 8f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(5, room);


        //SALA 6
        x = 8f * q; z = 15f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(6, room);


        //SALA 7
        x = 0f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(7, room);

        //SALA 8
        x = 24f * q; z = 5f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(8, room);


        //SALA 9
        x = 24f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(9, room);


        //SALA 10
        x = 24f * q; z = 15f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(10, room);


        //SALA 11
        x = 16f * q; z = 15f * q;
        room = new Dictionary<GameObject, ArrayList>();

        objects.Add(11, room);


        //SALA 12
        x = 32f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();


        objects.Add(12, room);
    }

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
            if (left) Instantiate(wallEnd, new Vector3(q * x - 2f, 1.0f, q * z), Quaternion.Euler(0f, 180f, 0f));
            else if (right) Instantiate(wallEnd, new Vector3(q * x + 2f, 1.0f, q * z), Quaternion.identity);
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
            if (bottom) Instantiate(wallEnd, new Vector3(q * x, 1.0f, q * z - 0.2f), Quaternion.Euler(0f, 270f, 0f));
            else if (top) Instantiate(wallEnd, new Vector3(q * x, 1.0f, q * z + 2f), Quaternion.Euler(0f, -90f, 0f));
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
                //bossKeyDoor
                GameObject obj = Instantiate(bossKeyDoor, new Vector3(q * x, 1.5f, q * z + 2f), Quaternion.Euler(0f, 90f, 0f));
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
                if ((x == 0 && z == 10) || (x == 8 && z == 5) || (x == 16 && z == 0)) Instantiate(corner, new Vector3(q * x, 1.0f, q * z), Quaternion.identity);//left-bottom
                else if ((x == 0 && z == 15) || (x == 8 && z == 20)) Instantiate(corner, new Vector3(q * x, 1.0f, q * z), Quaternion.Euler(0f, 90, 0f)); //left-up
                else if ((x == 40 && z == 10) || (x == 32 && z == 5) || (x == 24 && z == 0)) Instantiate(corner, new Vector3(q * x, 1.0f, q * z), Quaternion.Euler(0f, -90, 0f)); //right-bottom
                else if ((x == 40 && z == 15) || (x == 32 && z == 20)) Instantiate(corner, new Vector3(q * x, 1.0f, q * z), Quaternion.Euler(0f, 180, 0f)); //right-up
                else
                {
                    //Doors
                    if (!addDoors(x, z))
                    {
                        //Walls
                        if (z % 5 == 0 && wallInScopeZ(x, z)) Instantiate(wall, new Vector3(q * x, 1.0f, q * z), Quaternion.identity); //horizontal                                                                                                                               
                        if (x % 8 == 0 && wallInScopeX(x, z)) Instantiate(wall, new Vector3(q * x, 1.0f, q * z), Quaternion.Euler(0f, 90f, 0f)); //vertical
                        if (z == 20 && (x == 16 || x == 24)) Instantiate(split, new Vector3(q * x, 1.0f, q * z), Quaternion.Euler(0f, 180f, 0f)); //split                                                                                                                               
                    }
                }

                //Floor
                if (floorInScope(x, z)) Instantiate(floor, new Vector3(q * x + 1.4f, 0.0f, q * z + 1.4f), Quaternion.identity); // floor
            }
        }
    }

    void initObjects()
    {
        foreach (Dictionary<GameObject, ArrayList> room in objects.Values)
        {
            foreach (KeyValuePair<GameObject, ArrayList> obj in room)
            {
                foreach (Info info in obj.Value)
                {
                    Instantiate(obj.Key, info.position, info.rotation);
                }
            }
        }
    }
    
    void initButtons()
    {
        float x, z;
        GameObject btn;

        //SALA 1
        x = 16f * q; z = 0f * q;
        btn = Instantiate(button, new Vector3(x + q * 2f, 1f, z + q * 2f), Quaternion.identity);

        //SALA 7
        x = 0f * q; z = 10f * q;
        btn = Instantiate(button, new Vector3(x + 1f, 3f, z + 10f), Quaternion.Euler(0f, 0f, -90f));
        btn.GetComponent<button>().room = 7;

        //SALA 8
        x = 24f * q; z = 5f * q;
        btn = Instantiate(button, new Vector3(x + 31f, 3f, z + 5f), Quaternion.Euler(0f, 0f, 90f));
        btn.GetComponent<button>().room = 8;

        //SALA 11
        x = 16f * q; z = 15f * q;
        btn = Instantiate(button, new Vector3(x + 16f, 1f, z + 10f), Quaternion.identity);
        btn.GetComponent<button>().room = 11;

        //SALA 12
        x = 32f * q; z = 10f * q;
        btn = Instantiate(button, new Vector3(x + 9f, 1f, z + 4f), Quaternion.identity); //left
        btn.GetComponent<button>().room = 12;
        btn.tag = "btn";

        btn = Instantiate(button, new Vector3(x + 9f, 1f, z + 16f), Quaternion.Euler(0f, 90f, 0f));
        btn.GetComponent<button>().room = 12;
        btn.tag = "btn";

        btn = Instantiate(button, new Vector3(x + 23f, 1f, z + 4f), Quaternion.Euler(0f, -90f, 0f)); //right
        btn.GetComponent<button>().room = 12;
        btn.tag = "btn";

        btn = Instantiate(button, new Vector3(x + 23f, 1f, z + 16f), Quaternion.Euler(0f, 180f, 0f));
        btn.GetComponent<button>().room = 12;
        btn.tag = "btn";
    }

    private void spawnPlayer()
    {
        Instantiate(player, new Vector3(80f, 1.0f, 10f), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPositions();
        floorAndWalls();
        initObjects();
        initButtons();
        spawnPlayer();

    }

    // Update is called once per frame
    void Update()
    {

    }

}
