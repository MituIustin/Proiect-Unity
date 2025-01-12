using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ArrowScript : MonoBehaviour
{
    Rigidbody2D rb;

    float speed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (-transform.up * speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        Destroy(gameObject);
    }
}
