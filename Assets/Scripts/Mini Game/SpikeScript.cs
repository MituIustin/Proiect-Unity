using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeScript : MonoBehaviour
{
    Animator animator;
    Collider2D boxCollider;

    bool spikesOut = false;
    public float stayOffTime;
    public float stayOutTime;
    public float startDelay;

    float animationDuration = 0.31f;


    private void Awake()
    {
        animator = GetComponent<Animator>();    
        boxCollider = GetComponent<Collider2D>();
        boxCollider.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(SpikeRoutine());
    }

    IEnumerator SpikeRoutine()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            if (!spikesOut)
            {
                animator.SetBool("spikesOut", true);
                boxCollider.enabled = true;
                yield return new WaitForSeconds(stayOutTime);
                spikesOut = true;
            }
            else
            {
                animator.SetBool("spikesOut", false);
                yield return new WaitForSeconds(animationDuration);
                boxCollider.enabled = false;
                yield return new WaitForSeconds(stayOutTime);
                spikesOut = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
