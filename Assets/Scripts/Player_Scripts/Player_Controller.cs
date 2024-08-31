using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    public float speed = 5f;
    public float jump = 6f;

    private Vector2 movementDirection = Vector2.zero;
    private Touching_Directions Touching_Directions;

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
        Touching_Directions = GetComponent<Touching_Directions>();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(movementDirection.x * speed, rigidbody.velocity.y);
        
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
        
        if (Touching_Directions.IsGraund || Touching_Directions.IsGraundRight || Touching_Directions.IsGraundLeft)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump);
        }
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
