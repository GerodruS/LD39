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
#endif

    void OnMouseDown()
    {
//        print(this);
        if (!EventSystem.current.IsPointerOverGameObject())
            OnClicked.Invoke(Data.Point.X, Data.Point.Y);
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