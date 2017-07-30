using System;
using UnityEngine.Events;


public static class Events
{
    [Serializable]
    public class IntInt : UnityEvent<int, int>
    {
    }

    [Serializable]
    public class Float : UnityEvent<float>
    {
    }
}