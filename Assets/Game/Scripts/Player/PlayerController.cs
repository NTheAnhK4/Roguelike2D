
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BoardManager m_Board;
    private Vector2Int m_CellPosition;
    private bool m_IsGameOver = false;

    public float MoveSpeed = 5f;
    private bool m_IsMoving = false;
    private Vector3 m_MoveTarget;

    private void Awake()
    {
        ObserverManager.Attach(EventId.Lose, param => { m_IsGameOver = true;});
    }

    public void Spawn(BoardManager boardManager, Vector2Int cell)
    {
        m_Board = boardManager;
        MoveTo(cell, true);
    }

    public void MoveTo(Vector2Int cell, bool immediate)
    {
        m_CellPosition = cell;

        if (immediate)
        {
            m_IsMoving = false;
            transform.position = m_Board.CellToWorld(m_CellPosition);
            
        }
        m_IsMoving = true;
        m_MoveTarget = m_Board.CellToWorld(m_CellPosition);
    }

    public void Init()
    {
        m_IsMoving = false;
        m_IsGameOver = false;
    }
    private void Update()
    {
        
        if (m_IsGameOver)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame) ObserverManager.Notify(EventId.NewGame);
            return;
        }

        if (m_IsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_MoveTarget, MoveSpeed * Time.deltaTime);
            if (transform.position == m_MoveTarget)
            {
                m_IsMoving = false;
                var cellData = m_Board.GetCellData(m_CellPosition);
                if(cellData.ContainedObject != null) cellData.ContainedObject.PlayerEntered();
            }
            return;
        }
        Vector2Int newCellTarget = m_CellPosition;
        bool hasMoved = false;
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y += 1;
            hasMoved = true;
        }

        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y -= 1;
            hasMoved = true;
        }

        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x += 1;
            hasMoved = true;
        }

        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x -= 1;
            hasMoved = true;
        }
    
        if (hasMoved)
        {
            BoardManager.CellData cellData = m_Board.GetCellData(newCellTarget);
            if (cellData != null && cellData.Passable)
            {
                GameManager.Instance.TurnManager.Tick();
                if(cellData.ContainedObject == null) MoveTo(newCellTarget,false);
                else if (cellData.ContainedObject.PlayerWantsToEnter())
                {
                    MoveTo(newCellTarget,false);
                    
                }
                
            }
        }
    }
}
