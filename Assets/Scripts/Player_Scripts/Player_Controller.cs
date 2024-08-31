using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    public float speed = 5f;
    public float jump = 6f;

    private Vector2 movementDirection = Vector2.zero;
    private Touching_Directions touching_Directions;

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
        speed = 10;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (touching_Directions.OnGraund)
            {
                Jump(Vector2.up);
            }
        }
    }

    private void WallJump()
    {
        Vector2 wallJump = touching_Directions.OnWall ? Vector2.right : Vector2.left;
        Jump(Vector2.up + wallJump);
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
