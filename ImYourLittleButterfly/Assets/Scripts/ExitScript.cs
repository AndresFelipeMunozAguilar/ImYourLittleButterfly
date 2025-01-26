using UnityEngine;

public class ExitScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo del mouse
        {
            QuitGame(); 
        }
    }

    void QuitGame()
    {
        Debug.Log("🚪 Saliendo del juego..."); // Mensaje en consola para pruebas
        Application.Quit(); // Cierra el juego en la versión compilada

        // NOTA: Application.Quit() NO funciona en el editor de Unity.
        // Para simular la salida en el editor se usa el siguiente código:
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
