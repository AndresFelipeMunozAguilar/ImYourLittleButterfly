using UnityEngine;

public class PusherAnimation : MonoBehaviour
{

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if( animator != null )
        {
            if( Input.GetMouseButton(0) )
            {
                animator.SetBool("isPressing", true);
            }else{
                animator.SetBool("isPressing", false);
            }
        }
    }
}
