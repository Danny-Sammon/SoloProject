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

    public GameObject[] gameObjects;
    bool isOnGround;
    [SerializeField]
    Transform floorSensor;
    private float speed = 1.5f;
    private float jHeight = 3.5f;
    bool isAttacking = false;
    bool isBlocking = false;
    [SerializeField]
    GameObject HitBox;
    [SerializeField]
    GameObject DefHitBox;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        HitBox.SetActive(false);
        DefHitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("player_att");

            StartCoroutine(DoAttack());
        }
        if (Input.GetKey(KeyCode.Mouse1) && !isBlocking)
        {
            isBlocking = true;
            animator.Play("player_def");

            StartCoroutine(DoDef());
        }
    }

    IEnumerator DoAttack()
    {
        HitBox.SetActive(true);
        yield return new WaitForSeconds(.4f);
        HitBox.SetActive(false);
        isAttacking = false;
    }
    IEnumerator DoDef()
    {
        DefHitBox.SetActive(true);
        yield return new WaitForSeconds(.1f);
        DefHitBox.SetActive(false);
        isBlocking = false;
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
            if (isOnGround && !isAttacking)
            animator.Play("player_run");
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey("a"))
        {
           //move Left
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (isOnGround && !isAttacking)
            animator.Play("player_run");
            spriteRenderer.flipX = true;
        }
        else if (isOnGround)
        {
            if (!isAttacking)
            {
            animator.Play("player_idle");
            }
            
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKey("space") && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jHeight);
            animator.Play("player_Jump");
        }
    }
    void RemovalCoin()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Acid");
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
    void RemoveCoin()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Coin");
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
    void RemovalSpike()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Spike");
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "spike")
        {
            lives -= 1;
            Destroy(other.gameObject);
            if (lives == 0)
            {
                print("GAMEOVER");
                RemoveCoin();
                RemovalSpike();
                Time.timeScale = 0;
            }
        }
       
        if (other.gameObject.name == "Coin")
        {
            score += 50;
            Destroy(other.gameObject);
        }
    }
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 30), "Score: " + score);
        GUI.Box(new Rect(10, 40, 100, 30), "lives: " + lives);
    }
}
