using UnityEngine;
using UnityEngine.UI;

public class ComputerManager : MonoBehaviour
{
    [Header("Elementos que se activarán al encender")]
    [SerializeField] private GameObject[] objectsToActivate; // Arrastra aquí los objetos como el diálogo
    [Header("Botón de encendido")]
    [SerializeField] private Button powerButton; // Asigna el botón desde el Inspector
    [Header("Efecto de sonido")]
    [SerializeField] private AudioClip powerOnSound; 
    private AudioSource audioSource; 


    private bool isOn = false; // Estado de la "computadora"

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtiene el AudioSource
        
        // Asegurarnos de que todo esté apagado al inicio
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        // Asignar el evento al botón de encendido
        powerButton.onClick.AddListener(TurnOnComputer);
    }

    void TurnOnComputer()
    {
        if (isOn) return; // Evita que se active varias veces

        isOn = true;
        Debug.Log("💻 Computadora encendida!");

        // Reproducir sonido
        if (powerOnSound && audioSource) 
        {
            audioSource.PlayOneShot(powerOnSound); // Reproduce el sonido de encendido
        }


        // Activar los elementos del juego
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }

        // Opcional: Desactivar el botón después de encender
        powerButton.gameObject.SetActive(false);
    }
}
