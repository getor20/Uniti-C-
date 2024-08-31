using UnityEngine;

public class Touching_Directions : MonoBehaviour
{
    private BoxCollider2D TouchingBoxCollider;

    public bool OnGraund { get; private set; }
    public bool IsLeft { get; private set; }
    public bool IsRight { get; private set; }

    public bool OnWall => IsLeft || IsRight;

    private RaycastHit2D[] hits= new RaycastHit2D[5];
    private float graundDistans = 0.05f;
    private float wallDistans = 0.05f;

    private void Awake()
    {

        TouchingBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        OnGraund = TouchingBoxCollider.Cast(Vector2.down, hits, graundDistans) > 0;
        if (!OnGraund)
        {
            IsLeft = TouchingBoxCollider.Cast(Vector2.left, hits, wallDistans) > 0;
            IsRight = TouchingBoxCollider.Cast(Vector2.right, hits, wallDistans) > 0;
        }
        else
        {
            IsLeft = false;
            IsRight = false;
        }
    } 
}
