using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetUI()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
            PickUp();
    }

    public abstract void PickUp();
}
