using System.Collections;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public enum MovementType { Patrol, Loop}

    [Header("Movimiento")]
    [SerializeField] private Vector2 movementRange;
    [SerializeField] private float speed;
    [SerializeField] private MovementType movementType;
    [SerializeField] private float resetDelay = 0;
    [Header("Animacion")]
    
    private Vector2 initialPosition;
    private Vector2 destinyPosition;

    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();

        //Guardar la posicion inicial
        initialPosition = transform.position;

        //Establecer la posicion destino
        destinyPosition = initialPosition + movementRange;

        //Iniciar movimiento
        switch (movementType)
        {
            case MovementType.Patrol: StartCoroutine(PatrolMoveCoroutine());
                break;
            case MovementType.Loop: StartCoroutine(LoopMoveCourtine());
                break;
        }
    }

    private void CalculateNextDestiny()
    {
        //Comprobar si la posicion destino anterior era la misma posicion inicial
        destinyPosition = (destinyPosition == initialPosition ? initialPosition + movementRange : initialPosition);

        //Cambiar la direccion a la que mira
        ChangeOrientation();

    }

    private void ChangeOrientation()
    {
        Vector3 currentRotation = transform.eulerAngles;

        //Comprobar si se mueve en el eje X o Y, aplicar una rotación
        if (movementRange.x != 0) { currentRotation.y += (destinyPosition == initialPosition) ? -180f : 180f; }
        if (movementRange.y != 0) { currentRotation.z += (destinyPosition == initialPosition) ? -180f : 180f; }

        //Actualizar la orientación
        transform.eulerAngles = currentRotation;
    }

    private IEnumerator PatrolMoveCoroutine()
    {
        while (true)
        {
            //Mover al enemigo
            transform.position = Vector2.MoveTowards(transform.position, destinyPosition, speed * Time.deltaTime);

            //Verificar si ya llego a la posicion destino
            if ((Vector2)transform.position ==  destinyPosition)
            {
                //Ejecutar animación de pausa
                animator.SetBool("moving", false);

                // Esperar antes de cambiar de direccion
                yield return new WaitForSeconds(resetDelay);

                //Calcular el siguiente punto
                CalculateNextDestiny();

                //Ejecutar animación de movimiento
                animator.SetBool("moving", true);
            }

            yield return null;
        }
    }

    private IEnumerator LoopMoveCourtine()
    {
        while (true)
        {
            //Verificar si esta empezando
            if ((Vector2)transform.position == initialPosition)
            {
                //Esperar a que se ejecute la animación inicial
                yield return new WaitForSeconds(resetDelay);

                //Activar la animacion de movimiento
                animator.SetBool("moving", true);
            }

            //Mover al enemigo
            transform.position = Vector2.MoveTowards(transform.position, destinyPosition, speed * Time.deltaTime);

            //Verificar si ya llego a la posicion destino
            if ((Vector2)transform.position == destinyPosition)
            {
                //Ejecutar la animacion de choque
                animator.SetBool("moving", false);

                // Esperar antes de teletransportarse a la posición inicial
                yield return new WaitForSeconds(resetDelay);

                // Teletransportarse a la posición inicial
                transform.position = initialPosition;

                //Ejecutar la animación de inicio
                animator.SetTrigger("contact");
            }

            yield return null;
        }
    }
}
