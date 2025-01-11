using UnityEngine;

public class OpenChest : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            animator.SetBool("Player_Nearby", true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            animator.SetBool("Player_Nearby", false);
        }
    }
}
