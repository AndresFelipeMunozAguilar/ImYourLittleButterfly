using UnityEngine;

public class FanMovement : MonoBehaviour
{

    public Camera camera;
    private Vector3 bottomLeft;
    private Vector3 topRight;

    private float objectWidth = 0.5f;
    private float objectHeight = 0.5f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the camera bounds using ScreenToWorldPoint
        camera = Camera.main;
        Vector3 bottomLeft = camera.ScreenToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector3 topRight = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.nearClipPlane));


        // Get the object's dimensions
        objectWidth = transform.GetComponent<Renderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<Renderer>().bounds.size.y / 2;


        // Clamp the position to the camera bounds minus half the object's dimensions
        minX = bottomLeft.x + objectWidth;
        maxX = topRight.x - objectWidth;
        minY = bottomLeft.y + objectHeight;
        maxY = topRight.y - objectHeight;

    }

    // Update is called once per frame
    void Update()
    {
        FollowCursor();
    }

    void FollowCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f; // Set z to 0 to keep it on the same plane   


        mousePosition.x = Mathf.Clamp(mousePosition.x, minX, maxX);
        mousePosition.y = Mathf.Clamp(mousePosition.y, minY, maxY);

        transform.position = mousePosition;
    }

}
