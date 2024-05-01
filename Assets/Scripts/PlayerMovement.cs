using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private float jumpingPower = 5f;
    //public Vector3 jump;
    private bool isFacingRight = true;
    public bool isGrounded;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        //jump = new Vector3(0.0f, 2.0f, 0.0f);


        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpingPower, 0);
            isGrounded = false;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f, 0);
            isGrounded = false;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, 0);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}