using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    private static OverlayManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantiene el overlay al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Si ya existe uno, destruye el nuevo para evitar duplicados
        }
    }
}
