using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Transform followingTarget;

    [SerializeField, Range(0f, 1f)]
    private float parallaxStrength = 0.1f;

    private Vector3 targetPreviousPosition;

    private void Start()
    {
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }

        targetPreviousPosition = followingTarget.position;
    }

    private void Update()
    {
         var delta = followingTarget.position - targetPreviousPosition;

        delta.y = 0;
        
        targetPreviousPosition = followingTarget.position;

        transform.position += delta * parallaxStrength;
    }
}
