using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private static bool animationPlayed = false;

    private void Start()
    {

        if (!animationPlayed)
        {
            animator.SetTrigger("start");
            animationPlayed = true;
        }
    }

}
