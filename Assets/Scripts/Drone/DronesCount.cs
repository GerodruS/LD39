using UnityEngine;
using UnityEngine.UI;


public class DronesCount : MonoBehaviour
{
    [SerializeField]
    string Format;
    [SerializeField]
    Text Label;
    
    public void OnDronesCountWasChanged(int value, int max)
    {
        if (Label == null) return;
        Label.text = string.Format(Format, value, max);
    }
}