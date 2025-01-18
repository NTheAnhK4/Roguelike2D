using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : CellObject
{
    public int AmountGranted = 10;
    public override void PlayerEntered(Vector2Int m_CellPosition)
    {
        base.PlayerEntered(m_CellPosition);
        PoolingManager.Despawn(gameObject);
        GameManager.Instance.BoardManager.SetCellContainObject(m_CellPosition,null);
        GameManager.Instance.ChangeFood(AmountGranted);
    }
}
