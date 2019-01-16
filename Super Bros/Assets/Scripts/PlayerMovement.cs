using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerSpeed = 10;
    private bool facingRight = false;
    public int playerJumpPower = 1250;
    private float moveX;
    public bool isGrounded;
    public float distanceToBottomOfPlayer = 0.9f;

    private Rigidbody2D body;
	
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update ()
    {
        PlayerMove();
        PlayerRaycast();
	}

    void PlayerMove()
    {
        // Controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            if (isGrounded == false)
            {
                return;
            }

            Jump();
        }

        // Player direction
        if (moveX < 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == true)
        {
            FlipPlayer();
        }

        // Physics
        body.velocity = new Vector2(moveX * playerSpeed, body.velocity.y);

        //if ((gameObject.GetComponent<Rigidbody2D>().velocity.y < -0.1) || (gameObject.GetComponent<Rigidbody2D>().velocity.y > 0.1))
        //{
        //    isGrounded = false;
        //}
        //else
        //{
        //    isGrounded = true;
        //}
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    Debug.Log("Player has collided with " + col.collider.name);
    //    if (col.gameObject.tag == "Ground")
    //    {
    //        isGrounded = true;
    //    }
    //}

    void PlayerRaycast()
    {
        // Ray up
        RaycastHit2D rayUp = Physics2D.Raycast(transform.position, Vector2.up);
        if (rayUp.collider != null && rayUp.distance < distanceToBottomOfPlayer && rayUp.collider.tag == "BoxBonus")
        {
            Destroy(rayUp.collider.gameObject);
        }

        // Ray down
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        if (rayDown.collider != null && rayDown.distance < distanceToBottomOfPlayer && rayDown.collider.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 300);
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 7;
            rayDown.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            rayDown.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rayDown.collider.gameObject.GetComponent<EnemyBasic>().enabled = false;
        }
        if (rayDown.collider != null && rayDown.distance < distanceToBottomOfPlayer && rayDown.collider.tag != "Enemy")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
