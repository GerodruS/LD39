using UnityEngine;


[ExecuteInEditMode]
public class Cell : MonoBehaviour
{
    public FieldData.Point Point;
    
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

    public Events.IntInt OnClicked;

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

    void OnMouseDown()
    {
        print(this);
        OnClicked.Invoke(Point.X, Point.Y);
    }

    public void SetData(FieldData.Cell data)
    {
        Point = data.Point;
        SetType(data.Type);
    }

    void SetType(FieldData.CellType type)
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