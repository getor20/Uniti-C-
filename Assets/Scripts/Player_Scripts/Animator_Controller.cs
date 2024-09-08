using UnityEngine;

public class Animator_Controller : MonoBehaviour
{
    private Animation _animation;
    private Touching_Directions _touchingDirections;
    private Player_Controller _playerController;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
        _touchingDirections = GetComponent<Touching_Directions>();
        _playerController = GetComponent<Player_Controller>();
    }

    private void Update()
    {
        
    }

    public void Flip(float x)
    {
        if ((x > 0 && transform.localScale.x < 0) || (x < 0 && transform.localScale.x > 0))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}
