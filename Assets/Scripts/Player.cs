using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    public Rigidbody2D rigidbod;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D myFeet;
    float gravity;
    GameSession gameSession;

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpDistamce = 20f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] public Vector2 deathKick = new Vector2(25f, 25f);

    bool isAlive = true;

	// Use this for initialization
	void Start () {
        rigidbod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravity = rigidbod.gravityScale;
        gameSession = FindObjectOfType<GameSession>();
	}

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return;  }
        Run();
        flipSprite();
        Jump();
        Climb();
        //Die();
    }

    void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rigidbod.velocity.y);
        rigidbod.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbod.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void flipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbod.velocity.x) < Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbod.velocity.x), 1f);
        }
    }

    void Jump()
    {
        LayerMask layer = LayerMask.GetMask("Ground");

        if (!myFeet.IsTouchingLayers(layer)) return;

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            rigidbod.velocity += new Vector2(0f, rigidbod.velocity.y + jumpDistamce);
        }
    }

    void Climb()
    {
        LayerMask layer = LayerMask.GetMask("Ladder");

        if (!myFeet.IsTouchingLayers(layer))
        {
            rigidbod.gravityScale = gravity;
            animator.SetBool("isClimbing", false);
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVector = new Vector3(rigidbod.velocity.x, controlThrow * climbSpeed);
        rigidbod.velocity = climbVector;

        rigidbod.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(rigidbod.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    public void Die()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Death")))
        {
            isAlive = false;
            animator.SetTrigger("Die");
            rigidbod.velocity = deathKick;
            gameSession.ProcessPlayerDeath();
        }

        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            rigidbod.velocity = deathKick;
        }

    }

}
