using UnityEngine;

public class RoomInfo {

    public Vector2 gridPosition, trueArrayPos;

    public int  type;

    public bool doorTop, doorBot, doorLeft, doorRight;

    public RoomInfo(Vector2 givenGridPos,Vector2 givenTrueArrayPos, int  givenType )
    {
        type = givenType;
        gridPosition = givenGridPos;
        trueArrayPos = givenTrueArrayPos;
    }
}
