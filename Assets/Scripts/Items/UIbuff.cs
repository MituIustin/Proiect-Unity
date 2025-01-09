using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class stats : MonoBehaviour
{
    private Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
        StartCoroutine(RemoveIn15());
        
    }

    void Update()
    {
    }
    private void StopBlinking()
    {
        Color color = _image.color;
        color.a = 1.0f;
        _image.color = color;
    }

    private void StartBlinking()
    {
        float alpha = 0.2f;
        Color color = _image.color;
        color.a = alpha;
        _image.color = color;
    }
    private IEnumerator RemoveIn15()
    {
        yield return new WaitForSeconds(10);
        for (float i = 0; i < 5; i = i + 0.5f)
        {
            StartBlinking();
            yield return new WaitForSeconds(0.25f);
            StopBlinking();
            yield return new WaitForSeconds(0.25f);
        }
        Destroy(gameObject);
    }


}
