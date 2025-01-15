using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
   
    public PlayerController PlayerController;
    public BoardManager BoardManager;
    public TurnManager TurnManager { get; private set; }

   

    private void Start()
    {
        TurnManager = new TurnManager();
        BoardManager.Init();
        PlayerController.Spawn(BoardManager,new Vector2Int(1,1));
    }
}
