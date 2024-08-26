using UnityEngine;

public class Touching_Directions : MonoBehaviour
{
    private BoxCollider2D TouchingBoxCollider;

    public bool IsGraund { get; private set; }
    public bool IsGraundLeft { get; private set; }
    public bool IsGraundRight { get; private set; }

    private RaycastHit2D[] graundHits= new RaycastHit2D[5];
    private float graundDistans = 0.05f;

    private void Awake()
    {
        TouchingBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        IsGraund = TouchingBoxCollider.Cast(Vector2.down, graundHits, graundDistans) > 0;
        IsGraundLeft = TouchingBoxCollider.Cast(Vector2.left, graundHits, graundDistans) > 0;
        IsGraundRight = TouchingBoxCollider.Cast(Vector2.right, graundHits, graundDistans) > 0;
    } 
}
