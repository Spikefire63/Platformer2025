using UnityEngine;

public class FireController : MonoBehaviour
{
    public float force = 200f;

    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = Player.GetComponent<PlayerController>().GetDirection();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(Vector2.right * force);
        rb.velocity = force * direction;
        Invoke("Die", 1.5f); //wait 4 seconds bedore destroying the bullet
    }

    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
            Die();   
    }
}
