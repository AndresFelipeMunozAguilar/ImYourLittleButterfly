using System.Collections;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public enum MovementType { Patrol, Loop}

    [Header("Movimiento")]
    [SerializeField] private Vector2 movementRange;
    [SerializeField] private float speed;
    [SerializeField] private MovementType movementType;
    [SerializeField ]private float resetDelay = 0;

    private Vector2 initialPosition;
    private Vector2 destinyPosition;

    private void Start()
    {
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
                // Esperar antes de cambiar de direccion
                yield return new WaitForSeconds(resetDelay);

                //Calcular el siguiente punto
                CalculateNextDestiny();
            }

            yield return null;
        }
    }

    private IEnumerator LoopMoveCourtine()
    {
        while (true)
        {
            //Mover al enemigo
            transform.position = Vector2.MoveTowards(transform.position, destinyPosition, speed * Time.deltaTime);

            //Verificar si ya llego a la posicion destino
            if ((Vector2)transform.position == destinyPosition)
            {
                // Esperar antes de teletransportarse a la posición inicial
                yield return new WaitForSeconds(resetDelay);

                // Teletransportarse a la posición inicial
                transform.position = initialPosition;
            }

            yield return null;
        }
    }
}
