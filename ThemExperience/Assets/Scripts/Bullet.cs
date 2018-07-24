using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //constructor ish
    public void Spawned(int BDamage, float BSpeed, bool BDir)
    {
        damage = BDamage;
        speed = BSpeed;
        DirRight = BDir;
    }

    private int damage;
    private float speed;
    private bool DirRight;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (DirRight)
            rb.AddForce(transform.right * speed * Time.deltaTime * 10, ForceMode2D.Impulse);
        else
            rb.AddForce(-transform.right * speed * Time.deltaTime * 10, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //damage other
            Destroy(gameObject);
            Debug.Log("1");
        } else if (!collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("2");
        }
    }
    public void Damage(ref float health)
    {
        health -= damage;
    }

}
