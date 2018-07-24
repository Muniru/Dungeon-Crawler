using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speedVertical;
    public float speedHorizontal;
    [Header("Flip")]
    [SerializeField] private GameObject flipRight;
    [SerializeField] private GameObject flipLeft; private bool lastFlip;
    private SpriteRenderer playerSprite;
    [Header("Shooting")]
    public int damage;
    public float shotSpeed;
    public GameObject bulletObj;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.O))
        {
            Shoot();
        }
    }

    public void Movement()
    {
        //movement  
        Vector2 xy = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speedHorizontal, Input.GetAxis("Vertical") * Time.deltaTime * speedVertical);
        transform.position = new Vector3(transform.position.x + xy.x, transform.position.y + xy.y);

        //flipping sprite
        if (Input.GetAxis("Horizontal") >= .3f)
        {
            playerSprite.flipX = true;
            lastFlip = true;

        }
        else if (Input.GetAxis("Horizontal") <= -.3f)
        {
            playerSprite.flipX = false;
            lastFlip = false;
        }
        else { playerSprite.flipX = lastFlip; }
        //flipping the gun & hand
        if (lastFlip == false)
        {
            flipLeft.SetActive(true);
            flipRight.SetActive(false);
        }
        else
        {
            flipLeft.SetActive(false);
            flipRight.SetActive(true);
        }
    }

    //shooting
    void Shoot()
    {   
        //HardCoding the offset for the bullet spawn
        Vector3 bulletSpwnPos = playerSprite.flipX ? new Vector3(0.17f + transform.position.x, 0.015f + transform.position.y, 0) : new Vector3(-0.17f + transform.position.x, 0.015f + transform.position.y, 0);
        GameObject bulletRB;
        bulletRB = Instantiate(bulletObj, bulletSpwnPos, Quaternion.identity) as GameObject;
        Bullet bulletScript = bulletRB.GetComponent<Bullet>();
        bulletScript.Spawned(damage, shotSpeed, playerSprite.flipX);
    }
}
