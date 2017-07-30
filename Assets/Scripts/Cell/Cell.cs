using UnityEngine;


[ExecuteInEditMode]
public class Cell : MonoBehaviour
{
    public bool TopRight;
    public bool TopLeft;
    public bool Right;
    public bool Left;
    public bool BottomRight;
    public bool BottomLeft;

    public GameObject TopRightBorder;
    public GameObject TopLeftBorder;
    public GameObject RightBorder;
    public GameObject LeftBorder;
    public GameObject BottomRightBorder;
    public GameObject BottomLeftBorder;

    public GameObject BaseIcon;

#if UNITY_EDITOR
    void Update()
    {
        TopRightBorder.SetActive(TopRight);
        TopLeftBorder.SetActive(TopLeft);
        RightBorder.SetActive(Right);
        LeftBorder.SetActive(Left);
        BottomRightBorder.SetActive(BottomRight);
        BottomLeftBorder.SetActive(BottomLeft);
    }
#endif

    public void SetType(FieldData.CellType type)
    {
        BaseIcon.SetActive(false);
        switch (type)
        {
            case FieldData.CellType.Common:
                break;

            case FieldData.CellType.Base:
                BaseIcon.SetActive(true);
                break;

            default:
                Debug.LogError("SetType");
                break;
        }
    }
}