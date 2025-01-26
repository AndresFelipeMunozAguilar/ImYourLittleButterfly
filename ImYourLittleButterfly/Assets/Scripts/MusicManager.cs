using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance; // Variable para guardar la instancia única

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Asigna esta instancia
            DontDestroyOnLoad(gameObject); // Evita que se destruya al cambiar escena
        }
        else
        {
            Destroy(gameObject); // Si ya existe, destruye el duplicado
        }
    }
}
