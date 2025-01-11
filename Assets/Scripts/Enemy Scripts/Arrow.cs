using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    private Vector2 direction;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, 3f); 
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerCombat playerCombat = other.gameObject.GetComponent<PlayerCombat>(); 
            if (playerCombat != null)
            {
                playerCombat.TakeDamage(damage); 
            }
        }

        Destroy(gameObject); 
    }
}
