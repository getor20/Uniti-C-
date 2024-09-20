using Assets.Scripts.Player_Scripts;
using UnityEngine;

public class Animator_Controller : MonoBehaviour
{
    private Animator _animator;
    private Touching_Directions _touchingDirections;
    private Player_Controller _playerController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _touchingDirections = GetComponent<Touching_Directions>();
        _playerController = GetComponent<Player_Controller>();
    }

    private void Update()
    {
        _animator.SetBool(PlayerAnimationStrings.IsRunning, _playerController.IsRunning);
        _animator.SetBool(PlayerAnimationStrings.IsWalking, _playerController.IsWalking);
        _animator.SetBool(PlayerAnimationStrings.IsGround, _touchingDirections.OnGraund);
        _animator.SetBool(PlayerAnimationStrings.IsCrouch, _playerController.IsCrouch);
        _animator.SetFloat(PlayerAnimationStrings.yVelocity, _playerController.Rigidbody.velocity.y);
        
    }

    public void Flip(float x)
    {
        if ((x > 0 && transform.localScale.x < 0) || (x < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
