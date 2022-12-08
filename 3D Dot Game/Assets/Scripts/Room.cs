using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room<TRoomObject>
{
    private int height, width, roomIndex;
    private TRoomObject[,] room;

    public Room(int width, int height, int roomIndex,  Func<Room<TRoomObject>, int, int, TRoomObject> createNode)
    {
        this.width = width;
        this.height = height;
        this.roomIndex = roomIndex;

        room = new TRoomObject[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                room[x, y] = createNode(this, x, y);
            }
        }
    }
    
    /*Getters*/
    public TRoomObject getValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return room[x, y];
        }
        else
        {
            return default;
        }
    }

    public int getWidth()
    {
        return width;
    }

    public int getHeight()
    {
        return height;
    }

    public int getRoomIndex()
    {
        return roomIndex;
    }
}