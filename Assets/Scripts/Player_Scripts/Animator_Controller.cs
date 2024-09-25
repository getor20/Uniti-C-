using Assets.Scripts.Player_Scripts;
using UnityEngine;

public class Animator_Controller : MonoBehaviour
{
    private Animator _animator;
    private Touching_Directions _touchingDirections;
    private Player_Controller _playerController;

    private int _isWalkingHash;
    private int _isRunningHash;
    private int _isGroundedHash;
    private int _isCrouchHash;
    private int _yVelocityHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<Touching_Directions>();
        _playerController = GetComponent<Player_Controller>();

        CacheAnimationHashes();
    }

    private void CacheAnimationHashes()
    {

        _isWalkingHash = Animator.StringToHash(PlayerAnimationStrings.IsWalking);
        _isRunningHash = Animator.StringToHash(PlayerAnimationStrings.IsRunning);
        _isGroundedHash = Animator.StringToHash(PlayerAnimationStrings.IsGround);
        _isCrouchHash = Animator.StringToHash(PlayerAnimationStrings.IsCrouch);
        _yVelocityHash = Animator.StringToHash(PlayerAnimationStrings.yVelocity);
    }

    private void Update()
    {
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        _animator.SetBool(_isRunningHash, _playerController.IsRunning);
        _animator.SetBool(_isWalkingHash, _playerController.IsWalking);
        _animator.SetBool(_isGroundedHash, _touchingDirections.OnGraund);
        _animator.SetBool(_isCrouchHash, _playerController.IsCrouch);
        _animator.SetFloat(_yVelocityHash, _playerController.rigidBody.velocity.y);
    }

    public void Flip(float x)
    {
        if ((x > 0 && transform.localScale.x < 0) || (x < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void AnimateJump()
    {
        _animator.SetTrigger(PlayerAnimationStrings.Jump);
    }
}
