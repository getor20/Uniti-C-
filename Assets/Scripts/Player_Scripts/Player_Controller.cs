using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private Vector2 movementDirection;
    private Touching_Directions touching_Directions;

    [Header("Static")]
    [Space]
    public float speed = 5f;
    public float jump = 6f;
    public float slideSpeed = 2f;

    [Space]

    [Header("Booleans")]
    [Space]
    public bool canMove;
    public bool wallSlide;
    public bool sliding;
    public bool wallJump;

    private bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get{ return _isFacingRight; }   
        private set 
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        touching_Directions = GetComponent<Touching_Directions>();
    }

    private void FixedUpdate()
    {
        Move(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        if (!canMove)
        {
            return;
        }
        rigidbody.velocity = new Vector2(movementDirection.x * speed, rigidbody.velocity.y);
    }

    private void Jump(Vector2 direction)
    {
        rigidbody.AddForce(direction * jump, ForceMode2D.Impulse);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        speed = 5f;
        movementDirection = context.ReadValue<Vector2>();
        SetFacingDirection(movementDirection);
    }

    public void Acceleration(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            speed = 10;
        }
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (touching_Directions.OnGraund)
            {
                Jump(Vector2.up);
            }
            else if (touching_Directions.OnWall)
            {
                WallJump();
            }
        }
    }

    private void WallJump()
    {
        canMove = true;

        Vector2 wallDirection = touching_Directions.OnWall ? Vector2.right : Vector2.left;
        Jump(Vector2.up + wallDirection);
        
        wallJump = true;
        canMove = false;
    }    

    private void SetFacingDirection(Vector2 direction)
    {
        if (direction.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (direction.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
   
}
