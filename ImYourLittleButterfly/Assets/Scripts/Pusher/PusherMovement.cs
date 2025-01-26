using UnityEngine;

public class PusherMovement : MonoBehaviour
{

    //=========== Limitar objeto a los límites de la pantalla ===========
    public Camera camera;

    private float objectWidth = 0.5f;
    private float objectHeight = 0.5f;

    private float screenWidth;
    private Vector3 screenBounds;

    public float offSet = 0.47f;
    private Vector3 mousePosition;

    //=======================================================


    //=========== Empujar burbuja ===========

    private Bubble bubble;

    public float pushForce = 10f;
    //=======================================================


    // Start se llama una vez antes de la primera ejecución de Update después de que se crea el MonoBehaviour
    void Start()
    {
        // Obtener los límites de la cámara usando ScreenToWorldPoint
        camera = Camera.main;



        // Obtener las dimensiones de este objeto y dividirlas por 2
        // esto nos permite obtener el centro del objeto
        objectWidth = transform.GetComponent<Renderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<Renderer>().bounds.size.y / 2;

        /* Calcular los límites de la pantalla, a través del ancho
        y la altura de la pantalla, y la posición de la cámara*/

        screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));


        // =========== Empujar burbuja ===========
        // Primero, encontramos el objeto Bubble en la escena
        bubble = FindFirstObjectByType<Bubble>();


    }

    // Update se llama una vez por cuadro
    void Update()
    {
        FollowCursor();


        // Si el botón izquierdo del ratón se mantiene presionado
        if (Input.GetMouseButton(0))
        {
            PushBubble();
        }
    }

    private void FollowCursor()
    {
        // Obtener la posición del ratón
        mousePosition = Input.mousePosition;

        // Convertir la posición del ratón a posición en el mundo
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Establecer z a 0 para mantenerlo en el mismo plano   
        mousePosition.z = 0f;


        /* 
        Limitar las posiciones x e y del objeto a los límites de la pantalla
        */
        mousePosition.x = Mathf.Clamp(mousePosition.x, camera.transform.position.x - screenBounds.x + objectWidth, camera.transform.position.x + screenBounds.x - objectWidth);
        mousePosition.y = Mathf.Clamp(mousePosition.y, (screenBounds.y * -1) + objectHeight + offSet, screenBounds.y - objectHeight + offSet);

        // Establecer la posición del objeto a la posición del ratón
        transform.position = mousePosition;
    }

    private void PushBubble()
    {
        // Empujar la burbuja 
        Debug.Log("la burbuja esat activa? " + bubble.isActive);

        if (bubble.isActive)
        {
            bubble.PushBubble(transform.position, pushForce);
        }

    }

}
