﻿using UnityEngine;


public class Field : MonoBehaviour
{
    public GameObject CellPrefab;
    public Drone DronePrefab;
    public FieldData FieldData;
    public int Height;
    public int Width;
    public float Radius;

    Drone CurrentDrone;
    Cell[] Cells;

    float VerticalDistance
    {
        get { return Mathf.Sqrt(Mathf.Pow(2.0f * Radius, 2.0f) - Mathf.Pow(1.0f * Radius, 2.0f)); }
    }

    void Start()
    {
        var verticalDistance = VerticalDistance;
        Cells = new Cell[FieldData.Cells.Length];
        for (int i = 0, n = FieldData.Cells.Length; i < n; ++i)
        {
            var cellData = FieldData.Cells[i];
            var x = cellData.Point.X;
            var y = cellData.Point.Y;
            var offset = 0 == x % 2 ? 0.0f : 0.5f;
            var cellObject = Instantiate(CellPrefab, transform);
            cellObject.transform.localPosition = new Vector3(x * 0.5f * Radius, (y + offset) * verticalDistance, 0.0f);
            cellObject.name = string.Format("{0: 00;-00; 00}   {1: 00;-00; 00}", x, y);
            var cell = cellObject.GetComponent<Cell>();
            cell.SetData(cellData);
            cell.OnClicked.AddListener(OnCellClicked);
            Cells[i] = cell;
        }
    }

    void OnCellClicked(int x, int y)
    {
        if (CurrentDrone)
            TryMoveDrone(x, y);
        else
            TrySpawnDrone();
    }

    void TryMoveDrone(int x, int y)
    {
    }

    void TrySpawnDrone()
    {
        Cell baseCell = null;
        for (int i = 0, n = FieldData.Cells.Length; i < n; ++i)
        {
            var cellData = FieldData.Cells[i];
            if (cellData.Type == FieldData.CellType.Base)
            {
                baseCell = Cells[i];
                break;
            }
        }
        if (baseCell)
        {
            var newDrone = Instantiate(DronePrefab, transform);
            newDrone.transform.localPosition = baseCell.transform.localPosition;
            CurrentDrone = newDrone;
        }
    }
}