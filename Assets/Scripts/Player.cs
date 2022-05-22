using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float horizontalSpeed, jumpHeight, jumpGravity, fallGravity, maxVerticalVelocity;
    public int numberOfJumps;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D bc;
    private float verticalVelocity = 0f, horizontalVelocity = 0f;
    public int jumps;
    private List<Collision2D> groundedPoints;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        groundedPoints = new List<Collision2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if jumped
        bool jump = Input.GetKey(KeyCode.Space);
        bool jumpStarted = Input.GetKeyDown(KeyCode.Space);

        // Calculate horizontal velocity
        horizontalVelocity = Input.GetAxisRaw("Horizontal") * horizontalSpeed;

        if (IsGrounded())
        {
            jumps = 0;
            if (jumpStarted && jumps < numberOfJumps)
            {
                //Jump
                verticalVelocity = Mathf.Sqrt(2.0f * jumpGravity * jumpHeight);
                jumps++;
            }
            else if (!jump)
            {
                verticalVelocity = 0f;
            }
        }
        else
        {
            //even if yPosition ground check is on: don't fall trough stuff
            if (IsGrounded() && verticalVelocity < 0f) verticalVelocity = 0f;

            //Even if first jump is not used to get to air, it is used now.
            if (jumps == 0) jumps = 1;

            if (jumpStarted && jumps < numberOfJumps)
            {
                //On air jump
                verticalVelocity = Mathf.Sqrt(2.0f * jumpGravity * jumpHeight);
                jumps++;
            }
            else if (jump && verticalVelocity > 0f)
            {
                //Gaining height:
                verticalVelocity -= jumpGravity * Time.deltaTime;
            }
            else
            {
                //Falling:
                verticalVelocity -= fallGravity * Time.deltaTime;
            }

            verticalVelocity = Mathf.Clamp(verticalVelocity, -maxVerticalVelocity, maxVerticalVelocity);
        }

        rb.velocity = new Vector3(horizontalVelocity, verticalVelocity, 0f);
        
        //Animation
        anim.SetFloat("Speed", Mathf.Abs(horizontalVelocity));

        //Turn when moving to either direction:
        if (horizontalVelocity > 0f) transform.localScale = new Vector3(1f, 1f, 1f);
        else if (horizontalVelocity < 0f) transform.localScale = new Vector3(-1f, 1f, 1f);

        Debug.Log("Grounded: " + IsGrounded());
        if (IsGrounded()) Debug.DrawRay(bc.bounds.center, Vector2.down * (bc.bounds.extents.y + 0.01f), Color.green);
        else Debug.DrawRay(bc.bounds.center, Vector2.down * (bc.bounds.extents.y + 0.01f), Color.red);
    }


    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(bc.bounds.center, Vector2.down, bc.bounds.extents.y + 0.1f, 1 << LayerMask.NameToLayer("Obstacle"));

        return hit.collider != null;
        //return groundedPoints.Count > 0;
        //return grounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[10];
        int contactCount = collision.GetContacts(contacts);

        for (int i = 0; i < contactCount; i++)
        {
            if ((contacts[i].point.y < transform.position.y && Mathf.Abs(contacts[i].point.x - transform.position.x) < 0.5f))
            {
                groundedPoints.Add(collision);
            }
        }
    }
   /* private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[10];
        int contactCount = collision.GetContacts(contacts);

        for (int i = 0; i < contactCount; i++)
        {
            if ((contacts[i].point.y < transform.position.y && Mathf.Abs(contacts[i].point.x-transform.position.x)<0.5f))
            {
                grounded = true;
                return;
            }
        }

        grounded = false;
    }*/

    private void OnCollisionExit2D(Collision2D collision)
    {
        groundedPoints.Remove(collision);
        //grounded = false;
    }


}
