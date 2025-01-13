using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    bool _stillSpawning;
    Camera _camera;
    PlayerMovement _player;
    public GameObject _enemy1;
    public GameObject _enemy2;
    public GameObject _enemy3;
    bool _pauseSpawning;
    int _spawned;
    string _sceneName;
    int _radius;

    void Start()
    {
        _stillSpawning = true;
        _pauseSpawning = false;
        _spawned = 0;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _sceneName= SceneManager.GetActiveScene().name;

        if (_sceneName == "FirstLevel")
            _radius = 30;
        else
            _radius = 50;
    }

    void Update()
    {
        if (_stillSpawning && !_pauseSpawning)
        {
            SpawnEnemy();
            _spawned++;
        }
        if (_spawned > 2)
        {
            StartCoroutine(PauseSpawning());
            _spawned = 0;
        }

    }

    bool IsOutOfMap(Vector3 postion)
    {
        if(_sceneName == "FirstLevel")
            return (postion.z < -15) || (postion.z > 0)
                    || (postion.x > 50) || (postion.x < 5);
        else
            return (postion.z < -1.5) || (postion.z > 6)
                    || (postion.x > 50) || (postion.x < 13);
    }
    bool IsVisible(Vector3 worldPosition)
    {
        if (IsOutOfMap(worldPosition))
        {
            return true;
        }

        Vector3 viewportPoint = _camera.WorldToViewportPoint(worldPosition);
        
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
               viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
               viewportPoint.z > 0;
    }

    private Vector3 getLocationForSpawn()
    {
            Vector3 spawnPosition;
            do
            {
                spawnPosition = _player.transform.position + Random.insideUnitSphere * _radius;
                spawnPosition.y = _player.transform.position.y; 
            } while (IsVisible(spawnPosition));

            return spawnPosition;
    }
    private void SpawnEnemy() {
        var randomChoice = Random.Range(0, 3);
        if (randomChoice == 0)
        {
            var enemy = Instantiate(_enemy1);
            enemy.transform.position = getLocationForSpawn();
        }
        else
        {
            if(randomChoice == 1)
            {
                var enemy = Instantiate(_enemy2);
                enemy.transform.position = getLocationForSpawn();
            }
            else
            {
                var enemy = Instantiate(_enemy3);
                enemy.transform.position = getLocationForSpawn();
            }
        }
    }

    private IEnumerator PauseSpawning()
    {
        _pauseSpawning = true;
        yield return new WaitForSeconds(5);
        _pauseSpawning = false;
    }

    public void SetSpawn()
    {
        _stillSpawning = false;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies) {
            if (enemy.GetComponent<MeleeEnemy>() != null)
            {
                enemy.GetComponent<MeleeEnemy>().Die();
            }
            if (enemy.GetComponent<RangedEnemy>() != null)
            {
                enemy.GetComponent<RangedEnemy>().Die();
            }
        }
    }
}
