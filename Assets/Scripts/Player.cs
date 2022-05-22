using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float horizontalSpeed, jumpHeight, jumpGravity, fallGravity, maxVerticalVelocity;
    public int numberOfJumps;

    private Rigidbody2D rb;
    private Animator anim;
    private float verticalVelocity = 0f, horizontalVelocity = 0f;
    public int jumps;
    private bool grounded;

    public enum GroundCheck { yPosition, touch, onTop };
    public GroundCheck groundCheck = GroundCheck.onTop;
    
    public enum Controls { normal, inverted, rotated}
    public Controls controls = Controls.normal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateData();

        // Check if jumped
        bool jump = (Input.GetKey(KeyCode.Space) && controls != Controls.rotated) || (Input.GetKey(KeyCode.LeftArrow) && controls == Controls.rotated);
        bool jumpStarted = (Input.GetKeyDown(KeyCode.Space) && controls != Controls.rotated) || (Input.GetKeyDown(KeyCode.LeftArrow) && controls == Controls.rotated);

        // Calculate horizontal velocity
        if (controls == Controls.normal)
        {
            horizontalVelocity = Input.GetAxisRaw("Horizontal") * horizontalSpeed;
        }
        else if (controls == Controls.inverted)
        {
            horizontalVelocity = -Input.GetAxisRaw("Horizontal") * horizontalSpeed;
        }
        else if (controls == Controls.rotated)
        {
            if (Input.GetKey(KeyCode.RightArrow)) horizontalVelocity = -horizontalSpeed;
            else if (Input.GetKey(KeyCode.Space)) horizontalVelocity = horizontalSpeed;
            else horizontalVelocity = 0f;
        }

        if (IsGrounded())
        {
            jumps = 0;
            if (jumpStarted && jumps < numberOfJumps)
            {
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
            if (grounded && verticalVelocity < 0f) verticalVelocity = 0f;

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

        Debug.Log("Should face left: " + (horizontalVelocity < 0f) + ". Faces Left: " + anim.GetBool("Faces Left"));
    }

    private void UpdateData()
    {
        //FindObjectOfType<GameManager>().currentLevel.GetComponent<LevelManager>().SetValues(this);
    }

    public bool IsGrounded()
    {
        return grounded;
    }


    private void OnCollisionStay2D(Collision2D collision)
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }


}
