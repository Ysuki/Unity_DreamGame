using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Header("Settings")]
    public float gravity;
    public bool isGrounded;
    public int timeInAir;
    public bool useGravity = true;
    [Space(10)]
    [Header("Gizmos")]
    public bool draw;
    public float groundCheckSize;
    public Vector3 groundCheckPosition;
    public LayerMask layerMask;

    Rigidbody rb;
    float fixedDelta;

    public float currentGravity;





    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnDrawGizmos()
    {
     if (draw == true)
        {
            if (isGrounded)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position + groundCheckPosition, groundCheckSize);

            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position + groundCheckPosition, groundCheckSize);

            }
        }
    }

    private void Update()
    {
        currentGravity = gravity + timeInAir * 24;


        if (useGravity == true)
        {
        var groundCheck = Physics.OverlapSphere(transform.position + groundCheckPosition, groundCheckSize, layerMask);
            if (groundCheck.Length != 0)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        PlayerIsGrounded();
        }
        
    }


    private void FixedUpdate()
    {

    }
    void PlayerIsGrounded()
    {
        //Vector3 down = new Vector3();
        //down = rb.velocity + Vector3.down * (gravity * 2) * Time.deltaTime;
        //down.x = rb.velocity.x;
        //down.z = rb.velocity.z;

        //rb.velocity = down;
        rb.AddForce(transform.up * currentGravity * -1, ForceMode.Acceleration);


        if (isGrounded == false)
        {
            fixedDelta += Time.fixedDeltaTime * 3;
            timeInAir = Mathf.RoundToInt(fixedDelta);
        }
        else
        {
            timeInAir = 0;
            fixedDelta = 0;
        }
    }
}
