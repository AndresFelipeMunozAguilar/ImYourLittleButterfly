using UnityEngine;

public class PusherMovement : MonoBehaviour
{

    public Camera camera;

    private float objectWidth = 0.5f;
    private float objectHeight = 0.5f;

    private Vector3 screenBounds;

    private Vector3 mousePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the camera bounds using ScreenToWorldPoint
        camera = Camera.main;

        /* 
        Calculate the screen bounds, through the width
        and height of the screen, and the camera's position
        */
        screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));

        // Get this object's dimensions and divide them by 2
        // this allows us to get the center of the object
        objectWidth = transform.GetComponent<Renderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<Renderer>().bounds.size.y / 2;



    }

    // Update is called once per frame
    void Update()
    {
        FollowCursor();
    }

    void FollowCursor()
    {
        // Get the mouse position
        mousePosition = Input.mousePosition;

        // Convert the mouse position to world position
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Set z to 0 to keep it on the same plane   
        mousePosition.z = 0f;

        /* 
        Clamp the x and y positions of the object to the screen bounds
        */
        mousePosition.x = Mathf.Clamp(mousePosition.x, (screenBounds.x * -1) + objectWidth, screenBounds.x - objectWidth);
        mousePosition.y = Mathf.Clamp(mousePosition.y, (screenBounds.y * -1) + objectHeight, screenBounds.y - objectHeight);

        // Set the object's position to the mouse position
        transform.position = mousePosition;
    }

}
