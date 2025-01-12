using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public abstract class BaseItem : MonoBehaviour
{
    public Sprite sprite;
    public GameObject UI;
    GameObject _UI;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetUI(bool already)
    {
        if (already)
        {
            Destroy(_UI.gameObject);
        }
        _UI = Instantiate(UI);
        _UI.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        _UI.transform.parent=GameObject.FindGameObjectWithTag("buffpanel").transform;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
            PickUp();
    }

    public abstract void UseEffect();
    public abstract void PickUp();

    public abstract int GetPrice();

    public abstract bool AlreadyHasThisBoost();
}
