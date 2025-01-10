using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject optionsPrefab;
    GameObject options;
    void Start()
    {
        options = Instantiate(optionsPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
