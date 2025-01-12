using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ArrowTrap : MonoBehaviour
{
    Animator animator;
    public GameObject arrowPrefab;

    Vector2 offset = new Vector2(0, -0.6f);
    
    bool isShooting;

    float rotationZ;
    public float shootingDelay;
    public float startDelay;

    float animationDuration = 0.5f;

    private void Awake()
    {
        rotationZ = transform.eulerAngles.z;
        Debug.Log(rotationZ);
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(ShootingCoroutine());
    }

    IEnumerator ShootingCoroutine()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            if (!isShooting)
            {
                animator.SetBool("isShooting", true);
                yield return new WaitForSeconds(animationDuration);
                isShooting = true;
                Shoot();
            }
            else
            {
                animator.SetBool("isShooting", false);
                yield return new WaitForSeconds(shootingDelay);
                isShooting = false;
            }
        }
    }

    void Shoot()
    {
        Vector3 spawnPosition = transform.position;

        Vector3 rotatedOffset = (Quaternion.Euler(0, 0, rotationZ) * offset);

        GameObject arrow = Instantiate(arrowPrefab, spawnPosition + rotatedOffset, Quaternion.Euler(0, 0, rotationZ));
    }
}
