using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.Tilemaps;
using Object = System.Object;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public ExitCellObject ExitCellObject;
    [SerializeField] private Tilemap m_TileMap;
    [SerializeField] private Grid m_Grid;
    
    public int Width;
    public int Height;
    public Tile[] GroundTiles;
    public Tile[] WallTiles;
    private List<Vector2Int> m_EmptyCellList;
    
    
    public class CellData
    {
        public bool Passable;
        public CellObject ContainedObject;
    }

    private CellData[,] m_BoardData;

    private void Awake()
    {
        m_TileMap = transform.GetComponentInChildren<Tilemap>();
        m_Grid = transform.GetComponent<Grid>();
    }

    public void Init()
    {
        
        
        m_EmptyCellList = new List<Vector2Int>();
        m_BoardData = new CellData[Width, Height];

       
        for (int y = 0; y < Width; ++y)
        {
            
            for (int x = 0; x < Height; ++x)
            {
                
                Tile tile;
                m_BoardData[x, y] = new CellData();
                if (x == 0 || x == Height - 1 || y == 0 || y == Width - 1)
                {
                    tile = WallTiles[Random.Range(0, WallTiles.Length)];
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    tile = GroundTiles[Random.Range(0, GroundTiles.Length)];
                    m_BoardData[x, y].Passable = true;
                    m_EmptyCellList.Add(new Vector2Int(x,y));
                }
                
                m_TileMap.SetTile(new Vector3Int(x, y,0),tile);
            }
        }

        m_EmptyCellList.Remove(new Vector2Int(1, 1));
        GenerateExitCell();
        GenerateFood();
        GenerateWall();
    }

   

    

    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    public CellData GetCellData(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.x >= Width || cellIndex.y < 0 || cellIndex.y >= Height) return null;
        return m_BoardData[cellIndex.x, cellIndex.y];
    }

    public void SetCellContainObject(Vector2Int cellIndex, CellObject data)
    {
        if (cellIndex.x < 0 || cellIndex.x >= Width || cellIndex.y < 0 || cellIndex.y >= Height) return;
        m_BoardData[cellIndex.x, cellIndex.y].ContainedObject = data;
    }
    public Tile GetCellTile(Vector2Int cellIndex)
    {
        return m_TileMap.GetTile<Tile>(new Vector3Int(cellIndex.x, cellIndex.y, 0));
    }

    void AddObject(CellObject obj, Vector2Int coord)
    {
        CellData data = m_BoardData[coord.x, coord.y];
        data.ContainedObject = obj;
        obj.Init(coord);
    }

    void GenerateFood()
    {
        int wallCount = Random.Range(6,10);
        for (int i = 0; i < wallCount; ++i)
        {
            int randomIndex = Random.Range(0, m_EmptyCellList.Count);
            Vector2Int coord = m_EmptyCellList[randomIndex];
            
            m_EmptyCellList.RemoveAt(randomIndex);
            CellObject newWall = ObjectSpawner.Instance.GenerateFood(CellToWorld(coord));
            AddObject(newWall,coord);
        }
    }

    void GenerateWall()
    {
        int foodCount = 5;
        for (int i = 0; i < foodCount; ++i)
        {
            int randomIndex = Random.Range(0, m_EmptyCellList.Count);
            Vector2Int coord = m_EmptyCellList[randomIndex];
            
            m_EmptyCellList.RemoveAt(randomIndex);

            CellObject newFood = ObjectSpawner.Instance.GenerateWall(CellToWorld(coord));
            AddObject(newFood,coord);
        }
    }

    void GenerateExitCell()
    {
        Vector2Int coord = new Vector2Int(Width - 2, Height - 2);
        m_EmptyCellList.Remove(coord);
        CellObject exitCell = PoolingManager.Spawn(ExitCellObject.gameObject, CellToWorld(coord)).GetComponent<CellObject>();
        AddObject(exitCell,coord);
    }
    public void SetCellTile(Vector2Int cellIndex, Tile tile)
    {
        m_TileMap.SetTile(new Vector3Int(cellIndex.x,cellIndex.y,0),tile);
    }

    public void Clean()
    {
        if (m_BoardData == null) return;
        for (int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                var cellData = m_BoardData[x, y];
                if (cellData.ContainedObject != null) PoolingManager.Despawn(cellData.ContainedObject.gameObject);
                SetCellTile(new Vector2Int(x,y),null);
            }
        }
    }
}
