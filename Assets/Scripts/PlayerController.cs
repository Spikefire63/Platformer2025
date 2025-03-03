using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //privates
    private Vector2 movementVector;
    private Animator animator;
    private SpriteRenderer sprite_r;
    private Rigidbody2D body;
    private bool isGrounded = false;
    private bool Jump = false;
    private float fireRate = 0.3f;
    private float nextFire = 0f;
    private bool facingRight = true;
    

    //publics
    public float Speed = 3;
    public float JumpForce = 250;
    public float maxSpeed = 7f;
    public float gravityMultiplier = 3f;
    public GameObject fire;
    public Transform firePoint;
    public float fadeDuration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite_r = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Speed", Mathf.Abs(movementVector.x));
        
        if(movementVector.x > 0 && body.velocity.x < maxSpeed)
            transform.Translate(Vector2.right * Speed * Time.deltaTime); //vector is (1,0) THIS IS WITHOUT PHYSICS
            //body.AddForce(Vector2.right * Speed);//THIS IS WITH PHYSICS

        else if (movementVector.x < 0 && Mathf.Abs(body.velocity.x) < maxSpeed)
            transform.Translate(Vector2.left * Speed * Time.deltaTime); //vector (-1,0)
            //body.AddForce(Vector2.left * Speed);

        if(movementVector.x < 0 && facingRight) //walking left
            {
                //sprite_r.flipX = true;
                Flip();
                facingRight = false;
            }
        else if (movementVector.x > 0 && !facingRight) //walking right
            {
                //sprite_r.flipX = false;
                Flip();
                facingRight = true;
            }

        if(Jump)
        {
            //transform.Translate(Vector2.up * JumpForce * Time.deltaTime);
            //StartCoroutine("LerpJump");

            body.AddForce(Vector2.up * JumpForce);
            Jump = false;
            isGrounded = false;
        }

        if(body.velocity.y < 0) //falling
        {
            body.gravityScale = gravityMultiplier;
        }
        else
        {
            body.gravityScale = 1;
        }

    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector.x);
    }

    public void OnJump(InputValue movementValue)
    {
        if(isGrounded)
            Jump = true;
        //transform.Translate(Vector2.up * Jump * Time.deltaTime);
        //Debug.Log("Jumping");
    }

    public void OnFire(InputValue movementValue)
    {
        if(Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.SetTrigger("IsShooting");
            Instantiate(fire, firePoint.position, facingRight ? firePoint.rotation : Quaternion.Euler(0, 180, 0));
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Touching Ground");
        }
        //adds a collision with the enemys so that now enemys decrease lives and reset the current level
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.DecreaseLives();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator LerpJump()
    {
        float desired = transform.position.y + 2; //how high to go
        
        while(transform.position.y < desired) //while not that high
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("boundary"))
        {
            //when falling off of the level, instead of completely restarting the sceneManager loads the current scene the player was on.
            GameManager.instance.DecreaseLives();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.gameObject.CompareTag("Next Level"))
        {
            //load the second scene
            SceneManager.LoadScene(1);
        }
    }

    public Vector2 GetDirection()
    {
        //retrun facingRight
        if(facingRight)
            return Vector2.right;
        else
            return Vector2.left;
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1; //invert the value
        transform.localScale = theScale;
    }

}
