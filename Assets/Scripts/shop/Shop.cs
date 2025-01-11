using UnityEngine;

public class Shop : MonoBehaviour
{
    GameObject _player;
    public GameObject shopPrefab;
    bool _isOpened;
    void Start()
    {
        _isOpened = false;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance < 4)
        {
            CanOpenShop();
            
        }
    }

    private void CanOpenShop()
    {
        if (Input.GetKey(KeyCode.E) && !_isOpened)
        {
            _isOpened = true;
            Instantiate(shopPrefab);
            Time.timeScale = 0;
        }
    }

    public void CloseShop()
    {
        _isOpened = false;
        
        Time.timeScale = 1;
    }
}
