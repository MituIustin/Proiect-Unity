using UnityEngine;

public class OpenDoorTrigger : MonoBehaviour
{
    public GameObject door;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Key")
        {
            door.GetComponent<OpenDoor>().isLocked = false;
        }
    }
}
