using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Field Data", menuName = "Field Data", order = 1)]
public class FieldData : ScriptableObject
{
    public enum CellType
    {
        Common,
        Base,
    }

    [Serializable]
    public class Cell
    {
        public int X;
        public int Y;

        public bool TopRight;
        public bool TopLeft;
        public bool Right;
        public bool Left;
        public bool BottomRight;
        public bool BottomLeft;

        public CellType Type;

        public new string ToString()
        {
            return string.Format("{0: 00;-00; 00}   {1: 00;-00; 00}", X, Y);
        }
    }

    public Cell[] Cells;
}