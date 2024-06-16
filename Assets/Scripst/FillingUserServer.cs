using Photon.Pun;
using UnityEngine;

public class FillingUserServer : fillingUser
{
    [SerializeField] private PhotonView _photonView;
    private CellsGrid _cellsGrid;

    public override void Awake()
    {
        if(_photonView.IsMine)
            base.Awake();
    }

    public override void Start()
    {
        _cellsGrid = CellsGrid.Instance;
        base.Start();
    }

    public override void ChooseCell(Cell cell)
    {
        if(_photonView.IsMine)
        {
            base.ChooseCell(cell);
            _photonView.RPC(nameof(SendValueCell), RpcTarget.Others, cell.X, cell.Y);
        }
    }

    public override void PutValue(int value)
    {
        if(_photonView.IsMine)
        {
            base.PutValue(value);
            _photonView.RPC(nameof(SyncPutValue), RpcTarget.Others, value);
        }
    }

    [PunRPC]
    public void SyncPutValue(int value)
    {
        base.PutValue(value);
    }

    [PunRPC]
    public void SendValueCell(int x, int y)
    {
        var cell = _cellsGrid.GetCellByPosition(x, y);
        base.ChooseCell(cell);
    }
}
