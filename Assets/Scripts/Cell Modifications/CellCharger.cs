using UnityEngine;


public class CellCharger : CellModification
{
    public int Remain = -1;

    public override void OnActivate()
    {
        var field = FindObjectOfType<Field>();
        var drone = field.CurrentDrone;
        if (-1 == Remain)
        {
            drone.Power = drone.MaxPowerValue;
        }
        else
        {
            var value = Mathf.Min(drone.MaxPowerValue - drone.Power, Remain);
            drone.Power += value;
            Remain -= value;
        }
    }
}