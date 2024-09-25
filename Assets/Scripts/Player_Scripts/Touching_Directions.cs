using UnityEngine;

public class Touching_Directions : MonoBehaviour
{
    private BoxCollider2D TouchingBoxCollider;
    public bool OnGround { get; private set; }
    public bool OnLeft { get; private set; }
    public bool OnRight { get; private set; }

    public bool OnWall => OnLeft || OnRight;

    private RaycastHit2D[] hits= new RaycastHit2D[5];
    private float graundDistans = 0.05f;
    private float wallDistans = 0.05f;

    private void Awake()
    {

        TouchingBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        CheckGround();
        CheckWall();
    } 
    
    private void CheckGround()
    {
        OnGround = TouchingBoxCollider.Cast(Vector2.down, hits, graundDistans) > 0;
    }

    private void CheckWall()
    {
        if (!OnGround)
        {
            OnLeft = TouchingBoxCollider.Cast(Vector2.left, hits, wallDistans) > 0;
            OnRight = TouchingBoxCollider.Cast(Vector2.right, hits, wallDistans) > 0;
        }
        else
        {
            OnLeft = false;
            OnRight = false;
        }
    }

}
