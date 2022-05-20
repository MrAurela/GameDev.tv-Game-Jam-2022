using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float airDeceleration = 50;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float[] jumpHeight = new float[] { 4, 3 };

    private BoxCollider2D collider;

    private Vector2 velocity;

    private bool grounded;
    private int jumps;
    private Memory memory;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        memory = GetComponent<Memory>();
    }

    float xInput = 0f;
    int jumpCount = 0;

    private void Update()
    {
        //float xInput = Input.GetAxisRaw("Horizontal");

        /*if (Input.GetButton("Right"))
        {
            xInput = 1f;
            memory.Add(1f);
        }
        else if (Input.GetButton("Left"))
        {
            xInput = -1f;
            memory.Add(-1f);
        } else
        {
            memory.Add(0f);
        }*/



        if (grounded)
        {
            velocity.y = 0f;
            jumps = 0;
        } else
        {
            jumps = Mathf.Max(1, jumps);
        }

        if (IsJump() && jumps < jumpHeight.Length)
        {
            velocity.y = Mathf.Sqrt(2 * jumpHeight[jumps] * Mathf.Abs(Physics2D.gravity.y));
            jumps++;
        }

        float acceleration = grounded ? walkAcceleration : airAcceleration;
        float deceleration = grounded ? groundDeceleration : airDeceleration;

        if (xInput != 0f)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * xInput, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0f, deceleration * Time.deltaTime);
        }

        velocity.y += Physics2D.gravity.y * Time.deltaTime;

        transform.Translate(velocity * Time.deltaTime);


        //Test collisions
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, collider.size, 0);


        grounded = false;

        //Move away from collisions
        foreach (Collider2D hit in hits)
        {
            if (hit == collider) continue;

            string tag = hit.gameObject.tag;

            if (tag == "Obstacle" || tag == "Player")
            {
                ColliderDistance2D colliderDistance = hit.Distance(collider);
                if (colliderDistance.isOverlapped)
                {
                    Vector2 distance = colliderDistance.pointB - colliderDistance.pointA;
                    transform.Translate(-distance);
                    if (distance.y > 0) velocity.y = 0;
                }
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                }
            }
            
        }

    }

    public void Move(float x)
    {
        xInput = x;
    }

    public void Jump()
    {
        jumpCount++;
    }

    private bool IsJump()
    {
        /*bool j = jump;
        jump = false;
        return j;*/
        bool isJump = jumpCount > 0;
        jumpCount = Mathf.Max(jumpCount-1, 0);
        return isJump;
    }

}
