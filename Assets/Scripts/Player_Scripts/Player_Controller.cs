using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private Vector2 moveInput;
    private Touching_Directions touching_Directions;

    [Header("Static")]
    [Space]
    public float speed = 5f;
    public float jump = 6f;
    public float slideSpeed = 2f;

    [Space]

    [Header("Booleans")]
    [Space]
    public bool canMove = true;
    public bool wallSlide = false;
    public bool sliding = false;
    public bool wallJump = false;

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
        Move(moveInput);

        if (!touching_Directions.OnGraund && touching_Directions.OnWall)
        {
            if (rigidbody.velocity.y > 0)
            {
                wallJump = false;
                return;
            }

            if ((moveInput.x >= 0 && touching_Directions.OnRight) || (moveInput.x <= 0 && touching_Directions.OnLeft))
            {
                if (!wallSlide)
                {
                    sliding = false;
                }
                wallSlide = true;
            }
            else
            {
                wallSlide = false;
            }

            if (wallSlide && !sliding)
            {
                sliding = true;
            }

            if (wallSlide)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -slideSpeed);
            }
        }
    }

    private void Move(Vector2 direction)
    {
        if (!canMove)
        {
            return;
        }

        if (wallSlide && ((moveInput.x > 0 && touching_Directions.OnLeft) || (moveInput.x < 0 && touching_Directions.OnRight)))
        {
            rigidbody.velocity = new Vector2(default, rigidbody.velocity.y);
        }
        else if (!wallJump)
        {
            rigidbody.velocity = new Vector2(direction.x * speed, rigidbody.velocity.y);
        }
    }

    private void Jump(Vector2 direction)
    {
        rigidbody.AddForce(direction * jump, ForceMode2D.Impulse);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        speed = 5f;
        moveInput = context.ReadValue<Vector2>();
        SetFacingDirection(moveInput);
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
            else if (touching_Directions.OnWall)
            {
                WallJump();
            }
        }
    }

    private void WallJump()
    {
        StartCoroutine(DisableMovement(0.05f));
        Vector2 wallDirection = touching_Directions.OnWall ? Vector2.right : Vector2.left;
        Jump(Vector2.up + wallDirection);
        
        wallJump = true;
    }    

    private IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
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
