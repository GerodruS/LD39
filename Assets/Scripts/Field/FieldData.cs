using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Field Data", menuName = "Field Data", order = 1)]
public class FieldData : ScriptableObject
{
    public enum Direction
    {
        TopRight,
        Right,
        BottomRight,
        BottomLeft,
        Left,
        TopLeft
    }

    public Dictionary<Direction, Direction> OppositeDirection = new Dictionary<Direction, Direction>
    {
        {Direction.TopRight, Direction.BottomLeft},
        {Direction.Right, Direction.Left},
        {Direction.BottomRight, Direction.TopLeft},
        {Direction.BottomLeft, Direction.TopRight},
        {Direction.Left, Direction.Right},
        {Direction.TopLeft, Direction.BottomRight},
    };

    public Dictionary<Direction, int> DirectionCost = new Dictionary<Direction, int>
    {
        {Direction.TopRight, 3},
        {Direction.Right, 2},
        {Direction.BottomRight, 1},
        {Direction.BottomLeft, 1},
        {Direction.Left, 2},
        {Direction.TopLeft, 3},
    };

    public enum CellType
    {
        Common,
        Base,
    }

    [Serializable]
    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;
    }

    [Serializable]
    public class Cell
    {
        public Point Point;

        public bool TopRight;
        public bool TopLeft;
        public bool Right;
        public bool Left;
        public bool BottomRight;
        public bool BottomLeft;

        public CellType Type;

        public new string ToString()
        {
            return string.Format("{0: 00;-00; 00}   {1: 00;-00; 00}", Point.X, Point.Y);
        }
    }

    public Cell[] Cells;
}