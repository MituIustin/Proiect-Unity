using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        slider = GameObject.FindGameObjectWithTag("VolumeSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider == null)
        {
            slider = GameObject.FindGameObjectWithTag("VolumeSlider").GetComponent<Slider>();
        }
        GetComponent<TMP_Text>().text = ((int)(slider.value * 100)).ToString();
        
    }
}
