using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public abstract class BaseItem : MonoBehaviour
{
    public Sprite sprite;
    public GameObject UI;
    GameObject _UI;
    public string _tag;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetUI()
    {
        _UI = Instantiate(UI);
        _UI.tag = _tag;
        _UI.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        _UI.transform.parent=GameObject.FindGameObjectWithTag("buffpanel").transform;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        if (collision.collider.tag == "Player")
            PickUp();
    }

    public abstract void UseEffect();
    public abstract void PickUp();

    public abstract int GetPrice();

    public abstract bool AlreadyHasThisBoost();
}
