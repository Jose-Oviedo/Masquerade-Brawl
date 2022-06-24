using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private Vector3 startPos;

    LayerMask notAnEnemy;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        startPos = transform.position;
    }

    //when it collides with something
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the collider is friendly aka the one who shoot, do nothing
        if (other.gameObject.layer == notAnEnemy.value)
        {
            return;
        }

        //if other player 
        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        if (pm != null)
        {
            pm.TakeDamage(10);
        }
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponentInChildren<Animator>().Play("BulletCollides");
        rb.velocity = new Vector2( 0f,0f);
        transform.Rotate(0f, 180f, 0f);
    }

    //this is called when the collision animation ends
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void SetFrienlyLayer(LayerMask isEnemy)
    {
        notAnEnemy = isEnemy;        
    }

    private void Update()
    {
        //if the bullet flies to far away it is deleted
        float dist = Vector3.Distance(startPos, transform.position);
        if (Mathf.Abs(dist) > 20)
        {
            Destroy(gameObject);
        }
    }
}
