using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    public bool IsGrounded { get { return Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer).Length > 0; } }

    [SerializeField] private float speed = 5f;
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float gravityLerpSpeed = 5f;


    private Vector2 moveInput;
    private bool jumpInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (jumpInput && IsGrounded)
            Jump();

        Move();
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        moveInput = _context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.started)
            jumpInput = true;
        else if (_context.canceled)
            jumpInput = false;
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        jumpInput = false;
    }

    public void Move()
    {
        Vector2 newVelocity = rb.velocity;

        newVelocity.x = Mathf.Lerp(rb.velocity.x, moveInput.x * speed, lerpSpeed * Time.deltaTime);
        newVelocity.y = Mathf.Lerp(rb.velocity.y, -gravity, gravityLerpSpeed * Time.deltaTime);

        rb.velocity = newVelocity;
    }

#if UNITY_EDITOR
    public bool showGizmos = true;
    private void OnDrawGizmos()
    {
        if (!showGizmos) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
#endif
}
