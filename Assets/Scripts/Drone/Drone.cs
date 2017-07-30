using UnityEngine;


public class Drone : MonoBehaviour
{
    public int PowerValue = 100;

    public Events.Int OnPowerLevelChanged;

    void Start()
    {
        var powerIndicator = FindObjectOfType<PowerIndicator>();
        if (powerIndicator != null)
            OnPowerLevelChanged.AddListener(powerIndicator.OnPowerLevelChanged);
    }

    void OnDestroy()
    {
        var powerIndicator = FindObjectOfType<PowerIndicator>();
        if (powerIndicator != null)
            OnPowerLevelChanged.RemoveListener(powerIndicator.OnPowerLevelChanged);
    }

    public int Power
    {
        get { return PowerValue; }
        set
        {
            PowerValue = value;
            OnPowerLevelChanged.Invoke(PowerValue);
        }
    }
}