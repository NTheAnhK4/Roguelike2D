using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodObject : CellObject
{
    public int AmountGranted = 10;
    public override void PlayerEntered()
    {
        base.PlayerEntered();
        PoolingManager.Despawn(gameObject);
        GameManager.Instance.BoardManager.SetCellContainObject(m_Cell,null);
        GameManager.Instance.ChangeFood(AmountGranted);
    }
}
