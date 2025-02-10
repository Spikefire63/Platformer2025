using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float force = 200f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(Vector2.right * force);
        rb.velocity = force * Vector2.right;
        Invoke("Die", 4f); //wait 4 seconds bedore destroying the bullet
    }

    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject);
    }
}
