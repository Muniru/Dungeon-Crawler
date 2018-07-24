using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RoomMovement : MonoBehaviour
{

    public RoomSpawner spawner;
    public RoomAssigner[,] map;
    public Vector2 mapPosition = Vector2.zero;
    public GameObject currentRoom = null;

    static float FloatConverterToNegative(float givenNumbers)
    {
        givenNumbers = givenNumbers * -1;
        return givenNumbers;
    }

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(.1f);
        map = new RoomAssigner[spawner.gridSizeX * 50, spawner.gridSizeY * 50];

        mapPosition = spawner.worldSize;
        foreach (RoomAssigner room in spawner.assignedRooms)
        {
            if (room.type == 1)
            {
                map[(int)spawner.worldSize.x, (int)spawner.worldSize.y] = room;
            }

            map[(int)room.pos.x, (int)room.pos.y] = room;
            //Debug.Log(room.transform.position.ToString());
        }

        RoomAssigner ra = map[(int)mapPosition.x, (int)mapPosition.y].gameObject.GetComponent<RoomAssigner>();
        currentRoom = ra.room;
    }


    public void SwitchRoom(string dir)
    {
        switch (dir)
        {
            case "NORTH":
                mapPosition.y = +25;
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 16);
                break;
            case "WEST":
                mapPosition.x = +25;
                gameObject.transform.position = new Vector3(transform.position.x + 11, transform.position.y);

                break;
            case "SOUTH":
                mapPosition.y = -25;
                gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 16);
                break;
            case "EAST":
                mapPosition.x = -25;
                gameObject.transform.position = new Vector3(transform.position.x - 11, transform.position.y);
                break;
        }
    }
}