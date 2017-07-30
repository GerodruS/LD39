using UnityEngine;
using UnityEngine.UI;


public class PowerIndicator : MonoBehaviour
{
    [SerializeField]
    string Format;
    [SerializeField]
    Text Label;

    public void OnPowerLevelChanged(int value)
    {
        if (Label == null) return;
        Label.text = string.Format(Format, value);
    }
}