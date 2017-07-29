using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DroneController : MonoBehaviour
{
    [SerializeField]
    float Speed = 100.0f;
    [SerializeField]
    float Torque = 100.0f;
    [SerializeField]
    float PowerLevel = 100.0f;
    [SerializeField]
    float RotatingPowerUsage = 1.0f;
    [SerializeField]
    float MovingPowerUsage = 2.0f;

    public Events.Float OnPowerLevelChanged;
    
    Rigidbody2D Rigidbody2D;
    
    void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dronePosition = transform.position;
        var direction = mousePosition - dronePosition;
        direction.z = 0.0f;
        direction.Normalize();
        if (Input.GetMouseButton(0))
        {
            var angle = Quaternion.FromToRotation(transform.right, direction).eulerAngles.z;
            if (180.0f < angle)
                angle -= 360.0f;
            var value = Time.deltaTime * Torque * angle;
            Rigidbody2D.AddTorque(value);
            PowerLevel -= Mathf.Abs(value) * RotatingPowerUsage;
        }
        if (Input.GetMouseButton(1))
        {
            var value = Time.deltaTime * Speed;
            Rigidbody2D.AddForce(transform.right * value);
            PowerLevel -= value * MovingPowerUsage;
        }
        OnPowerLevelChanged.Invoke(PowerLevel);
    }
}