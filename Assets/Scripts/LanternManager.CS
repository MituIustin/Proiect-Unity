using UnityEngine;

public class LanternManager : MonoBehaviour
{
    public GameObject lanternLight; // Referință la obiectul de lumină (lanterna)

    private void OnEnable()
    {
        NPC.OnDialogEnded += ActivateLantern;
    }

    private void OnDisable()
    {
        NPC.OnDialogEnded -= ActivateLantern;
    }

    private void ActivateLantern()
    {
        if (lanternLight != null)
        {
            lanternLight.SetActive(true);
            Debug.Log("Lantern activated!");
        }
        else
        {
            Debug.LogWarning("Lantern light reference is missing!");
        }
    }
}
