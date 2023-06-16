using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float jumpHeight = 4;
    public float moveSpeed = 3;

    public float hangTime = 0.3f;
    private float hangCount;

    public float jumpBuffer = 0.3f;
    private float bufferCount;

    public float colliderRadius = 0.15f;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //hang time
        if (isGrounded())
        {
            hangCount = hangTime;
        }
        else
        {
            hangCount -= Time.deltaTime;
        }

        //jump buffer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bufferCount = jumpBuffer;
        }
        else
        {
            bufferCount -= Time.deltaTime;
        }
        //horizontal
        myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidBody.velocity.y);

        //vertical
        if (bufferCount >= 0 && hangCount > 0)
        {
            myRigidBody.velocity = Vector2.up * jumpHeight;
            bufferCount = 0f;
            hangCount = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && myRigidBody.velocity.y > 0)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y * 0.5f);
        }
    }
    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckCollider.position, colliderRadius, groundLayer);
    }
}
