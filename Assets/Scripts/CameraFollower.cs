using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    float Factor = 0.75f;
    [SerializeField]
    Transform TargetTransform;
    
    void Start()
    {
        
    }

    void Update()
    {
        var currentPosition = transform.position;
        var targetPosition = TargetTransform.position;
        targetPosition.z = currentPosition.z;
        var t = Factor * Time.deltaTime;
        transform.position = Vector3.Lerp(currentPosition, targetPosition, t);
    }
}
