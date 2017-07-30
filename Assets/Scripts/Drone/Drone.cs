using UnityEngine;


public class Drone : MonoBehaviour
{
    public int PowerValue = 100;
    public int MaxPowerValue = 100;

    public Events.IntInt OnPowerLevelChanged;
    public Field Field;
    public FieldData.Point CurrentPoint;
    public FieldData.Direction PreviousDirection;

    public int Power
    {
        get { return PowerValue; }
        set
        {
            PowerValue = value;
            OnPowerLevelChanged.Invoke(PowerValue, MaxPowerValue);
        }
    }

    void Awake()
    {
        Field = FindObjectOfType<Field>();
    }

    void Start()
    {
        var powerIndicator = FindObjectOfType<PowerIndicator>();
        if (powerIndicator != null)
        {
            OnPowerLevelChanged.AddListener(powerIndicator.OnPowerLevelChanged);
            powerIndicator.OnPowerLevelChanged(PowerValue, MaxPowerValue);
        }
    }

    void OnDestroy()
    {
        var powerIndicator = FindObjectOfType<PowerIndicator>();
        if (powerIndicator != null)
            OnPowerLevelChanged.RemoveListener(powerIndicator.OnPowerLevelChanged);
    }

    public void MoveTo(int x, int y)
    {
        transform.localPosition = Field.GetPosition(x, y);
        CurrentPoint = new FieldData.Point(x, y);
    }

    public void OnAbandon()
    {
        Field.OnPlayerWasMoved.AddListener(Descent);
    }

    void Descent()
    {
        var currentCell = Field.GetCell(CurrentPoint);
        if (currentCell != null)
        {
            if (0 == Random.Range(0, 2))
            {
                var nextCell = currentCell.AdjacentCells[FieldData.Direction.BottomLeft];
                if (null == nextCell)
                {
                    nextCell = currentCell.AdjacentCells[FieldData.Direction.BottomRight];
                }
                if (nextCell != null)
                {
                    MoveTo(nextCell.Data.Point.X, nextCell.Data.Point.Y);
                }
            }
            else
            {
                var nextCell = currentCell.AdjacentCells[FieldData.Direction.BottomRight];
                if (null == nextCell)
                {
                    nextCell = currentCell.AdjacentCells[FieldData.Direction.BottomLeft];
                }
                if (nextCell != null)
                {
                    MoveTo(nextCell.Data.Point.X, nextCell.Data.Point.Y);
                }
            }
        }
    }
}