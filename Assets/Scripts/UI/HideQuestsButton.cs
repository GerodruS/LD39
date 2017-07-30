using UnityEngine;

public class HideQuestsButton : MonoBehaviour
{
    public bool IsVisible;
    public RectTransform RectTransform;

    void Start()
    {
        IsVisible = true;
        Refresh();
    }

    public void Activate()
    {
        IsVisible = !IsVisible;
        Refresh();
    }

    void Refresh()
    {
        RectTransform.anchoredPosition = IsVisible ? Vector2.zero : new Vector2(RectTransform.sizeDelta.x, 0.0f);
    }
}
