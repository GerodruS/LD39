using UnityEngine;
using UnityEngine.UI;


public class PowerIndicator : MonoBehaviour
{
    [SerializeField]
    string Format;
    [SerializeField]
    Text Label;
    
    void Awake()
    {
        FindObjectOfType<DroneController>().OnPowerLevelChanged.AddListener(OnPowerLevelChanged);
    }

    public void OnPowerLevelChanged(float value)
    {
        if (Label != null)
        {
            Label.text = string.Format(Format, value.ToString("0.0"));
        }
    }
}
