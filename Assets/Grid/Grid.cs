using UnityEngine;
using CodeMonkey.Utils;

public class Grid
{   
    #region Variables
    private int width, height;
    private float cellSize;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    private bool showDebug;  
    private Vector3 origin;  
    #endregion
    #region Build Grid
    public Grid (int _width, int _height, float _cellSize, Vector3 originPoint, bool _showGrid)
    {
        width = _width;
        height = _height;
        cellSize = _cellSize;
        showDebug = _showGrid;
        origin = originPoint;

        gridArray = new int [width, height];

        if(showDebug) ShowDebug();
    }
    #endregion
    #region Get and Set Functions
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + origin;
    }
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - origin).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - origin).y / cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x,y] = value;
            debugTextArray[x,y].text = value.ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x,y;
        GetXY(worldPosition,out x, out y);
        SetValue(x,y,value);
    }

    public int GetValue(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
            return gridArray[x,y];
        else
            return -999;
    }

    public int GetValue(Vector3 worldPosition, int value)
    {
        int x,y;
        GetXY(worldPosition,out x, out y);
        return GetValue(x,y);
    }

    #endregion
    #region Debug
    private void ShowDebug()
    {
        debugTextArray = new TextMesh[width, height];
        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for(int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x,y].ToString(), null, 
                    GetWorldPosition(x, y) + new Vector3(cellSize,cellSize) * .5f,
                    20, Color.white, TextAnchor.MiddleCenter);           
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x,y+1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1,y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0,height), GetWorldPosition(width,height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width,0), GetWorldPosition(width,height), Color.white, 100f);
    }
    #endregion
}
