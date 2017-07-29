using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform TargetTransform;
    
    void Start()
    {
        
    }

    void Update()
    {
        var currentPosition = transform.position;
        var targetPosition = TargetTransform.position;
        targetPosition.z = currentPosition.z;
        var t = 0.2f * Time.deltaTime;
        transform.position = Vector3.Lerp(currentPosition, targetPosition, t);
    }
}
