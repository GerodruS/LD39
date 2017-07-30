using UnityEngine;


public class Field : MonoBehaviour
{
    public GameObject CellPrefab;
    public Transform CellParent;
    public Drone DronePrefab;
    public FieldData FieldData;
    public int Height;
    public int Width;
    public float Radius;

    Drone CurrentDrone;
    FieldData.Point DronePoint;
    FieldData.Direction PreviousDirection;
    int ComboBonus;
    Cell[] Cells;

    float VerticalDistance
    {
        get { return Mathf.Sqrt(Mathf.Pow(2.0f * Radius, 2.0f) - Mathf.Pow(1.0f * Radius, 2.0f)); }
    }

    void Start()
    {
//        Generate();
        var verticalDistance = VerticalDistance;
        Cells = new Cell[FieldData.Cells.Length];
        for (int i = 0, n = FieldData.Cells.Length; i < n; ++i)
        {
            var cellData = FieldData.Cells[i];
            var x = cellData.Point.X;
            var y = cellData.Point.Y;
            var offset = 0 == x % 2 ? 0.0f : 0.5f;
            var cellObject = Instantiate(CellPrefab, CellParent);
            cellObject.transform.localPosition = new Vector3(x * 0.5f * Radius, (y + offset) * verticalDistance, 0.0f);
            cellObject.name = string.Format("{0: 00;-00; 00}   {1: 00;-00; 00}", x, y);
            var cell = cellObject.GetComponent<Cell>();
            cell.SetData(cellData);
            cell.OnClicked.AddListener(OnCellClicked);
            Cells[i] = cell;
        }
        for (int i = 0, n = FieldData.Cells.Length; i < n; ++i)
        {
            var c = Cells[i];
            var p = c.Data.Point;
            var cells = c.AdjacentCells;
            cells.Add(FieldData.Direction.Right, c.Data.Right ? GetCell(p.X + 2, p.Y) : null);
            cells.Add(FieldData.Direction.Left, c.Data.Left ? GetCell(p.X - 2, p.Y) : null);
            var offset = 0 == p.X % 2 ? -1 : 0;
            cells.Add(FieldData.Direction.TopRight, c.Data.TopRight ? GetCell(p.X + 1, p.Y + 1 + offset) : null);
            cells.Add(FieldData.Direction.TopLeft, c.Data.TopLeft ? GetCell(p.X - 1, p.Y + 1 + offset) : null);
            cells.Add(FieldData.Direction.BottomRight, c.Data.BottomRight ? GetCell(p.X + 1, p.Y - 0 + offset) : null);
            cells.Add(FieldData.Direction.BottomLeft, c.Data.BottomLeft ? GetCell(p.X - 1, p.Y - 0 + offset) : null);
        }
    }

    void Generate()
    {
        var halfWidth = Width / 2;
        var halfHeight = Height / 2;
        FieldData.Cells = new FieldData.Cell[Width * Height];
        var i = 0;
        for (var x = -halfWidth; x < halfWidth; ++x)
        {
            for (var y = -halfHeight; y < halfHeight; ++y)
            {
                var newCell = new FieldData.Cell
                {
                    Point =
                    {
                        X = x,
                        Y = y
                    }
                };
                FieldData.Cells[i] = newCell;
                ++i;
            }
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
        var droneCell = GetCell(DronePoint.X, DronePoint.Y);
        if (droneCell == null)
            return;
        var targetCell = GetCell(x, y);
        if (targetCell == null)
            return;
        foreach (var elem in droneCell.AdjacentCells)
        {
            if (elem.Value != targetCell) continue;
            var cost = FieldData.DirectionCost[elem.Key];
            if (PreviousDirection == elem.Key)
                if (ComboBonus + 1 < cost)
                    ++ComboBonus;
                else
                    ComboBonus = 0;
            else
                ComboBonus = 0;
            cost -= ComboBonus;
            if (CurrentDrone.Power < cost)
            {
                Debug.LogFormat("Not enough power: {0}/{1}", CurrentDrone.Power, cost);
                return;
            }
            CurrentDrone.Power -= cost;
            PreviousDirection = elem.Key;
            MoveDrone(x, y);
//            print(elem.Key);
            return;
        }
//        foreach (var elem in droneCell.AdjacentCells)
//        {
//            if (elem.Value == null) continue;
//            if (elem.Value != targetCell.AdjacentCells[FieldData.OppositeDirection[elem.Key]]) continue;
//            var cost = FieldData.DirectionCost[elem.Key];
//            if (CurrentDrone.Power < cost)
//            {
//                Debug.LogFormat("Not enough power: {0}/{1}", CurrentDrone.Power, cost);
//                return;
//            }
//            CurrentDrone.Power -= cost;
//            print(elem.Key);
//            return;
//        }
        print("--");
    }

    void MoveDrone(int x, int y)
    {
        CurrentDrone.transform.localPosition = GetPosition(x, y);
        MoveCameraTo(x, y);
        DronePoint = new FieldData.Point(x, y);
    }

    Vector3 GetPosition(int x, int y)
    {
        var cell = GetCell(x, y);
        return cell != null ? cell.transform.localPosition : Vector3.zero;
    }

    Cell GetCell(int x, int y)
    {
        for (int i = 0, n = FieldData.Cells.Length; i < n; ++i)
        {
            var cellData = FieldData.Cells[i];
            if (cellData.Point.X == x && cellData.Point.Y == y)
            {
                return Cells[i];
            }
        }
        return null;
    }

    void MoveCameraTo(int x, int y)
    {
        var cell = GetCell(x, y);
        var cellPosition = cell != null ? cell.transform.position : Vector3.zero;
        var mainCamera = Camera.main;
        var position = mainCamera.transform.position;
        position.x = cellPosition.x;
        position.y = cellPosition.y;
        mainCamera.transform.position = position;
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
            PreviousDirection = FieldData.Direction.None;
            DronePoint = baseCell.Data.Point;
        }
    }
}