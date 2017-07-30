using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapSwitcher : MonoBehaviour
{
    [SerializeField]
    float CommonValue;
    [SerializeField]
    float MapValue;

    Camera Camera;

    void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Camera.orthographicSize = MapValue;
        }
        else
        {
            Camera.orthographicSize = CommonValue;
        }
    }
}