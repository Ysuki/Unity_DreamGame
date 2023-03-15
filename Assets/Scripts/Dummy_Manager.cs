using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Manager : MonoBehaviour
{
    float playerHeight = 2f;
    [SerializeField] Transform orientation;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    public bool isGrounded { get; private set; }

    Vector3 slopeMoveDirection;
    RaycastHit slopeHit;

    public float rotateSpeed;
    float horizontalMovement;
    float verticalMovement;

    Gravity gravity;
    Statistics statistics;
    Rigidbody rb;

    [HideInInspector] public Transform cameraObject;
    float moveAmount;
    Animator anim;
     
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    HashSet<GameObject> damaged = new HashSet<GameObject>();

    [SerializeField] bool canAttack = true;
    [SerializeField] bool drawn;
    [SerializeField] float gizmoSize;
    [SerializeField] Color color;
    [SerializeField] Color hitColor;
    Color originalColor;
    [SerializeField] Transform autoAttPos;

    private void OnDrawGizmos()
    {
        if (drawn)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(autoAttPos.position, gizmoSize);
            Gizmos.color = color * new Color(0.5f, 0, 0, 2);
            Gizmos.DrawWireSphere(autoAttPos.position, gizmoSize);
        }
    }

    private void Awake()
    {
        gravity = GetComponent<Gravity>();
        statistics = GetComponent<Statistics>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        Cursor.visible = false;

        cameraObject = Camera.main.transform;

        rb.freezeRotation = true;

        originalColor = color;
        damaged.Add(this.gameObject);
    }

    private void Update()
    {
        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 0, 0.35f, Time.deltaTime);
        }

        isGrounded = Physics.CheckSphere(transform.position + gravity.groundCheckPosition, gravity.groundCheckSize, gravity.layerMask);

        MyInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        if (Input.GetMouseButton(1))
        {
            if (canAttack)
            {
                BasicAttackMelee();
            }
        }
    }

    private void BasicAttackMelee()
    {
        color = hitColor;
        canAttack = false;
        Collider[] colliders = Physics.OverlapSphere(autoAttPos.position, gizmoSize);
        StartCoroutine(ResetAttackCooldown());

        foreach (Collider c in colliders)
        {
            if (c.gameObject.GetComponentInParent<Statistics>() != null && !damaged.Contains(c.gameObject))
            {
                Statistics currentStats = c.gameObject.GetComponentInParent<Statistics>();
                bool yourTeam;
                if (statistics.thisIndex[1] != 'N')
                {

                    yourTeam = currentStats.thisIndex[1] == statistics.thisIndex[1];
                }
                else
                {
                    yourTeam = false;

                }

                if (currentStats.thisIndex != statistics.thisIndex && yourTeam == false)
                {
                    currentStats.TakeDamage(statistics.physicalDamage, DamageType.Physical_Damage, statistics.thisName);
                    damaged.Add(c.gameObject); ;

                }
            }
        }
    }
    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(statistics.attackSpeed);
        canAttack = true;
        damaged.Clear();
        damaged.Add(this.gameObject);
        color = originalColor;

    }
    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(transform.up * statistics.Jump * 100, ForceMode.Impulse);
        }
    }

    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        moveAmount = Mathf.Clamp01(Mathf.Abs(input.x) + Mathf.Abs(input.z));

        float cameraRot = Camera.main.transform.rotation.eulerAngles.y;

        Vector3 targetDir = Vector3.zero;
        targetDir = cameraObject.forward * input.z;
        targetDir += cameraObject.right * input.x;
        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = this.transform.forward;
        }
        float rs = rotateSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(this.transform.rotation, tr, rs * Time.deltaTime);
        this.transform.rotation = targetRotation;


        Vector3 direction = targetDir;


        if (isGrounded && !OnSlope() && moveAmount != 0)
        {
            rb.AddForce(direction * moveSpeed * movementMultiplier, ForceMode.Acceleration);

        }
        else if (isGrounded && OnSlope() && moveAmount != 0)
        {
            slopeMoveDirection = Vector3.ProjectOnPlane(targetDir, slopeHit.normal);

            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);

        }
        else if (!isGrounded && moveAmount != 0)
        {
            rb.AddForce(direction * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);

        }
    }
}