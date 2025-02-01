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
    private int m_CurrentLevel = 1;

    public int CurrentLevel => m_CurrentLevel;
    protected override void Awake()
    {
        base.Awake();
        ObserverManager.Attach(EventId.NewGame,param=>StartNewGame());
    }

    private void Start()
    {
        TurnManager = new TurnManager();
        TurnManager.OnTick += OnTurnHappen;
       
        StartNewGame();
    }
    
    void OnTurnHappen()
    {
        EnergyManager.Instance.TakeEnegy(1);
    }

    public void ChangeFood(int amount)
    {
        EnergyManager.Instance.AddEnergy(amount);
    }

    public void NewLevel()
    {
        BoardManager.Clean();
        BoardManager.Init();
        
        PlayerController.Spawn(BoardManager,new Vector2Int(1,1));
        m_CurrentLevel++;
    }

    public void StartNewGame()
    {
        m_CurrentLevel = 0;
        EnergyManager.Instance.Init(20);
        PlayerController.Init();
        NewLevel();
    }
}
