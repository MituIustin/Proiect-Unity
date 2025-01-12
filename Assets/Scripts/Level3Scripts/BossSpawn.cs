using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject _enemy1;
    public float spawnOffset = 5f; 

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;

            Vector3 spawnPosition = new Vector3(playerPosition.x + spawnOffset, playerPosition.y, playerPosition.z);

            var enemy = Instantiate(_enemy1, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    void Update()
    {

    }
}
