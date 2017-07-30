using UnityEngine;


public class Drone : MonoBehaviour
{
    public int PowerValue = 100;
    public int MaxPowerValue = 100;

    public Events.Int OnPowerLevelChanged;

    void Start()
    {
        var powerIndicator = FindObjectOfType<PowerIndicator>();
        if (powerIndicator != null)
        {
            OnPowerLevelChanged.AddListener(powerIndicator.OnPowerLevelChanged);
            powerIndicator.OnPowerLevelChanged(PowerValue);
        }
    }

    void OnDestroy()
    {
        var powerIndicator = FindObjectOfType<PowerIndicator>();
        if (powerIndicator != null)
            OnPowerLevelChanged.RemoveListener(powerIndicator.OnPowerLevelChanged);
    }

    public void OnAbandon()
    {
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