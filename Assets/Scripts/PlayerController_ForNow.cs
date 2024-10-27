using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;       // Viteza de mișcare
    public Terrain terrain;            // Referința către Terrain

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();   // Obține componenta Rigidbody

        if (terrain == null)
        {
            // Găsește automat terenul în scenă dacă nu este setat din Inspector
            terrain = FindObjectOfType<Terrain>();
        }
    }

    void Update()
    {
        // Citirea inputului pentru mișcare
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculăm direcția de mișcare
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed * Time.deltaTime;

        // Calculăm poziția dorită
        Vector3 targetPosition = transform.position + movement;

        // Obținem înălțimea terenului la poziția player-ului
        float terrainHeight = terrain.SampleHeight(targetPosition) + terrain.GetPosition().y;

        // Setăm noua poziție cu înălțimea ajustată
        targetPosition.y = terrainHeight;

        // Mutăm player-ul la noua poziție calculată
        rb.MovePosition(targetPosition);
    }
}
