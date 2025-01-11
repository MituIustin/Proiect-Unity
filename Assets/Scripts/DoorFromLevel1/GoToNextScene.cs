using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    bool _GotKey;
    private void Start()
    {
        _GotKey = false;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && _GotKey)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void GotTheKey()
    {
        _GotKey = true;
    }
}
