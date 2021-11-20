using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int lives = 3;
    public int score = 0;

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    bool isOnGround;
    [SerializeField]
    Transform floorSensor;
    private float speed = 1.5f;
    private float jHeight = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(Physics2D.Linecast(transform.position, floorSensor.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
        if (Input.GetKey("d"))
        {
            //move right
            
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (isOnGround)
            animator.Play("player_run");
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey("a"))
        {
           //move Left
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (isOnGround)
            animator.Play("player_run");
            spriteRenderer.flipX = true;
        }
        else
        {
            if (isOnGround)
            animator.Play("player_idle");

            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKey("space") && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jHeight);
            animator.Play("player_Jump");
        }
    }
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 30), "Score: " + score);
        GUI.Box(new Rect(10, 40, 100, 30), "lives: " + lives);
    }
}
