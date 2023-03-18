using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Header("Settings")]
    public float gravity;
    public float multiplier;
    public bool isGrounded;

    public int timeInAir;
    public bool useGravity = true;
    [Space(10)]
    [Header("Gizmos")]

    public float groundDistance;

    public bool draw;
    public Vector3 groundCheckPosition;
    public Vector3 groundCheckRaycast;
    public float groundCheckSize;


    public LayerMask layerMask;

    Rigidbody rb;
    [HideInInspector]public float fixedDelta;
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
        currentGravity = gravity + timeInAir * multiplier;

        var groundCheck = Physics.OverlapSphere(transform.position + groundCheckPosition, groundCheckSize, layerMask);
        if (groundCheck.Length != 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        Vector3 raycastOrigin = transform.position - (transform.up * groundCheckRaycast.y);
        if (Physics.Raycast(raycastOrigin, -transform.up, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            groundDistance = hit.distance;
        }
    }

    private void FixedUpdate()
    {
        PlayerIsGrounded();
    }
    void PlayerIsGrounded()
    {
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
