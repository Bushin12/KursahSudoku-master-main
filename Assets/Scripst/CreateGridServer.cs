using Photon.Pun;

public class CreateGridServer : CreateGrid
{
    public static CreateGridServer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            base.Start();
        else
            CreateStartPosition();
    }

    public void SetGrid(int[,] map, bool[,] ActiveCells)
    {
        Map = map;
        CellActive = ActiveCells;
        Print();
        HideCells();
        SetGridPlayer();
    }
}
