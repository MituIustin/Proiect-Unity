using UnityEngine;

public class MiniGameKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Bravo )");
        }
    }
}
