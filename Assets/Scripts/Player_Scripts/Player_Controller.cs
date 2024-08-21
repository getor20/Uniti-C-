using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    public float speed = 5f;

    private Vector2 movementDirection = Vector2.zero;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(movementDirection.x * speed, rigidbody.velocity.y);
        spriteRenderer.flipX = movementDirection.x < 0 ? true : false;
    }      

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    public Transform player; // —сылка на игрока

    /*void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);

    }*/

    public float parallaxSpeed;
    private float startPos;
    private float length;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        float temp = (Camera.main.transform.position.x
 * (1 - parallaxSpeed));
        float
 dist = (temp - startPos);

        if (temp > startPos + length) startPos = temp;
        else if (temp < startPos - length) startPos = temp;

        transform.position = new Vector3(startPos + dist, transform.position.y);
    }

}
