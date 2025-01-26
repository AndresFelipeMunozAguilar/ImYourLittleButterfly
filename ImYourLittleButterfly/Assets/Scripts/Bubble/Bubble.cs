using UnityEngine;

public class Bubble : MonoBehaviour
{

    public bool isActive = true;

    private Rigidbody2D rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PushBubble(Vector3 pusherPosition, float pushForce)
    {

        /* Obtenemos un vector que es igual
        a la diferencia de posiciones entre el
        objeto que empuja y la burbuja*/
        Vector3 posDiff = new Vector3(transform.position.x - pusherPosition.x, transform.position.y - pusherPosition.y, 0);

        // Normalizamos el vector y lo 
        posDiff.Normalize();

        // Multiplicamos el vector por una fuerza
        posDiff *= pushForce * Time.deltaTime;

        // Aplicamos la fuerza al objeto
        rigidBody.AddForce(posDiff, ForceMode2D.Impulse);
    }

}
