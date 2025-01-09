using UnityEngine;

public class HitScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            //collision.gameObject.TakeDamage(damage);
            Debug.Log("hit!\n");
        }
    }
}
