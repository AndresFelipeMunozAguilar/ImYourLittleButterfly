using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Limites camara")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private Bubble bubble;
    private Vector3 bubbleXPosition;
    public float lerpInterpolation = 0.1f;
    private float cameraHalfWidth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubble = FindFirstObjectByType<Bubble>();

        // Calcular la mitad del ancho de la cámara
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
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
            //Obtener la posicion de la camara
            bubbleXPosition = new Vector3(bubble.transform.position.x, transform.position.y, transform.position.z);

            // Ajustar la posición deseada para mantener los bordes de la cámara dentro de los límites
            bubbleXPosition.x = Mathf.Clamp(bubbleXPosition.x, minX + cameraHalfWidth, maxX - cameraHalfWidth);

            transform.position = Vector3.Lerp(transform.position, bubbleXPosition, lerpInterpolation);
        }
    }
}
