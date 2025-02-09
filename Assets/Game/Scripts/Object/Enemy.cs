using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CellObject
{
    public int Health = 3;
    private int m_CurrentHealth;

    private void Awake()
    {
        GameManager.Instance.TurnManager.OnTick += TurnHappened;
    }

    private void OnDestroy()
    {
        GameManager.Instance.TurnManager.OnTick -= TurnHappened;
    }

    public override void Init(Vector2Int coord)
    {
        base.Init(coord);
        m_CurrentHealth = Health;
    }

    public override bool PlayerWantsToEnter()
    {
        m_CurrentHealth -= 1;
        if (m_CurrentHealth <= 0)
        {
            GameManager.Instance.BoardManager.GetCellData(m_Cell).ContainedObject = null;
            PoolingManager.Despawn(gameObject);
            return true;
        }
        return false;
    }

    bool MoveTo(Vector2Int coord)
    {
        var board = GameManager.Instance.BoardManager;
        var targetCell = board.GetCellData(coord);
        if (targetCell == null
            || !targetCell.Passable
            || targetCell.ContainedObject != null) return false;
        //remove enemy from current cell
        var currentCell = board.GetCellData(m_Cell);
        currentCell.ContainedObject = null;
        // add it to next cell
        targetCell.ContainedObject = this;
        m_Cell = coord;
        transform.position = board.CellToWorld(coord);
        return true;
    }

    void TurnHappened()
    {
        var playerCell = GameManager.Instance.PlayerController.Cell;

        int xDist = playerCell.x - m_Cell.x;
        int yDist = playerCell.y - m_Cell.y;

        int absXDist = Mathf.Abs(xDist);
        int absYDist = Mathf.Abs(yDist);
        
        if((xDist == 0 && absYDist == 1)
           || (yDist == 0 && absXDist == 1)) GameManager.Instance.ChangeFood(3);
        else
        {
            if (absXDist > absYDist)
            {
                if (!TryMoveInX(xDist)) TryMoveInY(yDist);
            }
            else
            {
                if (!TryMoveInY(yDist)) TryMoveInX(xDist);
            }
        }
    }

    private bool TryMoveInY(int yDist)
    {
        if (yDist > 0) return MoveTo(m_Cell + Vector2Int.up);
        else return MoveTo(m_Cell + Vector2Int.down);
    }

    private bool TryMoveInX(int xDist)
    {
        if (xDist > 0) return MoveTo(m_Cell + Vector2Int.right);
        return MoveTo(m_Cell + Vector2Int.left);
    }
    
}
