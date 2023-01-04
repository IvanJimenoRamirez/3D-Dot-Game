using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int room;

    public GameObject gamePlayer, gameMainCamera;
    public List<List<Vector2>> collisions = new List<List<Vector2>>()
    {
        new List<Vector2>(){},   // 0 - No collisions at the moment
        new List<Vector2>(){},   // 1 - No collisions at the moment       
        new List<Vector2>(){
            
            new Vector2(0, 3),
            new Vector2(1, 3),
            new Vector2(2, 3),
            new Vector2(2, 4),
            new Vector2(3, 3),
            new Vector2(3, 4),
            new Vector2(3, 5),
            new Vector2(3, 6),


            new Vector2(13, 6),
            new Vector2(13, 5),
            new Vector2(13, 4),
            new Vector2(13, 4),
            new Vector2(13, 3),
            new Vector2(14, 3),
            new Vector2(15, 3),
            new Vector2(16, 3),

        },  // 2
        new List<Vector2>() {
            new Vector2(2,2),
            new Vector2(2,3),
            new Vector2(2,4),
            new Vector2(2,5),
            new Vector2(2,6),
            new Vector2(2,7),

            new Vector2(3,2),
            new Vector2(4,2),
            new Vector2(5,2),
            new Vector2(6,2),
            new Vector2(9,2),
            new Vector2(10,2),
            new Vector2(11,2),
            new Vector2(12,2),
            new Vector2(13,2),
            
            new Vector2(13,3),
            new Vector2(13,4),
            new Vector2(13,5),
            new Vector2(13,6),
            new Vector2(13,7),

            new Vector2(3,7),
            new Vector2(4,7),
            new Vector2(5,7),
            new Vector2(6,7),
            new Vector2(9,7),
            new Vector2(10,7),
            new Vector2(11,7),
            new Vector2(12,7),
        },  // 3
        new List<Vector2>(){
            new Vector2(3, 2),
            new Vector2(3, 3),
            new Vector2(3, 4),
            new Vector2(3, 6),
            new Vector2(3, 7),
            
            new Vector2(4, 2),
            new Vector2(4, 3),
            new Vector2(4, 4),
            new Vector2(4, 6),
            new Vector2(4, 7),
            
            new Vector2(5, 2),
            new Vector2(5, 3),
            new Vector2(5, 4),
            new Vector2(5, 6),
            new Vector2(5, 7),

            new Vector2(10, 2),
            new Vector2(10, 3),
            new Vector2(10, 4),
            new Vector2(10, 6),
            new Vector2(10, 7),

            new Vector2(11, 2),
            new Vector2(11, 3),
            new Vector2(11, 4),
            new Vector2(11, 6),
            new Vector2(11, 7),

            new Vector2(12, 2),
            new Vector2(12, 3),
            new Vector2(12, 4),
            new Vector2(12, 6),
            new Vector2(12, 7),
        },  // 4
        new List<Vector2>(){
        
            //Left table
            new Vector2(3,2),
            new Vector2(3,3),
            new Vector2(4,2),
            new Vector2(4,3),
            new Vector2(5,2),
            new Vector2(5,3),

            //Right table
            new Vector2(11,2),
            new Vector2(11,3),
            new Vector2(12,2),
            new Vector2(12,3),
            new Vector2(13,2),
            new Vector2(13,3),

            // Middle left
            new Vector2(4,5),

            // Middle right
            new Vector2(11,4),
            new Vector2(12,4),
            new Vector2(13,4),
            new Vector2(11,5),
            new Vector2(12,5),
            new Vector2(13,5),

            // Top left
            new Vector2(3,7),
            new Vector2(4,7),
            new Vector2(5,7),
            new Vector2(3,8),
            new Vector2(4,8),
            new Vector2(5,8),

            // Top right
            new Vector2(11,7),
            new Vector2(11,8),
            new Vector2(12,7),
            new Vector2(12,8),
            new Vector2(13,7),
            new Vector2(13,8),
        },  // 5
        new List<Vector2>(){
            new Vector2(2,5),
            new Vector2(2,6),
            new Vector2(3,5),
            new Vector2(3,6),

            new Vector2(4,2),
            new Vector2(4,3),
            new Vector2(5,2),
            new Vector2(5,3),
            new Vector2(6,2),
            new Vector2(6,3),

            new Vector2(9,2),
            new Vector2(9,3),
            new Vector2(10,2),
            new Vector2(10,3),
            new Vector2(11,2),
            new Vector2(11,3),

            new Vector2(11,5),
            new Vector2(11,6),
            new Vector2(12,5),
            new Vector2(12,6),
            new Vector2(13,5),
            new Vector2(13,6),
        },  // 6
        new List<Vector2>(){
            new Vector2(4,0),
            new Vector2(4,1),
            new Vector2(4,2),
            new Vector2(4,3),
            new Vector2(4,4),
            new Vector2(4,5),
            new Vector2(4,6),
            new Vector2(4,7),
            new Vector2(4,8),
            new Vector2(4,9),
        },  // 7
        new List<Vector2>(){
            new Vector2(0,6),
            new Vector2(1,6),
            new Vector2(2,6),
            new Vector2(3,6),
            new Vector2(4,6),
            new Vector2(5,6),
            new Vector2(6,6),
            new Vector2(7,6),
            new Vector2(8,6),
            new Vector2(9,6),
            new Vector2(10,6),
            new Vector2(11,6),
            new Vector2(12,6),
            new Vector2(13,6),
            new Vector2(14,6),
            new Vector2(15,6),

            new Vector2(13,4),
            new Vector2(14,4),
            new Vector2(15,4),
            new Vector2(13,3),
            new Vector2(14,3),
            new Vector2(15,3),
            new Vector2(13,2),
            new Vector2(14,2),
            new Vector2(15,2),
            new Vector2(13,1),
            new Vector2(14,1),
            new Vector2(15,1),
            new Vector2(13,0),
            new Vector2(14,0),
            new Vector2(15,0),
        },  // 8 
        new List<Vector2>(){},   // 9 - No collisions at the moment
        new List<Vector2>(){
            new Vector2(2,3),
            new Vector2(2,4),
            new Vector2(2,5),
            new Vector2(2,6),
            new Vector2(2,7),

            new Vector2(3,3),
            new Vector2(3,4),
            new Vector2(3,5),
            new Vector2(3,6),
            new Vector2(3,7),

            new Vector2(4,3),
            new Vector2(4,4),
            new Vector2(4,5),
            new Vector2(4,6),
            new Vector2(4,7),

            new Vector2(5,3),
            new Vector2(5,4),
            new Vector2(5,5),
            new Vector2(5,6),
            new Vector2(5,7),

            new Vector2(6,3),
            new Vector2(6,4),
            new Vector2(6,5),
            new Vector2(6,6),
            new Vector2(6,7),

            new Vector2(9,3),
            new Vector2(9,4),
            new Vector2(9,5),
            new Vector2(9,6),
            new Vector2(9,7),

            new Vector2(10,3),
            new Vector2(10,4),
            new Vector2(10,5),
            new Vector2(10,6),
            new Vector2(10,7),

            new Vector2(11,3),
            new Vector2(11,4),
            new Vector2(11,5),
            new Vector2(11,6),
            new Vector2(11,7),

            new Vector2(12,3),
            new Vector2(12,4),
            new Vector2(12,5),
            new Vector2(12,6),
            new Vector2(12,7),

            new Vector2(13,3),
            new Vector2(13,4),
            new Vector2(13,5),
            new Vector2(13,6),
            new Vector2(13,7),
        },  // 10
        new List<Vector2>(){
            new Vector2(7,5),
            new Vector2(8,5),
            new Vector2(8,4),
        },  // 11
        new List<Vector2>(){
            new Vector2(2,1),
            
            new Vector2(2,3),
            new Vector2(2,4),
            new Vector2(2,5),
            new Vector2(2,6),
            new Vector2(3,3),
            new Vector2(3,4),
            new Vector2(3,5),
            new Vector2(3,6),

            new Vector2(2,8),
            
            new Vector2(4,2),
            new Vector2(4,8),

            new Vector2(7,2),
            new Vector2(7,3),
            new Vector2(8,2),
            new Vector2(8,3),
            new Vector2(9,2),
            new Vector2(9,3),

            new Vector2(7,7),
            new Vector2(7,8),
            new Vector2(8,7),
            new Vector2(8,8),
            new Vector2(9,7),
            new Vector2(9,8),

            new Vector2(11,2),
            new Vector2(11,8),

            new Vector2(13,1),
            
            new Vector2(13,3),
            new Vector2(13,4),
            new Vector2(13,5),
            new Vector2(13,6),
            new Vector2(14,3),
            new Vector2(14,4),
            new Vector2(14,5),
            new Vector2(14,6),

            new Vector2(13,8),

        },  // 12 
        new List<Vector2>(){},   // 13 - No collisions at the moment
    };

    public GameObject floor, wall, corner, split, wallEnd, wallDoor, wallDoorBoss, button, player, column, crate, crateDark, chest, table, tableMedium, tableSmall, chair, barrel, mug, bookcase, bookcaseBroken, book, bookOpen;
    public GameObject wallGate, wallGateDoor, doorGate, scaffold, scaffoldLeft, scaffoldRight, scaffoldLowLeft, scaffoldLowRight, columnBroken, bossKeyDoor;
    public GameObject crab, bolb, archer, skeleton, bat, boss, potion, coin, cratePlatformSmall, cratePlatformMedium, cratePlatformBig;
    public GameObject torch, torchLight, torchFire;

    private List<GameObject> room4archers = new List<GameObject>(), room9archers = new List<GameObject>();
    
    float q = 4.0f;

    struct Info
    {
        public Vector3 position;
        public Quaternion rotation;

        public Info(Vector3 pos, Quaternion rot)
        {
            this.position = pos;
            this.rotation = rot;
        }
    }
    private Dictionary<int, Dictionary<GameObject, ArrayList>> objects = new Dictionary<int, Dictionary<GameObject, ArrayList>>();
    private Dictionary<int, Dictionary<GameObject, ArrayList>> enemies = new Dictionary<int, Dictionary<GameObject, ArrayList>>(); // room < enemy, info >

    private ArrayList activeEnemies;

    private bool room6Chest = false;
    public bool checkRoom6 = false;
    
    void objectPositions()
    {
        Dictionary<GameObject, ArrayList> room;
        ArrayList infos;
        float x, z;

        //SALA 1
        x = 16f * q; z = 0f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //torchs light
        torchInstantiate(x, z, ref room);
        
        objects.Add(1, room);

        //SALA 2
        x = 16f * q; z = 5f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //torchs light
        torchInstantiate(x, z, ref room);

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

        //torchs light
        torchInstantiate(x, z, ref room);

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
        GameObject chest3 = Instantiate(chest, new Vector3(x + 5f + 2f * 5.5f, 1f, z + 5f + 2f * 2.5f), Quaternion.identity);
        chest3.GetComponent<chest>().hasKey = true;
        chest3.GetComponent<chest>().appearSound = false;

        objects.Add(3, room);


        //SALA 4
        x = 16f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //torchs light
        torchInstantiate(x, z, ref room);

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

        //torchs light
        torchInstantiate(x, z, ref room);

        //barrels
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 7f, 2f, z + 15f), Quaternion.Euler(-90f, 0f, 0f))); //left - up
        infos.Add(new Info(new Vector3(x + 9f, 2f, z + 15f), Quaternion.Euler(-90f, 0f, 0f)));
        infos.Add(new Info(new Vector3(x + 8f, 3.6f, z + 15f), Quaternion.Euler(-90f, 0f, 0f)));
        infos.Add(new Info(new Vector3(x + 8.3f, 2f, z + 10.3f), Quaternion.Euler(0f, 0f, 0f))); //left - mid
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

        //torchs light
        torchInstantiate(x, z, ref room);

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

        //door
        GameObject roomDoor6 = Instantiate(bossKeyDoor, new Vector3(x + 16f, 1f, z + 0.2f), Quaternion.identity);
        roomDoor6.gameObject.tag = "roomDoor";
        roomDoor6.transform.localScale += new Vector3(1f, 0.3f, 0f);
        roomDoor6.GetComponent<bossKeyDoor>().horizontal = true;
        roomDoor6.GetComponent<bossKeyDoor>().open = true;
        roomDoor6.GetComponent<bossKeyDoor>().sound = false;

        objects.Add(6, room);


        //SALA 7
        x = 0f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //torchs light
        torchInstantiate(x, z, ref room);

        //cratePlatformSmall
        infos = new ArrayList();
        for (int i = 0; i < 10; i++)
            if (i == 5 || i == 4)
                infos.Add(new Info(new Vector3(x + 8f, 1f, z + 0.5f + 2f * i), Quaternion.identity));
        room.Add(cratePlatformSmall, infos);

        //cratePlatformMedium
        infos = new ArrayList();
        for (int i = 0; i < 10; i++)
            if (i == 2 || i == 3 || i == 6 || i == 7)
                infos.Add(new Info(new Vector3(x + 8f, 1f, z + 0.5f + 2f * i), Quaternion.identity));
        room.Add(cratePlatformMedium, infos);

        //cratePlatformBig
        infos = new ArrayList();
        for (int i = 0; i < 10; i++)
            if (i == 0 || i == 1 || i == 8 || i == 9)
                infos.Add(new Info(new Vector3(x + 8f, 1f, z + 0.5f + 2f * i), Quaternion.identity));
        room.Add(cratePlatformBig, infos);

        objects.Add(7, room);

        //SALA 8
        x = 24f * q; z = 5f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //torchs light
        torchInstantiate(x, z, ref room);

        //cratesPlatformMedium
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 30f, 1f, z + 8f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 28f, 1f, z + 8f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 26f, 1f, z + 8f), Quaternion.identity));
        room.Add(cratePlatformMedium, infos);

        //cratesPlatformSmall
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 26f, 1f, z + 6f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 26f, 1f, z + 4f), Quaternion.identity));
        infos.Add(new Info(new Vector3(x + 26f, 1f, z + 2f), Quaternion.identity));
        room.Add(cratePlatformSmall, infos);

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

        //torchs light
        torchInstantiate(x, z, ref room);

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

        //torchs light
        torchInstantiate(x, z, ref room);

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
            for (int j = 0; j < 3; j++)
                if (!(i == 2 && j == 2)) infos.Add(new Info(new Vector3(x + 7f + i * 2f, 2f, z + 12f - j * 2f), Quaternion.Euler(0f, 0f, 0f)));
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

        //torchs light
        torchInstantiate(x, z, ref room);

        objects.Add(11, room);


        //SALA 12
        x = 32f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //torchs light
        torchInstantiate(x, z, ref room);

        //columnBroken
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 4.4f, 1f, z + 2.4f), Quaternion.identity)); //left-down
        infos.Add(new Info(new Vector3(x + 4.4f, 1f, z + 16.3f), Quaternion.Euler(0f, 90f, 0f))); //left-up
        infos.Add(new Info(new Vector3(x + 26.4f, 1f, z + 2.4f), Quaternion.Euler(0f, -90f, 0f))); //right-down
        infos.Add(new Info(new Vector3(x + 26.4f, 1f, z + 16.3f), Quaternion.Euler(0f, 180f, 0f))); //right-up
        room.Add(columnBroken, infos);

        //tables
        infos = new ArrayList();
        infos.Add(new Info(new Vector3(x + 5.2f, 1f, z + 9.7f), Quaternion.Euler(0f, 90f, 0f))); //left
        infos.Add(new Info(new Vector3(x + 27.2f, 1f, z + 9.7f), Quaternion.Euler(0f, 90f, 0f))); //right
        infos.Add(new Info(new Vector3(x + 15.7f, 1f, z + 15.4f), Quaternion.identity)); //up
        infos.Add(new Info(new Vector3(x + 15.7f, 1f, z + 5.4f), Quaternion.identity)); //down
        room.Add(table, infos);

        objects.Add(12, room);


        //SALA 13
        x = 12f * q; z = 20f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //torchs light
        torchInstantiate(x, z, ref room);

        objects.Add(13, room);
    }

    void torchInstantiate(float x, float z, ref Dictionary<GameObject, ArrayList> room)
    {
        ArrayList infos, torchInfos, torchLightInfos, torchFireInfos;

        if (!(x == 12f * 4f && z == 20f * 4)) { 
            
            //torch
            infos = new ArrayList();
            if (!(x == 8f * 4f && z == 15f * 4f))
            {
                infos.Add(new Info(new Vector3(x + 8f, 3f, z + 19.3f), Quaternion.Euler(0f, 180f, 0f))); //horizontal up
                infos.Add(new Info(new Vector3(x + 24f, 3f, z + 19.3f), Quaternion.Euler(0f, 180f, 0f)));
            }
            infos.Add(new Info(new Vector3(x + 8f, 3f, z + 0.7f), Quaternion.identity)); //horizontal down
            infos.Add(new Info(new Vector3(x + 24f, 3f, z + 0.7f), Quaternion.identity));
            infos.Add(new Info(new Vector3(x + 0.7f, 3f, z + 7f), Quaternion.Euler(0f, 90f, 0f))); //side left
            infos.Add(new Info(new Vector3(x + 0.7f, 3f, z + 13f), Quaternion.Euler(0f, 90f, 0f))); 
            infos.Add(new Info(new Vector3(x + 31.3f, 3f, z + 7f), Quaternion.Euler(0f, -90f, 0f))); //side right
            infos.Add(new Info(new Vector3(x + 31.3f, 3f, z + 13f), Quaternion.Euler(0f, -90f, 0f))); 
            room.Add(torch, infos);

            //torchLight
            infos = new ArrayList();
            if (!(x == 8f * 4f && z == 15f * 4f))
            {
                infos.Add(new Info(new Vector3(x + 8f, 4f, z + 18.3f), Quaternion.Euler(0f, 180f, 0f))); //horizontal up
                infos.Add(new Info(new Vector3(x + 24f, 4f, z + 18.3f), Quaternion.Euler(0f, 180f, 0f)));
            }
            infos.Add(new Info(new Vector3(x + 8f, 4f, z + 1.7f), Quaternion.identity)); //horizontal down
            infos.Add(new Info(new Vector3(x + 24f, 4f, z + 1.7f), Quaternion.identity));
            infos.Add(new Info(new Vector3(x + 1.7f, 4f, z + 7f), Quaternion.Euler(0f, 90f, 0f))); //side left
            infos.Add(new Info(new Vector3(x + 1.7f, 4f, z + 13f), Quaternion.Euler(0f, 90f, 0f))); 
            infos.Add(new Info(new Vector3(x + 30.3f, 4f, z + 7f), Quaternion.Euler(0f, -90f, 0f)));//side right
            infos.Add(new Info(new Vector3(x + 30.3f, 4f, z + 13f), Quaternion.Euler(0f, -90f, 0f)));

            room.Add(torchLight, infos);

            //torchFire
            infos = new ArrayList();
            if (!(x == 8f * 4f && z == 15f * 4f))
            {
                infos.Add(new Info(new Vector3(x + 8f, 3.7f, z + 18.7f), Quaternion.Euler(0f, 180f, 0f))); //horizontal up
                infos.Add(new Info(new Vector3(x + 24f, 3.7f, z + 18.7f), Quaternion.Euler(0f, 180f, 0f)));
            }
            infos.Add(new Info(new Vector3(x + 8f, 3.7f, z + 1.3f), Quaternion.identity)); //horizontal down
            infos.Add(new Info(new Vector3(x + 24f, 3.7f, z + 1.3f), Quaternion.identity));
            infos.Add(new Info(new Vector3(x + 1.3f, 3.7f, z + 7f), Quaternion.Euler(0f, 90f, 0f))); //side left
            infos.Add(new Info(new Vector3(x + 1.3f, 3.7f, z + 13f), Quaternion.Euler(0f, 90f, 0f)));
            infos.Add(new Info(new Vector3(x + 30.7f, 3.7f, z + 7f), Quaternion.Euler(0f, -90f, 0f))); //side right
            infos.Add(new Info(new Vector3(x + 30.7f, 3.7f, z + 13f), Quaternion.Euler(0f, -90f, 0f)));
            room.Add(torchFire, infos);
        
        }
        else
        {
            // boss room
            torchInfos = new ArrayList();
            torchLightInfos = new ArrayList();
            torchFireInfos = new ArrayList();

            for (int i = 1; i <= 4; i++)
            {
                //torch
                torchInfos.Add(new Info(new Vector3(x + 12.8f * i, 3f, z + 35.3f), Quaternion.Euler(0f, 180f, 0f))); //horizontal up
                torchInfos.Add(new Info(new Vector3(x + 12.8f * i, 3f, z + 0.7f), Quaternion.identity)); //horizontal down
                torchInfos.Add(new Info(new Vector3(x + 0.7f, 3f, z + 7.2f * i), Quaternion.Euler(0f, 90f, 0f))); //side left
                torchInfos.Add(new Info(new Vector3(x + 63.3f, 3f, z + 7.2f * i), Quaternion.Euler(0f, -90f, 0f))); //side right

                //torchLight
                torchLightInfos.Add(new Info(new Vector3(x + 12.8f * i, 4f, z + 34.3f), Quaternion.Euler(0f, 180f, 0f))); //horizontal up
                torchLightInfos.Add(new Info(new Vector3(x + 12.8f * i, 4f, z + 1.7f), Quaternion.identity)); //horizontal down
                torchLightInfos.Add(new Info(new Vector3(x + 1.7f, 4f, z + 7.2f * i), Quaternion.Euler(0f, 90f, 0f))); //side left
                torchLightInfos.Add(new Info(new Vector3(x + 62.3f, 4f, z + 7.2f * i), Quaternion.Euler(0f, -90f, 0f)));//side right

                //torchFire
                torchFireInfos.Add(new Info(new Vector3(x + 12.8f * i, 3.7f, z + 34.7f), Quaternion.Euler(0f, 180f, 0f))); //horizontal up
                torchFireInfos.Add(new Info(new Vector3(x + 12.8f * i, 3.7f, z + 1.3f), Quaternion.identity)); //horizontal down
                torchFireInfos.Add(new Info(new Vector3(x + 1.3f, 3.7f, z + 7.2f * i), Quaternion.Euler(0f, 90f, 0f))); //side left
                torchFireInfos.Add(new Info(new Vector3(x + 62.7f, 3.7f, z + 7.2f * i), Quaternion.Euler(0f, -90f, 0f))); //side right
            }

            room.Add(torch, torchInfos);
            room.Add(torchLight, torchLightInfos);
            room.Add(torchFire, torchFireInfos);
        }

    }
    
    void enemiesPosition()
    {
        Dictionary<GameObject, ArrayList> room;
        ArrayList infos;
        float x, z;

        //SALA 1
        x = 16f * q; z = 0f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //bolb
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 15f, 1f, z + 18f), Quaternion.Euler(0f, 180f, 0f))); //left
        infos.Add(new Info(new Vector3(x + 17f, 1f, z + 18f), Quaternion.Euler(0f, 180f, 0f))); //right
        room.Add(bolb, infos); // Afegeix l'objecte del enemic concret + totes les instàncies a generar (coordenades i rotacions)

        enemies.Add(1, room); // Sala 1, enemics de la room

        //SALA 2
        x = 16f * q; z = 5f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //bolb
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 10f, 1f, z + 6f), Quaternion.Euler(0f, 180f, 0f))); //left
        infos.Add(new Info(new Vector3(x + 22f, 1f, z + 6f), Quaternion.Euler(0f, 180f, 0f))); //right
        room.Add(bolb, infos); // Afegeix l'objecte del enemic concret + totes les instàncies a generar (coordenades i rotacions)

        //skeleton
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 2, 1f, z + 18), Quaternion.Euler(0f, 180f, 0f))); //left
        infos.Add(new Info(new Vector3(x + 31f, 1f, z + 18f), Quaternion.Euler(0f, 180f, 0f))); //right
        room.Add(skeleton, infos); // Afegeix l'objecte del enemic concret + totes les instàncies a generar (coordenades i rotacions)

        enemies.Add(2, room);


        //SALA 3
        x = 8f * q; z = 5f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //skeleton
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 2f, 1f, z + 2f), Quaternion.Euler(0f, 90f, 0f)));
        room.Add(skeleton, infos); // Afegeix l'objecte del enemic concret + totes les instàncies a generar (coordenades i rotacions)

        //bolb
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 30f, 1f, z + 2f), Quaternion.Euler(0f, 180f, 0f))); //bottom
        infos.Add(new Info(new Vector3(x + 30f, 1f, z + 16f), Quaternion.Euler(0f, 180f, 0f))); //top
        room.Add(bolb, infos); // Afegeix l'objecte del enemic concret + totes les instàncies a generar (coordenades i rotacions)

        enemies.Add(3, room);

        //SALA 4
        x = 16f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        // Bat
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 15f, 1f, z + 18f), Quaternion.Euler(0f, 180f, 0f)));
        room.Add(bat, infos); // Afegeix l'objecte del enemic concret + totes les instàncies a generar (coordenades i rotacions)

        enemies.Add(4, room);


        //SALA 5
        x = 8f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //skeleton
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 15f, 1f, z + 1f), Quaternion.Euler(0f, 0f, 0f))); //down
        infos.Add(new Info(new Vector3(x + 15f, 1f, z + 18f), Quaternion.Euler(0f, 180f, 0f))); //up
        room.Add(skeleton, infos);

        // Bat
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 10f, 1f, z + 10f), Quaternion.Euler(0f, 90f, 0f)));
        room.Add(bat, infos);

        enemies.Add(5, room);


        //SALA 6
        x = 8f * q; z = 15f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //bolb
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 10f, 1f, z + 12f), Quaternion.Euler(0f, 180f, 0f)));
        infos.Add(new Info(new Vector3(x + 20f, 1f, z + 12f), Quaternion.Euler(0f, 180f, 0f)));
        room.Add(bolb, infos);

        // Bat
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 2f, 1f, z + 12f), Quaternion.Euler(0f, 90f, 0f)));
        infos.Add(new Info(new Vector3(x + 28f, 1f, z + 12f), Quaternion.Euler(0f, 270f, 0f)));
        room.Add(bat, infos);

        enemies.Add(6, room);


        //SALA 7
        x = 0f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        // Bat
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 10f, 1f, z + 10f), Quaternion.Euler(0f, 90f, 0f)));
        room.Add(bat, infos);

        // bolb
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 18f, 1f, z + 2f), Quaternion.Euler(0f, 90f, 0f)));
        infos.Add(new Info(new Vector3(x + 18f, 1f, z + 18f), Quaternion.Euler(0f, 90f, 0f)));
        room.Add(bolb, infos);

        enemies.Add(7, room);

        //SALA 8
        x = 24f * q; z = 5f * q;
        room = new Dictionary<GameObject, ArrayList>();

        // skeleton
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 20f, 1f, z + 2f), Quaternion.Euler(0f, 0f, 0f)));
        infos.Add(new Info(new Vector3(x + 20f, 1f, z + 6f), Quaternion.Euler(0f, 180f, 0f)));
        infos.Add(new Info(new Vector3(x + 20f, 1f, z + 8f), Quaternion.Euler(0f, 180f, 0f)));
        room.Add(skeleton, infos);

        enemies.Add(8, room);


        //SALA 9
        x = 24f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        enemies.Add(9, room);


        //SALA 10
        x = 24f * q; z = 15f * q;
        room = new Dictionary<GameObject, ArrayList>();

        //bolb
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 2f, 1f, z + 2f), Quaternion.Euler(0f, 90f, 0f)));
        infos.Add(new Info(new Vector3(x + 30f, 1f, z + 18f), Quaternion.Euler(0f, 180f, 0f)));
        room.Add(bolb, infos);

        // skeleton
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 6f, 1f, z + 16f), Quaternion.Euler(0f, 0f, 0f)));
        infos.Add(new Info(new Vector3(x + 28f, 1f, z + 16f), Quaternion.Euler(0f, 180f, 0f)));

        room.Add(skeleton, infos);
        enemies.Add(10, room);

        //SALA 11
        x = 16f * q; z = 15f * q;
        room = new Dictionary<GameObject, ArrayList>();

        // skeleton
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 16f, 1f, z + 6f), Quaternion.Euler(0f, 90f, 0f)));
        infos.Add(new Info(new Vector3(x + 16f, 1f, z + 18f), Quaternion.Euler(0f, 90f, 0f)));

        // Bat
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 6f, 1f, z + 12f), Quaternion.Euler(0f, 90f, 0f)));
        room.Add(bat, infos);

        enemies.Add(11, room);

        //SALA 12
        x = 32f * q; z = 10f * q;
        room = new Dictionary<GameObject, ArrayList>();

        // skeleton
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 8f, 1f, z + 8f), Quaternion.Euler(0f, 180f, 0f)));
        infos.Add(new Info(new Vector3(x + 8f, 1f, z + 12f), Quaternion.Euler(0f, 0f, 0f)));
        infos.Add(new Info(new Vector3(x + 22f, 1f, z + 8f), Quaternion.Euler(0f, 180f, 0f)));
        infos.Add(new Info(new Vector3(x + 22f, 1f, z + 12f), Quaternion.Euler(0f, 0f, 0f)));
        room.Add(skeleton, infos);

        enemies.Add(12, room);

        //SALA 13
        x = 12f * q; z = 20f * q;
        room = new Dictionary<GameObject, ArrayList>();

        // boss
        infos = new ArrayList(); // Coordenades + rotació
        infos.Add(new Info(new Vector3(x + 32f, 1f, z + 18f), Quaternion.Euler(0f, 0f, 0f)));
        room.Add(boss, infos);

        enemies.Add(13, room);


    }

    bool wallInScopeX(float x, float z)
    {
        switch (x)
        {
            case 0f:
            case 40f:
                return z >= 10f && z <= 15f;
            case 8f:
            case 32f:
                return z >= 5f && z <= 20f;
            case 16f:
            case 24f:
                return z >= 0f && z < 20f;
            case 12f:
            case 28f:
                return z >= 21f && z <= 28f;
        }
        return false;
    }

    bool wallInScopeZ(float x, float z)
    {
        switch (z)
        {
            case 0.0f:
                return x >= 16f && x <= 24f;
            case 5.0f:
            case 20.0f:
                return x >= 8f && x <= 32f;
            case 10.0f:
            case 15.0f:
                return x >= 0f && x <= 40f;
            case 29.0f:
                return x >= 13f && x <= 28f;
        }
        return false;
    }

    bool floorInScope(float x, float z)
    {
        if ((x >= 8f && x < 32f) && (z >= 5f && z < 20f)) return true;
        if ((x >= 0f && x < 40f) && (z >= 10f && z < 15f)) return true;
        if ((x >= 16f && x < 24f) && (z >= 0f && z <= 5f)) return true;
        if ((x >= 12f && x < 28f) && (z >= 20f && z < 29f)) return true; //boss room
        return false;
    }

    bool addDoors(float x, float z)
    {
        bool bossDoor = false;
        if (z == 20 && x == 20) bossDoor = true;

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
            if (!bossDoor)
            {
                GameObject obj = Instantiate(wallDoor, new Vector3(q * x - 2.0f, 1.0f, q * z), Quaternion.Euler(0f, 180f, 0f));
                obj.transform.localScale += new Vector3(1f, 0.3f, 0f);
            }
            else
            {
                GameObject obj = Instantiate(wallDoorBoss, new Vector3(q * x - 2.0f, 1.0f, q * z), Quaternion.Euler(0f, 180f, 0f));
                obj.transform.localScale += new Vector3(1f, 0.3f, 0f);
                obj.GetComponent<doorScript>().needBossKey = true;
            }
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
            for (float z = 0; z <= 30; z++)
            {
                //Corner
                if ((x == 0 && z == 10) || (x == 8 && z == 5) || (x == 16 && z == 0)) Instantiate(corner, new Vector3(q * x, 1.0f, q * z), Quaternion.identity);//left-bottom
                else if ((x == 0 && z == 15) || (x == 8 && z == 20) || (x == 12 && z == 29)) Instantiate(corner, new Vector3(q * x, 1.0f, q * z), Quaternion.Euler(0f, 90, 0f)); //left-up
                else if ((x == 40 && z == 10) || (x == 32 && z == 5) || (x == 24 && z == 0)) Instantiate(corner, new Vector3(q * x, 1.0f, q * z), Quaternion.Euler(0f, -90, 0f)); //right-bottom
                else if ((x == 40 && z == 15) || (x == 32 && z == 20) || (x == 28 && z == 29)) Instantiate(corner, new Vector3(q * x, 1.0f, q * z), Quaternion.Euler(0f, 180, 0f)); //right-up
                else
                {
                    //Doors
                    if (!addDoors(x, z))
                    {
                        //Walls
                        if ((z % 5 == 0 || z == 29f) && wallInScopeZ(x, z)) Instantiate(wall, new Vector3(q * x, 1.0f, q * z), Quaternion.identity); //horizontal
                        if (((x % 8 == 0) || x == 12f || x == 28f) && wallInScopeX(x, z)) Instantiate(wall, new Vector3(q * x, 1.0f, q * z), Quaternion.Euler(0f, 90f, 0f)); //vertical
                        if (z == 20 && (x == 16 || x == 24)) Instantiate(split, new Vector3(q * x, 1.0f, q * z), Quaternion.Euler(0f, 180f, 0f)); //split

                        //Corners bottom boss room
                        if (x == 12 && z == 20) Instantiate(corner, new Vector3(q * x, 1.0f, q * z), Quaternion.identity);//left-bottom
                        else if (x == 28 && z == 20) Instantiate(corner, new Vector3(q * x, 1.0f, q * z), Quaternion.Euler(0f, -90, 0f)); //right-bottom

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
        btn.GetComponent<button>().room = 1;

        //SALA 7
        x = 0f * q; z = 10f * q;
        btn = Instantiate(button, new Vector3(x + 1f, 2.25f, z + 10f), Quaternion.Euler(0f, 0f, -90f));
        btn.GetComponent<button>().room = 7;

        //SALA 8
        x = 24f * q; z = 5f * q;
        btn = Instantiate(button, new Vector3(x + 31f, 2.25f, z + 5f), Quaternion.Euler(0f, 0f, 90f));
        btn.GetComponent<button>().room = 8;

        //SALA 11
        x = 16f * q; z = 15f * q;
        btn = Instantiate(button, new Vector3(x + 16f, 1f, z + 10f), Quaternion.identity);
        btn.GetComponent<button>().room = 11;

        //SALA 12
        x = 32f * q; z = 10f * q;
        btn = Instantiate(button, new Vector3(x + 8.4f, 1f, z + 4.4f), Quaternion.identity); //left
        btn.GetComponent<button>().room = 12;
        btn.tag = "btn";

        btn = Instantiate(button, new Vector3(x + 8.4f, 1f, z + 16.4f), Quaternion.Euler(0f, 90f, 0f));
        btn.GetComponent<button>().room = 12;
        btn.tag = "btn";

        btn = Instantiate(button, new Vector3(x + 22.4f, 1f, z + 4.4f), Quaternion.Euler(0f, -90f, 0f)); //right
        btn.GetComponent<button>().room = 12;
        btn.tag = "btn";

        btn = Instantiate(button, new Vector3(x + 22.4f, 1f, z + 16.4f), Quaternion.Euler(0f, 180f, 0f));
        btn.GetComponent<button>().room = 12;
        btn.tag = "btn";

        //SALA 13
        x = 32f * q; z = 10f * q;
    }

    void removeEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null) Destroy(enemy);
        }
    }

    void initEnemies(int roomKey)
    {
        activeEnemies = new ArrayList();
        foreach (System.Collections.Generic.KeyValuePair<UnityEngine.GameObject, System.Collections.ArrayList> obj in enemies[roomKey])
        {
            foreach (Info info in obj.Value)
            {
                GameObject newEnemy = Instantiate(obj.Key, info.position, info.rotation);
                activeEnemies.Add(newEnemy);
            }
        }
        
        if (roomKey == 4)
        {
            float x = 16f * q; float z = 10f * q;

            // Archer
            GameObject arch = Instantiate(archer, new Vector3(x + 2f, 1.5f, z + 2f), Quaternion.identity); //down-left
            room4archers.Add(arch);
            arch.GetComponent<archer>().position = 0;

            arch = Instantiate(archer, new Vector3(x + 30f, 1.5f, z + 2f), Quaternion.identity); //down-right
            room4archers.Add(arch);
            arch.GetComponent<archer>().position = 1;

            arch = Instantiate(archer, new Vector3(x + 2f, 1.5f, z + 18f), Quaternion.identity); //up-left
            room4archers.Add(arch);
            arch.GetComponent<archer>().position = 2;

            arch = Instantiate(archer, new Vector3(x + 30f, 1.5f, z + 18f), Quaternion.identity); //up-right
            room4archers.Add(arch);
            arch.GetComponent<archer>().position = 3;
        }

        if (roomKey == 9)
        {
            float x = 24f * q; float z = 10f * q;
            // archer
            GameObject arch = Instantiate(archer, new Vector3(x + 2f, 1.5f, z + 2f), Quaternion.identity); //down-left
            room9archers.Add(arch);
            arch.GetComponent<archer>().position = 0;

            arch = Instantiate(archer, new Vector3(x + 30f, 1.5f, z + 2f), Quaternion.identity); //down-right
            room9archers.Add(arch);
            arch.GetComponent<archer>().position = 1;

            arch = Instantiate(archer, new Vector3(x + 2f, 1.5f, z + 18f), Quaternion.identity); //up-left
            room9archers.Add(arch);
            arch.GetComponent<archer>().position = 2;

            arch = Instantiate(archer, new Vector3(x + 30f, 1.5f, z + 18f), Quaternion.identity); //up-right
            room9archers.Add(arch);
            arch.GetComponent<archer>().position = 3;
        }
        if (roomKey == 6) checkRoom6 = true;
    }

    private void spawnPlayer()
    {
        Instantiate(player, new Vector3(80f, 1.0f, 5f), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        room = 1;

        //set object and enemies position
        objectPositions();
        enemiesPosition();

        //instantiate floor, walls and doors
        floorAndWalls();

        //Instantiate objects, buttons, enemies and player
        initObjects();
        initButtons();
        initEnemies(1);
        spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        roomUbication();
    }

    //Identifies if player has changed of room
    private void roomUbication()
    {
        if (gamePlayer != null)
        {
            int actualRoom = getRoomIndex(gamePlayer.transform.position);

            if (actualRoom != room)
            {
                if (actualRoom == 4 || actualRoom == 9) deleteArchers(actualRoom);
                removeEnemies();
                initEnemies(actualRoom);
                room = actualRoom;
                gameMainCamera.GetComponent<mainCamera>().roomActual = room;
                gamePlayer.GetComponent<PlayerBehaviour>().removeBoomerang();
            }
            else if (actualRoom == 6) room6();
        }
    }

    private void deleteArchers(int roomkey)
    {
        if (roomkey == 4)
            foreach (GameObject archer in room4archers) if (archer != null) Destroy(archer);
        if (roomkey == 9)
            foreach (GameObject archer in room9archers) if (archer != null) Destroy(archer);
        room4archers = new List<GameObject>();
        room9archers = new List<GameObject>();
    }

    private void room6()
    {
        if (activeEnemies.Count == 0 && checkRoom6)
        {
            checkRoom6 = false;
            GameObject roomDoor = GameObject.FindGameObjectWithTag("roomDoor");
            roomDoor.GetComponent<bossKeyDoor>().open = true;
            if (!room6Chest)
            {
                //chest
                room6Chest = true;
                float x = 8f * q;
                float z = 15f * q;
                GameObject chest6 = Instantiate(chest, new Vector3(x + 16f, 1f, z + 12f), Quaternion.identity);
                chest6.GetComponent<chest>().hasBoomerang = true;
            }
        }
    }

    public void enemyDie(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }

    //Given a position in the world, returns the index of the room
    private int getRoomIndex(Vector3 worldPosition)
    {
        List<List<int>> roomIndexes = new List<List<int>>()
        {
            new List<int> { 7 },
            new List<int> { -1, 3, 5, 6, 13, 13 },  // -1 just to make easier the return
            new List<int> { 1, 2, 4, 11, 13, 13 },
            new List<int> { -1, 8, 9, 10, 13, 13 }, // -1 just to make easier the return
            new List<int> { 12 }
        };

        int roomX = (int)Mathf.Floor(worldPosition.x / 32f);
        if (roomX == 0 || roomX == 4) return roomIndexes[roomX][0];

        int roomZ = (int)Mathf.Floor(worldPosition.z / 20f);
        return roomIndexes[roomX][roomZ];
    }

    /**
     * Sets a new collision for the given room and coords (x, z)
     */
    public void setCollision(int room, Vector2 coords)
    {
        // if the coords are already in the list, return
        if (collisions[room].Contains(coords)) return;
        collisions[room].Add(coords);
    }

    /**
     * Removes a collision for a given room and coords (x, z)
     */
     public void removeCollision(int room, Vector2 coords)
    {
        collisions[room].Remove(coords);
    }
}
