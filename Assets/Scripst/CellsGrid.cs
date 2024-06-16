using UnityEngine;
using static Cell;

public class CellsGrid : MonoBehaviour
{
    public static CellsGrid Instance { private set; get; }
    private bool[,] _activeCell;
    private Cell[,] _grid;

    private void Awake()
    {
        Instance = this;
    }

    public void Inittialize(Cell[,] grid, bool[,] startActiveCell)
    {
        _grid = grid;
        _activeCell = startActiveCell;
    }

    public void OnCellGrid(int x, int y)
    {
        _activeCell[x, y] = true;
        CheckFinished();
    }

    private void CheckFinished()
    {
        foreach (var item in _activeCell)
        {
            if (item == false)
            {
                print(item.ToString());
                return;
            }
        }
        GameUi.Instance.Win();
        //GameActive = false;
        //_timer.StopTimer();
    }

    public void CrossChange(int x, int y, bool isChooise)
    {
        stateCell stateSomeValues = isChooise == true ? stateCell.Values : stateCell.NotChoose;
        stateCell line = isChooise == true ? stateCell.Choose : stateCell.NotChoose;

        for (int i = 0; i < 9; i++)
        {
            _grid[i, y].ChangeBackGround(line);
            _grid[x, i].ChangeBackGround(line);
        }
        if (GetCellByPosition(x,y).IsHade == false)
        {
            for (int i = 0; i < 9; i++)
            {
                _grid[x, y]._cellsThisValues[i].ChangeBackGround(stateSomeValues);
            }
        }
    }

    public Cell GetCellByPosition(int x, int y)
    {
        if (_grid.GetLength(0) < x || _grid.GetLength(1) < y)
        {
            throw new System.Exception();
        }
        else
        {
            return _grid[x, y];
        }
    }
}
