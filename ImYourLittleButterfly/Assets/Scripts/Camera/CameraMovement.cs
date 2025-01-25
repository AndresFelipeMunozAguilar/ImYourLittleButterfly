using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    // ============= Follow Bubble =============
    private Bubble bubble;

    private Vector3 bubbleXPosition;

    public float lerpInterpolation = 0.1f;

    // ========================================


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubble = FindFirstObjectByType<Bubble>();

    }

    // Update is called once per frame
    void Update()
    {
        FollowBubble();
    }

    private void FollowBubble()
    {
        if (bubble != null)
        {
            bubbleXPosition = new Vector3(bubble.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, bubbleXPosition, lerpInterpolation);
        }
    }
}
