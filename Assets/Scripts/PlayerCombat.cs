using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator animator;
    public GameObject hitDetection;

    float lastClickedTime = 0f;
    float doubleClickThreshold = 0.5f;

    int health = 100;

    bool canHit = true;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canHit)
        {
            if (Time.time - lastClickedTime <= doubleClickThreshold)
            {
                if (!animator.GetBool("IsAttackingTwice"))
                {
                    animator.SetBool("IsAttackingTwice", true);
                }
                else
                {
                    animator.SetBool("IsAttackingTwice", false);
                }
            }
            else
            {
                animator.SetBool("IsAttackingTwice", false);
            }

            hitDetection.SetActive(true);
            animator.SetBool("IsAttacking", true);
            lastClickedTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttacking", false);
            hitDetection.SetActive(false);
        }
    }
    
    public int GetHealth() { return health; }  

    public void SetCanHit(bool newCanHit) { canHit = newCanHit; }
}
