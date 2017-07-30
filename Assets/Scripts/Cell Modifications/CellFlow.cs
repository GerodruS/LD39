using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFlow : CellModification
{
    public FieldData.Direction Direction = FieldData.Direction.Left;
    
    public override void OnActivate()
    {
        var field = FindObjectOfType<Field>();
        field.TryMoveDrone(Direction);
    }
}
