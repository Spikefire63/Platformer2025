using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //privates
    private Vector2 movementVector;
    private Animator animator;
    private SpriteRenderer sprite_r;

    //publics
    public float Speed = 3;
    public float Jump = 500;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite_r = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("Speed", Mathf.Abs(movementVector.x));
        if(movementVector.x > 0)
            transform.Translate(Vector2.right * Speed * Time.deltaTime); //vector is (1,0)

        else if (movementVector.x < 0)
            transform.Translate(Vector2.left * Speed * Time.deltaTime); //vector (-1,0)

        if(movementVector.x < 0) //walking left
            sprite_r.flipX = true;
        else if (movementVector.x > 0) //walking right
            sprite_r.flipX = false;
    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector.x);
    }

    public void OnJump(InputValue movementValue)
    {
        transform.Translate(Vector2.up * Jump * Time.deltaTime);
        Debug.Log("Jumping");
    }
}
