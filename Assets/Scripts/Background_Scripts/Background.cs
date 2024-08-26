using UnityEngine;

public class Background : MonoBehaviour
{
    private void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}
