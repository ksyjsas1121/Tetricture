using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Android;
using UnityEditor;
public enum Tetromino
{
    I, J, L, O, S, T, Z
  //0, 1, 2, 3, 4, 5, 6
}

[System.Serializable]
public struct TetrominoData
{
    public Tile tile;
    public Tetromino tetromino;

    public Vector2Int[] cells { get; private set; }
    public Vector2Int[,] wallKicks { get; private set; }

    public void Initialize()
    {
        cells = Data.Cells[tetromino];
        wallKicks = Data.WallKicks[tetromino];
    }

}
