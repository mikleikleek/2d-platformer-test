using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBasic : MonoBehaviour
{
    public int enemySpeed;
    public int xMoveDirection;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0));
        body.velocity = new Vector2(xMoveDirection, 0) * enemySpeed;

        if (hit.distance < 0.7f)
        {
            Flip();

            if (hit.collider.tag == "Player")
            {
                Destroy(hit.collider.gameObject);
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
	}

    void Flip()
    {
        // This is a handy way to flip movement
        xMoveDirection *= -1;

        //if (xMoveDirection > 0)
        //{
        //    xMoveDirection = -1;
        //}
        //else
        //{
        //    xMoveDirection = 1;
        //}

        //Teacher told me of this one
        //bool x = false;
        //x |= true;
    }
}
