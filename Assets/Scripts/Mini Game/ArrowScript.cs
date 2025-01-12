using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ArrowScript : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D col;
    SpriteRenderer spriteRenderer;

    float speed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (-transform.up * speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            StartCoroutine(PlayerDie(collider));
            col.enabled = false;
            spriteRenderer.enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    IEnumerator PlayerDie(Collider2D collider)
    {
        collider.GetComponent<MiniGamePlayerMovement>().isDead = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
