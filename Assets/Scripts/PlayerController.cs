using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private CameraMovementController camController;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private List<GameObject> healthIcons = new List<GameObject>();

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float mushroomJumpForce = 15f;
    [SerializeField] private float mosquitoJumpForce = 8f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float gravityLerpSpeed = 5f;
    [SerializeField] private float rootTime = 1f;
    [SerializeField] private float rootCameraSpeedMultiplier = 0.2f;
    [SerializeField] private bool autoJump = true;
    [SerializeField] private float jumpTimeout = 0.5f;

    public bool IsGrounded { get { return Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer).Length > 0; } }
    public bool WasGrounded { get { return Time.time - lastGroundedTime < coyoteTime; } }
    public bool CanJump { get { return Time.time - lastJumpTime > jumpTimeout; } }
    public int Health { get; private set; }

    private Vector2 moveInput;
    private bool jumpInput;
    private bool rootInput;

    private float lastGroundedTime;
    private bool isRooted;
    private float rootTimer;
    private float lastJumpTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Health = maxHealth;
    }

    private void Update()
    {
        if (IsGrounded)
            lastGroundedTime = Time.time;

        if (isRooted)
        {
            rootTimer += Time.deltaTime;

            rb.velocity = Vector2.zero;

            if (rootTimer >= rootTime)
            {
                rootTimer = 0f;
                Root(false);
            }

            return;
        }

        if (autoJump)
        {
            if (IsGrounded && rb.velocity.y < 1f && CanJump)
                Jump(jumpForce);
        }
        else if (jumpInput && WasGrounded && CanJump)
            Jump(jumpForce);

        if (rootInput && IsGrounded)
            Root(true);

        Move();
    }

    public void OnMove(Vector2 _moveInput)
    {
        moveInput = _moveInput;
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        moveInput = _context.ReadValue<Vector2>();
    }

    public void OnJump()
    {
        jumpInput = true;
    }

    public void OnJump(InputAction.CallbackContext _context)
    {
        if (_context.started)
            jumpInput = true;
        else if (_context.canceled)
            jumpInput = false;
    }

    public void OnRoot()
    {
        rootInput = true;
    }

    public void OnRoot(InputAction.CallbackContext _context)
    {
        if (_context.started)
            rootInput = true;
        else if (_context.canceled)
            rootInput = false;
    }

    public void Move()
    {
        Vector2 newVelocity = rb.velocity;

        newVelocity.x = Mathf.Lerp(rb.velocity.x, moveInput.x * speed, lerpSpeed * Time.deltaTime);
        newVelocity.y = Mathf.Lerp(rb.velocity.y, -gravity, gravityLerpSpeed * Time.deltaTime);

        rb.velocity = newVelocity;
    }

    public void Jump(float _force)
    {
        rb.AddForce(Vector2.up * _force, ForceMode.Impulse);
        jumpInput = false;
        lastJumpTime = Time.time;
    }

    public void JumpOnMushroom()
    {
        Jump(mushroomJumpForce);
    }

    public void JumpOnMosquito()
    {
        Jump(mosquitoJumpForce);
    }

    public void Root(bool _root)
    {
        isRooted = _root;

        if (_root)
        {
            rootInput = false;
            rb.velocity = Vector2.zero;
        }

        camController.Multiplier = _root ? rootCameraSpeedMultiplier : 1f;
    }

    public void TakeDamage()
    {
        Health--;

        UpdateIcons();

        if (Health <= 0)
            Die();
    }

    public void Heal()
    {
        Health++;

        UpdateIcons();

        if (Health > maxHealth)
            Health = maxHealth;
    }

    public void UpdateIcons()
    {
        for (int i = 0; i < healthIcons.Count; i++)
        {
            if (i < Health)
                healthIcons[i].SetActive(true);
            else
                healthIcons[i].SetActive(false);
        }
    }

    public void WindHit()
    {
        if (isRooted) return;

        TakeDamage();
    }

    public void Die()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Loser");
        Cursor.lockState = CursorLockMode.Confined;
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
