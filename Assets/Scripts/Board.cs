using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;
using TMPro;
using UnityEngine.Android;


public class Board : MonoBehaviour
{
    public int stage = 0;


    public TextMeshProUGUI resultTime;


    public TextMeshProUGUI First;
    public TextMeshProUGUI Second;
    public TextMeshProUGUI Th;
    public TextMeshProUGUI Fo;

    string[] StageClearChack;


    public GameObject ResultPanel;
    public TextMeshProUGUI resultText;

    public GameObject pausePanel;

    public float scoreTime;

    public Slider timeS;
    public float time = 60f;

    int stop = 0;

    public bool playing;

    public Image showSolve;
   
    public Sprite[] solveImage;

    public GameObject solvePanel;

    public int stageChack = 0;

    public GameObject gameScene;
    public GameObject mainScene;

    public int pieceCount1 = 0; // m
    public int plus = 0;

    public int[] pathon1;
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }

    public TetrominoData[] tetrominoes;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);

    public int chacky;
    
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resome()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        
    }
    public void Exit()
    {
        Time.timeScale = 1;
        GameOver();
        gameScene.SetActive(false);
        mainScene.SetActive(true);
        DOTween.KillAll();
        DOTween.Init();
    }

    public Vector3Int cellPosition = new Vector3Int(-1, 8, 0);

    public RectInt Bounds {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    private void OnEnable()
    {
        stop = 0;
        solvePanel.SetActive(false);
    }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < tetrominoes.Length; i++) {
            tetrominoes[i].Initialize();
        }
    }

    public void Stage1()
    {

        stage = 1;
        chacky = 1;
        playing = true;
        stageChack = 0;
        pathon1 = new int[] { 3, 2, 5, 2, 5, 5, 5, 3, 5, 3, 1, 5,
        3, 5, 3, 5, 6, 5, 1, 5, 2, 1, 0 };
        StageClearChack = new string[] {"null", "null", "Yellow", "Yellow", "null",
        "Purple", "Purple", "Purple", "null", "null",
       "null","null","null","null","null","Blue", "Blue", "Blue","null","null" };
        scoreTime = 60f;


        StartCoroutine(Minus());



        time = 60f;

        DOTween.To(() => time, x => time = x, 0, 60);

        showSolve.sprite = solveImage[0];
        plus = 0;

        SpawnPiece();
    }

    public void Stage2()
    {
 

        stage = 2;
        chacky = -3;
        playing = true;
        stageChack = 0;
        pathon1 = new int[] {0,1,2,0,5,5,3,5,5,4,6,3,0,0 };
        StageClearChack = new string[] {"null", "null", "null", "Cyan", "Cyan",
        "Cyan", "Cyan", "null", "null", "null",
       "null","null","null","Cyan","Cyan","Cyan", "Cyan", "null","null","null" };
        // I, J, L, O, S, T, Z
        // 0, 1, 2, 3, 4, 5, 6
        scoreTime = 60f;


        StartCoroutine(Minus());



        time = 60f;

        DOTween.To(() => time, x => time = x, 0, 60);

        showSolve.sprite = solveImage[1];
        plus = 0;

        SpawnPiece();
    }

    public void Stage3()
    {
        stage = 3;
        chacky = -3;
        playing = true;
        stageChack = 0;
        pathon1 = new int[] { 1,2,3,3,0,0,1,0,2,2,1,0,3,6,5,5,0 };
        //pathon1 = new int[] { 1,0,3,6,5,5,5 };
        StageClearChack = new string[] {"null", "Cyan", "Blue", "Blue", "Blue", "Orange", "Orange", "Orange", "Cyan", "null",
       "null","Cyan","Cyan","Cyan","Cyan","Red", "Purple", "Purple","Purple","null" };
        // I, J, L, O, S, T, Z
        // 0, 1, 2, 3, 4, 5, 6
        scoreTime = 60f;


        StartCoroutine(Minus());



        time = 60f;

        DOTween.To(() => time, x => time = x, 0, 60);

        showSolve.sprite = solveImage[2];
        plus = 0;

        SpawnPiece();
    }
    public void Stage4()
    {
        stage = 4;
        chacky = -3;
        playing = true;
        stageChack = 0;
        pathon1 = new int[] { 5, 5, 6, 3, 4, 5, 5, 6, 4, 0, 0 };

        StageClearChack = new string[] {"null", "Purple", "Purple", "Purple", "Red", "Red", "Purple", "Purple", "Purple", "null",
       "null","Red","Red","Cyan","Cyan","Cyan", "Cyan", "Green","Green","null" };
        // I, J, L, O, S, T, Z
        // 0, 1, 2, 3, 4, 5, 6
        scoreTime = 60f;


        StartCoroutine(Minus());



        time = 60f;

        DOTween.To(() => time, x => time = x, 0, 60);

        showSolve.sprite = solveImage[3];
        plus = 0;

        SpawnPiece();
    }


    private void Start()
    {
         
        // I, J, L, O, S, T, Z
        // 0, 1, 2, 3, 4, 5, 6
        //O L T L T T T O T O J T O T O// T Z T J T L


        
        
        // m
    }

    public void SpawnPiece()
    {




        //int random = Random.Range(0, tetrominoes.Length);
        if(plus <= pathon1.Length)
        {
            TetrominoData data = tetrominoes[pathon1[plus]];
            plus++;




            activePiece.Initialize(this, spawnPosition, data);

            if (IsValidPosition(activePiece, spawnPosition))
            {
                Set(activePiece);
            }
            else
            {
                Stage1Clear();
            }
        }
        
    }

    public void GameOver()
    {
        tilemap.ClearAllTiles();

        // Do anything else you want on game over here..
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            // An out of bounds tile is invalid
            if (!bounds.Contains((Vector2Int)tilePosition)) {
                return false;
            }

            // A tile already occupies the position, thus invalid
            if (tilemap.HasTile(tilePosition)) {
                return false;
            }
        }

        return true;
    }

    public void ClearLines()
    {
        //RectInt bounds = Bounds;
        //int row = bounds.yMin;

        //// Clear from bottom to top
        //while (row < bounds.yMax)
        //{
        //    // Only advance to the next row if the current is not cleared
        //    // because the tiles above will fall down when a row is cleared
        //    if (IsLineFull(row))
        //    {
        //        LineClear(row);
        //    }
        //    else
        //    {
        //        row++;
        //    }
        //}
    }

    public bool IsLineFull(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            // The line is not full if a tile is missing
            if (!tilemap.HasTile(position)) {
                return false;
            }
        }

        return true;
    }

    public void LineClear(int row)
    {
        //RectInt bounds = Bounds;

        //// Clear all tiles in the row
        //for (int col = bounds.xMin; col < bounds.xMax; col++)
        //{
        //    Vector3Int position = new Vector3Int(col, row, 0);
        //    tilemap.SetTile(position, null);
        //}

        //// Shift every row above down one
        //while (row < bounds.yMax)
        //{
        //    for (int col = bounds.xMin; col < bounds.xMax; col++)
        //    {
        //        Vector3Int position = new Vector3Int(col, row + 1, 0);
        //        TileBase above = tilemap.GetTile(position);

        //        position = new Vector3Int(col, row, 0);
        //        tilemap.SetTile(position, above);
        //    }

        //    row++;
        //}
    }

    IEnumerator Minus()
    {
        scoreTime -= 1;
        //print(scoreTime);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Minus());

    }

    public void ScoreUpdate()
    {
        First.text = "" + SaveManager.instance.saved.firstStageTime;
        Second.text = "" + SaveManager.instance.saved.secondStageTime;
        Th.text = "" + SaveManager.instance.saved.tStageTime;
        Fo.text = "" + SaveManager.instance.saved.fStageTime;
    }

    public void Update()
    {
        if(time <= 0)
        {
            Stage1Clear();
        }


        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Pause();
        }
       

        timeS.value = time;
        
        if(plus == pathon1.Length && stop == 0)
        {
            stop = 1;
            Stage1Clear();
            
        }


        //Sprite sprite = tilemap.GetSprite(cellPosition);

        //if (sprite != null)
        //{
        //    Texture2D texture = sprite.texture;
        //    Vector2 centerUV = new Vector2(0.5f, 0.5f);
        //    Color centerColor = texture.GetPixelBilinear(centerUV.x, centerUV.y);

        //    // 색상 출력
        //    string hexColor = ColorUtility.ToHtmlStringRGB(centerColor);
        //    Debug.Log("Center color in hex: #" + hexColor);
        //}
        //else
        //{
        //    Debug.LogWarning("No tile at position (" + cellPosition.x + ", " + cellPosition.y + ")");
        //}
    }
   public void Stage1Clear()
    {
        Debug.Log("스테이지 클리어 호출");
        playing = false;

        bool clear1 = true;

        cellPosition.y = -10;
        

       for(int i = 0; i <= 19; i++)
        {
            print(StageClearChack[i]);
        }

        int startChack = 0;
        for(int re = 0; re<2; re++)
        {
            if(re == 1)
            {
                cellPosition.y = chacky;



            }
            for(int g = -5; g<= 4; g++  )
            {
                cellPosition.x = g;
                Sprite sprite = tilemap.GetSprite(cellPosition);
                
                if(sprite != null)
                {
                    if(sprite.name != StageClearChack[startChack])
                    {
                        clear1 = false;
                        Debug.Log(cellPosition.x + " " + cellPosition.y + " " + startChack);
                        Debug.Log(sprite.name + " != " + StageClearChack[startChack]);
                        
                    }
                    
                    else
                    {
                        Debug.Log("통과" + startChack);
                    }
                    startChack++;

                }
                else
                {
                    if (StageClearChack[startChack]!= "null")
                    {
                        clear1 = false;
                        Debug.Log(cellPosition.x + " " + cellPosition.y);

                    }
                    else
                    {
                        Debug.Log("통과" + startChack);
                    }
                    startChack++;
                }
            }
        }

        Debug.Log(clear1);
        if (clear1 == true)
        {
            Time.timeScale = 0;
            Result(true);
        }
        else
        {
            scoreTime = 0;
            Result(false);
            Time.timeScale = 0;
        }
      
        
        
    }

    public void Result(bool result)
    {
        ResultPanel.SetActive(true);

        if(result == true)
        {

            resultTime.text = "남은 시간 : " + (int)scoreTime;


           



            resultText.text = "Clear";

            switch (stage)
            {
                case 1: SaveManager.instance.saved.firstStageTime = Mathf.Max((int)scoreTime,
                    SaveManager.instance.saved.firstStageTime);
                    break;
                case 2:
                    SaveManager.instance.saved.secondStageTime = Mathf.Max((int)scoreTime, SaveManager.instance.saved.secondStageTime);
                    break;
                case 3:
                    SaveManager.instance.saved.tStageTime = Mathf.Max((int)scoreTime, SaveManager.instance.saved.tStageTime);
                    break;
                case 4:
                    SaveManager.instance.saved.fStageTime = Mathf.Max((int)scoreTime, SaveManager.instance.saved.fStageTime);
                    break;
            }
            SaveManager.instance.Save();
        }
        else
        {
            resultTime.text = "남은 시간 : " + (int)scoreTime;
            resultText.text = "Fail";
        }
    }


}

