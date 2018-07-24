using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomAssigner : MonoBehaviour
{

    //public Sprite doorU, doorD, doorR, doorL, doorUD, doorRL, doorUR, doorUL, doorDR, doorDL, doorUDL, doorURL, doorUDR, doorDRL, doorUDRL;
    public GameObject roomU, roomD, roomR, roomL, roomUD, roomRL, roomUR, roomUL, roomDR, roomDL, roomUDL, roomURL, roomUDR, roomDRL, roomUDRL;

    public bool up, down, left, right;
    public int type;
    public Vector2 pos;
    public GameObject room;

    void Start()
    {
        transform.position = new Vector3(transform.position.x * 25, transform.position.y * 25);
        PickSprite();
    }

    void PickSprite()
    {
        if (up)
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        room = Instantiate(roomUDRL, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        room = Instantiate(roomUDR, transform.position, Quaternion.identity);
                    }
                }
                else if (left)
                {
                    room = Instantiate(roomUDL, transform.position, Quaternion.identity);
                }
                else
                {
                    room = Instantiate(roomUD, transform.position, Quaternion.identity);
                }

            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        room = Instantiate(roomURL, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        room = Instantiate(roomUR, transform.position, Quaternion.identity);
                    }
                }
                else if (left)
                {
                    room = Instantiate(roomUL, transform.position, Quaternion.identity);
                }
                else
                {
                    room = Instantiate(roomU, transform.position, Quaternion.identity);
                }
            }
        }
        else
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        room = Instantiate(roomDRL, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        room = Instantiate(roomDR, transform.position, Quaternion.identity);
                    }
                }
                else if (left)
                {
                    room = Instantiate(roomDL, transform.position, Quaternion.identity);
                }
                else
                {
                    room = Instantiate(roomD, transform.position, Quaternion.identity);
                }

            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        room = Instantiate(roomRL, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        room = Instantiate(roomR, transform.position, Quaternion.identity);
                    }
                }
                else if (left)
                {
                    room = Instantiate(roomL, transform.position, Quaternion.identity);
                }
            }
        }
        
    }
}
