using System.Collections;
using System.Collections.Generic;
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
    

    //publics
    public float Speed = 3;
    public float JumpForce = 250;
    public float maxSpeed = 7f;
    public float gravityMultiplier = 3f;

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

        if(movementVector.x < 0) //walking left
            sprite_r.flipX = true;
        else if (movementVector.x > 0) //walking right
            sprite_r.flipX = false;

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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Touching Ground");
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
        GameManager.instance.DecreaseLives();
        SceneManager.LoadScene(0);
        }
    }

}
