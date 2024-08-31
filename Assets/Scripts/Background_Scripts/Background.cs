using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        transform.position = target.position;
    }
}
