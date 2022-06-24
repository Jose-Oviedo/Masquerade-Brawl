using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        if (pm != null)
        {
            pm.TakeDamage(10);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        //if the bullet flies to far away it is deleted
        float dist = Vector3.Distance(startPos, transform.position);
        if (Mathf.Abs(dist) > 50)
        {
            Destroy(gameObject);
        }
    }
}
