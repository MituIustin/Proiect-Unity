using System.Collections;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject door;
    public GameObject key;

    float timeToDoor = 2f;
    float xOffset = 0.3f;
    float yOffset = 1f;


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

        Destroy(key);
    }
}
