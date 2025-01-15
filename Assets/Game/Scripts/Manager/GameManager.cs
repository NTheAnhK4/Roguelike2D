using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int m_FoodAmount = 100;
    public PlayerController PlayerController;
    public BoardManager BoardManager;
    public TurnManager TurnManager { get; private set; }

   

    private void Start()
    {
        TurnManager = new TurnManager();
        TurnManager.OnTick += OnTurnHappen;
        BoardManager.Init();
        PlayerController.Spawn(BoardManager,new Vector2Int(1,1));
    }

    void OnTurnHappen()
    {
        m_FoodAmount -= 1;
        Debug.Log("Current amount of food " + m_FoodAmount);
    }
}
