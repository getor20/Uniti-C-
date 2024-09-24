using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rigidBody { get; private set; }
    private Touching_Directions touching_Directions;
    private Animator_Controller animator_Controller;


    private Vector2 moveInput;

    [Header("Static")]
    [Space]
    public float speed = 5f;
    public float plusSpeed = 7f;
    public float jump = 5f;
    public float slideSpeed = 1f;
    public float wallSlideLerp = 10f;

    [Space]

    [Header("Booleans")]
    [Space]
    public bool canMove = true;
    public bool wallSlide = false;
    public bool sliding = false;
    public bool wallJump = false;

    public bool IsWalking { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsCrouch { get; private set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        touching_Directions = GetComponent<Touching_Directions>();
        animator_Controller = GetComponent<Animator_Controller>();
    }

    private void FixedUpdate()
    {
        Move();
        WallSlide();
        OnGround();
    }


    private void Move()
    {
        if (!canMove)
        {
            return;
        }

        float currentSpeed = IsRunning ? plusSpeed : speed;


        if (wallSlide && IsMovingTowardsWall())
        {
            rigidBody.velocity = new Vector2(default, rigidBody.velocity.y);
        }
        else if (!wallJump)
        {
            rigidBody.velocity = new Vector2(moveInput.x * currentSpeed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, new Vector2(moveInput.x * currentSpeed, rigidBody.velocity.y), wallSlideLerp * Time.fixedDeltaTime);
        }

        if (moveInput.x != 0 && !wallSlide)
        {
            animator_Controller.Flip(moveInput.x);
        }
    }

    private void WallSlide()
    {
        if (!touching_Directions.OnGraund && touching_Directions.OnWall)
        {
            if (rigidBody.velocity.y > 0)
            {
                wallJump = false;
                return;
            }

            if(IsMovingTowardsWall())
            {
                StartWallSlide();
            }
            else
            {
                StopWallSlide();
            }
        }
        else
        {
            StopWallSlide();
        }
    }

    private bool IsMovingTowardsWall()
    {
        return ((moveInput.x > 0 && touching_Directions.OnRight) || (moveInput.x < 0 && touching_Directions.OnLeft));
    }

    private void StartWallSlide()
    {
        if (!wallSlide)
        {
            rigidBody.velocity = Vector2.zero;
            sliding = true;
        }

        wallSlide = true;
        rigidBody.velocity = new Vector2(0, -slideSpeed);
    }

    private void StopWallSlide()
    {
        wallSlide = false;
        sliding = false;

    }

    private void OnGround()
    {
        if (touching_Directions.OnGraund)
        {
            wallSlide = false;
            sliding = false;
            wallJump = false;
        }
    }

    private void Jump(Vector2 direction)
    {
        rigidBody.AddForce(direction * jump, ForceMode2D.Impulse);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsWalking = moveInput != Vector2.zero;
    }

    public void OnSpidplus(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsCrouch = true;
        }
        else if( context.canceled)
        {
            IsCrouch = false;
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

}
