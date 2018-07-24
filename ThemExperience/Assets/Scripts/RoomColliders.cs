using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Cord
{
    NORTH, WEST, EAST, SOUTH
}

public class RoomColliders : MonoBehaviour {

    public Cord myCords = Cord.NORTH;
    public GameObject lastHit = null;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Transform hit = collision.gameObject.transform;
        lastHit = collision.gameObject;
        if (collision.CompareTag("Player"))
        {
            RoomMovement player = hit.GetComponent<RoomMovement>();
            player.SwitchRoom(myCords.ToString());
        }
    }
}