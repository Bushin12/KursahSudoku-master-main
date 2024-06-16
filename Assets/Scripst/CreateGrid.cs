using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CreateGrid : MonoBehaviour
{

    public static int CountActiveCell = 40;
    public int[,] Map { protected set; get; } = new int[n * n, n * n];
    public bool[,] CellActive { protected set; get; } = new bool[9, 9];
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private GameObject _cellPutPrefab;
    public Cell[,] _board { private set; get; } = new Cell[9, 9];
    private List<Cell> _hideCell = new List<Cell>();
    private Vector2 _position;
    private const int n = 3;

    public virtual void Start()
    {
        CreateStartPosition();
        GenerateMap();
        //_filling.SetGrid(_board,CellActive);
    }
    public void CreateStartPosition()
    {
        _position -= Vector2.right * (((float)Mathf.Sqrt(_board.Length) * _cellPrefab.transform.localScale) / 2);
        _position += Vector2.up * (((float)Mathf.Sqrt(_board.Length) * _cellPrefab.transform.localScale) / 2);
        _position += Vector2.one / 2;
    }
    private void GenerateMap()
    {
        for (int i = 0; i < n * n; i++)
        {
            for (int j = 0; j < n * n; j++)
            {
                CellActive[i, j] = true;
                Map[i, j] = (i * n + i / n + j) % (n * n) + 1;
            }
        }
        for (int i = 0; i < 40; i++)
        {
            ShuffleMap(Random.Range(0, 5));
        }
        Print();
        RandomChoise();
        HideCells();
        SetGridPlayer();
    }

    public void SetGridPlayer()
    {
        CellsGrid.Instance.Inittialize(_board, CellActive);
    }

    public void Print()
    {
        Vector2 lovalPosition = _position;
        List<Cell> _hideCellSort = new List<Cell>();
        for (int i = 0; i < 9; i++)
        {
            lovalPosition.x = _position.x;
            for (int y = 0; y < 9; y++)
            {
                Cell cell = Instantiate(_cellPrefab).GetComponent<Cell>();
                _board[i, y] = cell;
                _hideCellSort.Add(cell);
                cell.transform.position = lovalPosition;
                cell.SetValue(Map[i, y], i, y);
                lovalPosition += Vector2.right * (Vector2)_cellPrefab.transform.localScale;
            }
            lovalPosition += Vector2.down * (Vector2)_cellPrefab.transform.localScale;
        }
        _hideCellSort = _hideCellSort.OrderBy(p=>p.Value).ToList();
        for (int i = 0; i < _hideCellSort.Count;)
        {
            Cell[] values = new Cell[9];
            for (int y = 0; y < 9; y++)
            {
                values[y] = _hideCellSort[i+y];
            }
            for (int x = i; x < 9+i; x++)
            {
                _hideCellSort[x].SetCellsValue(values);
            }

            i += 9;
        }
    }

    private void ShuffleMap(int i)
    {
        switch (i)
        {
            case 0:
                MatrixTransposition();
                break;
            case 1:
                SwapRowsInBlock();
                break;
            case 2:
                SwapColumnsInBlock();
                break;
            case 3:
                SwapBlocksInRow();
                break;
            case 4:
                SwapBlocksInColumn();
                break;
            default:
                MatrixTransposition();
                break;
        }
    }
    private void SwapBlocksInColumn()
    {
        var block1 = Random.Range(0, n);
        var block2 = Random.Range(0, n);
        while (block1 == block2)
            block2 = Random.Range(0, n);
        block1 *= n;
        block2 *= n;
        for (int i = 0; i < n * n; i++)
        {
            var k = block2;
            for (int j = block1; j < block1 + n; j++)
            {
                var temp = Map[i, j];
                Map[i, j] = Map[i, k];
                Map[i, k] = temp;
                k++;
            }
        }
    }
    private void SwapBlocksInRow()
    {
        var block1 = Random.Range(0, n);
        var block2 = Random.Range(0, n);
        while (block1 == block2)
            block2 = Random.Range(0, n);
        block1 *= n;
        block2 *= n;
        for (int i = 0; i < n * n; i++)
        {
            var k = block2;
            for (int j = block1; j < block1 + n; j++)
            {
                var temp = Map[j, i];
                Map[j, i] = Map[k, i];
                Map[k, i] = temp;
                k++;
            }
        }
    }
    private void SwapRowsInBlock()
    {
        var block = Random.Range(0, n);
        var row1 = Random.Range(0, n);
        var line1 = block * n + row1;
        var row2 = Random.Range(0, n);
        while (row1 == row2)
            row2 = Random.Range(0, n);
        var line2 = block * n + row2;
        for (int i = 0; i < n * n; i++)
        {
            var temp = Map[line1, i];
            Map[line1, i] = Map[line2, i];
            Map[line2, i] = temp;
        }
    }
    private void SwapColumnsInBlock()
    {
        var block = Random.Range(0, n);
        var row1 = Random.Range(0, n);
        var line1 = block * n + row1;
        var row2 = Random.Range(0, n);
        while (row1 == row2)
            row2 = Random.Range(0, n);
        var line2 = block * n + row2;
        for (int i = 0; i < n * n; i++)
        {
            var temp = Map[i, line1];
            Map[i, line1] = Map[i, line2];
            Map[i, line2] = temp;
        }
    }
    private void MatrixTransposition()
    {
        int[,] tMap = new int[n * n, n * n];
        for (int i = 0; i < n * n; i++)
        {
            for (int j = 0; j < n * n; j++)
            {
                tMap[i, j] = Map[j, i];
            }
        }
        Map = tMap;
    }

    private void RandomChoise()
    {
        for (int i = 0; i < Map.Length - CountActiveCell;)
        {
            var x = Random.Range(0, 9);
            var y = Random.Range(0, 9);
            if (CellActive[x, y] == true)
            {
                CellActive[x, y] = false;
                i++;
            }
        }
    }

    public void HideCells()
    {
        for (int x = 0; x < CellActive.GetLength(0); x++)
        {
            for (int y = 0; y < CellActive.GetLength(1); y++)
            {
                if (CellActive[x,y] == false)
                {
                    _board[x, y].Hide();
                    _hideCell.Add(_board[x, y]);
                }
            }
        }

        _hideCell = _hideCell.OrderBy(p=>p.Value).ToList();
        int localValue = 0;
        int countValue = 0;
        Vector2 localPosition = _position;
        localPosition += Vector2.down * 12;
        for (int i = 0; i < _hideCell.Count; i++)
        {
            if (localValue == 0)
            {
                localValue = _hideCell[i].Value;
            }

            if(localValue == _hideCell[i].Value)
            {
                countValue++;
            }
            else
            {
                CreatePutCell(localValue, countValue, localPosition);
                countValue = 1;
                localValue = _hideCell[i].Value;
                localPosition += Vector2.right * (Vector2)_cellPrefab.transform.localScale;
            }
        }
        CreatePutCell(localValue, countValue, localPosition);
    }
    private void CreatePutCell(int value, int count, Vector2 position)
    {
        PutCell put = Instantiate(_cellPutPrefab).GetComponent<PutCell>();
        put.Inicialize(value, count);
        put.transform.position = position;
    }
}
