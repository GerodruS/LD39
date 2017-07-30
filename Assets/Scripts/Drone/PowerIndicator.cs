using UnityEngine;
using UnityEngine.UI;


public class PowerIndicator : MonoBehaviour
{
    [SerializeField]
    string Format;
    [SerializeField]
    Text Label;

    public void OnPowerLevelChanged(int value, int max)
    {
        if (Label == null) return;
        Label.text = string.Format(Format, (100.0f * value / max).ToString("00"));
    }
}