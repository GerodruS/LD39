using UnityEngine;


public class Field : MonoBehaviour
{
    public GameObject CellPrefab;
    public int Height;
    public int Width;
    public float Radius;

    void Start()
    {
        var height = Mathf.Sqrt(Mathf.Pow(2.0f * Radius, 2.0f) - Mathf.Pow(1.0f * Radius, 2.0f));
        var HalfWidth = Width / 2;
        var HalfHeight = Height / 2;
        for (var x = -HalfWidth; x < HalfWidth; ++x)
        {
            var offset = 0 == x % 2 ? 0.0f : 0.5f;
            for (var y = -HalfHeight; y < HalfHeight; ++y)
            {
                var cell = Instantiate(CellPrefab, transform);
                cell.transform.localPosition = new Vector3(x * 0.5f * Radius, (y + offset) * height, 0.0f);
                cell.name = string.Format("{0: 00;-00; 00}   {1: 00;-00; 00}", x, y);
            }
        }
    }
}