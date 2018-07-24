using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

    public GameObject roomSprites;
    public Vector2 worldSize = new Vector2(4, 4);
    public RoomInfo[,] chambers;
    public List<Vector2> takenPositions = new List<Vector2>();
    public int gridSizeX, gridSizeY, numberOfRooms = 20;
    public List<RoomAssigner> assignedRooms = new List<RoomAssigner>();
    public GameObject player;
    public GameObject nextFloor;


    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int re = 0;
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            re++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            re++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.right))
        {
            re++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            re++;
        }
        return re;
    }

    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingpos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool upDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (upDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingpos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingpos) || x >= gridSizeX || x< -gridSizeX || y >= gridSizeY || y< -gridSizeY);
        return checkingpos;
    }
    Vector2 SelectiveNewPosition()
    {
        int index = 0, rep = 0;
        int x = 0, y = 0;
        Vector2 checkPos = Vector2.zero;
        do
        {
            rep = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                rep++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && rep < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool upDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (upDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);

        return checkPos;
    }

    void Awake()
    {
        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        {
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms();
        SetRoomDoors();
        DrawMap();
    }
 
    public void NewMap()
    {
        

        CreateRooms();
        SetRoomDoors();
        DrawMap();
    }

    void CreateRooms()
    {
        //start
        chambers = new RoomInfo[gridSizeX * 2, gridSizeY * 2];
        chambers[gridSizeX, gridSizeY] = new RoomInfo(Vector2.zero, Vector2.zero, 1);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;

        //math numbers
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = .01f;

        //Makin Rooms
        for (int i = 0; i < numberOfRooms -1 ; i++)
        {
            //choose room
            float randomAdd = (i) / ((numberOfRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomAdd);

            //get position
            checkPos = NewPosition();

            //Test Position
            if(NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int repeat = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    repeat++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && repeat > 100);
            }
            chambers[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new RoomInfo(checkPos, new Vector2((int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY), 0);
            if ((numberOfRooms - i) == 2)
            {
                chambers[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new RoomInfo(checkPos, new Vector2((int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY), 2);
            }
            takenPositions.Insert(0, checkPos);
        }
    }

    void SetRoomDoors()
    {
        for (int x = 0; x < ((gridSizeX * 2)); x++)
        {
            for (int y = 0; y < ((gridSizeY * 2)); y++)
            {
                if (chambers[x, y] == null)
                {
                    continue;
                }
                //top check
                if (y - 1 < 0)
                {
                    chambers[x, y].doorBot = false;
                }
                else
                {
                    chambers[x, y].doorBot = (chambers[x,y -1] != null);
                }
                //bot check
                if (y + 1 >= gridSizeY * 2)
                {
                    chambers[x, y].doorTop = false;
                }
                else
                {
                    chambers[x, y].doorTop = (chambers[x, y + 1] != null);
                }
                //left check
                if (x - 1 < 0)
                {
                    chambers[x, y].doorLeft = false;
                }
                else
                {
                    chambers[x, y].doorLeft = (chambers[x -1, y] != null);
                }
                //right check
                if (x + 1 >= gridSizeY * 2)
                {
                    chambers[x, y].doorRight = false;
                }
                else
                {
                    chambers[x, y].doorRight = (chambers[x + 1, y] != null);
                }
            }
        }
    }

    void DrawMap()
    {
        foreach (RoomInfo room in chambers)
        {
            if (room == null)
            {
                continue;
            }
            Vector2 drawPos = room.gridPosition;

            RoomAssigner assigner = Instantiate(roomSprites, drawPos, Quaternion.identity,transform).GetComponent<RoomAssigner>();
            assigner.type = room.type;
            assigner.up = room.doorTop;
            assigner.down = room.doorBot;
            assigner.left = room.doorLeft;
            assigner.right = room.doorRight;
            assignedRooms.Add(assigner);

            if (assigner.type == 1)
            {
                GameObject gameObject = Instantiate(player, assigner.transform.position, Quaternion.identity);
                RoomMovement roomMovement = gameObject.GetComponent<RoomMovement>();
                roomMovement.spawner = this;
            } else if(assigner.type == 2)
            {
                StartCoroutine(SpawnBox(assigner));
            }

        }

    }
    IEnumerator SpawnBox(RoomAssigner assigner)
    { 
        yield return new WaitForSeconds(.1f);
        Instantiate(nextFloor, assigner.room.transform.position, Quaternion.identity);
    }
}

