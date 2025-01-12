using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < -15)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game Over!");

            
            GameObject obstacleSpawner = GameObject.Find("Obstacle Spawnner");
            if (obstacleSpawner != null)
            {
                ScoreManager scoreManager = obstacleSpawner.GetComponent<ScoreManager>();
                if (scoreManager != null)
                {
                    scoreManager.GameOver();
                }
            }
        }
    }
}
