using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DroneController : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dronePosition = transform.position;
        var direction = mousePosition - dronePosition;
        if (Input.GetMouseButton(0))
        {
            var currentRotation = transform.rotation;
            var targetRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            var t = 0.9f * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, t);
        }
    }
}