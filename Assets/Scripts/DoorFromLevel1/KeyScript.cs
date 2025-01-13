using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyScript : MonoBehaviour
{
    GameObject door;
    public GameObject key;

    float timeToDoor = 2f;
    float xOffset = 0.3f;
    float yOffset = 1f;

    void Start()
    {
        GameObject.FindGameObjectWithTag("enemySpawner").GetComponent<EnemySpawner>().SetSpawn();
        if (SceneManager.GetActiveScene().name == "FirstLevel")
            door = GameObject.FindGameObjectWithTag("door");
        else
            door = GameObject.FindGameObjectWithTag("minigameDoor");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            key.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(Fly());
        }
    }

    private IEnumerator Fly()
    {
        Vector3 startPosition = key.transform.position; 
        Vector3 doorPosition = door.transform.position;

        Vector3 targetPosition = doorPosition + new Vector3(-xOffset, yOffset, 0);

        float elapsedTime = 0f;

        while (elapsedTime < timeToDoor)
        {
            float t = elapsedTime / timeToDoor;

            key.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        key.transform.position = targetPosition;
        if (SceneManager.GetActiveScene().name == "FirstLevel")
            GameObject.FindGameObjectWithTag("nextScene").GetComponent<GoToNextScene>().GotTheKey();
        else
        {
            var portal = Resources.Load<GameObject>("Portal");
            var portalInstantiated=Instantiate(portal);
            portalInstantiated.transform.position = new Vector3(44.73f, 0.06f, 11.51f);
        }

        Destroy(key);
    }
}
