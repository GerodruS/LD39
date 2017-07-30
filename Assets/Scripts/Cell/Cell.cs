using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[ExecuteInEditMode]
public class Cell : MonoBehaviour
{
    public FieldData.Cell Data;

    public GameObject TopRightBorder;
    public GameObject TopLeftBorder;
    public GameObject RightBorder;
    public GameObject LeftBorder;
    public GameObject BottomRightBorder;
    public GameObject BottomLeftBorder;

    public Dictionary<FieldData.Direction, Cell> AdjacentCells = new Dictionary<FieldData.Direction, Cell>();

    public GameObject BaseIcon;
    public GameObject Curtain;
    public Transform ModificationsRoot;

    public Events.Empty OnCellWasActivated;
    public Events.IntInt OnClicked;

#if UNITY_EDITOR
    void Update()
    {
        RefreshRestrictions();
    }

    void OnMouseEnter()
    {
//        DrawPath();
    }

    void OnMouseExit()
    {
        DrawPath();
    }

    void DrawPath()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            var open = Input.GetKey(KeyCode.A);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 cellPosition = transform.position;
            var vector = mousePosition - cellPosition;
            var angle = GetAngle(Vector2.right, vector);
            if (0.0f <= angle && angle < 180.0f / 6.0f || angle <= 0.0f && -180.0f / 6.0f < angle)
                Data.Right = open;
            else if (0.0f <= angle && angle < 3.0f * 180.0f / 6.0f)
                Data.TopRight = open;
            else if (0.0f <= angle && angle < 5.0f * 180.0f / 6.0f)
                Data.TopLeft = open;
            else if (angle <= 0.0f && -3.0f * 180.0f / 6.0f < angle)
                Data.BottomRight = open;
            else if (angle <= 0.0f && -5.0f * 180.0f / 6.0f < angle)
                Data.BottomLeft = open;
            else 
                Data.Left = open;
        }
        FindObjectOfType<Field>().RefreshDiscover();
    }
#endif

    void OnMouseDown()
    {
//        print(this);
        if (!EventSystem.current.IsPointerOverGameObject())
            OnClicked.Invoke(Data.Point.X, Data.Point.Y);
    }
    
    private static float GetAngle(Vector2 v1, Vector2 v2)
    {
        var sign = Mathf.Sign(v1.x * v2.y - v1.y * v2.x);
        return Vector2.Angle(v1, v2) * sign;
    }

    public void OnActivate()
    {
        Discover();
        foreach (var elem in AdjacentCells)
        {
            if (elem.Value != null)
                elem.Value.Discover();
        }
        OnCellWasActivated.Invoke();
    }

    void Discover()
    {
        if (Curtain.activeSelf)
            FindObjectOfType<ProgressLabel>().OnCellWasDiscovered();
        Curtain.SetActive(false);
    }

    public void SetData(FieldData.Cell data)
    {
        Data = data;
        SetType(data.Type);
        RefreshRestrictions();
        SpawnModifications();
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

    void RefreshRestrictions()
    {
        TopRightBorder.SetActive(!Data.TopRight);
        TopLeftBorder.SetActive(!Data.TopLeft);
        RightBorder.SetActive(!Data.Right);
        LeftBorder.SetActive(!Data.Left);
        BottomRightBorder.SetActive(!Data.BottomRight);
        BottomLeftBorder.SetActive(!Data.BottomLeft);
    }

    void SpawnModifications()
    {
        foreach (var modification in Data.Modificators)
        {
            var modificator = Instantiate(modification.Prefab, ModificationsRoot);
            OnCellWasActivated.AddListener(modificator.OnActivate);
        }
    }
}