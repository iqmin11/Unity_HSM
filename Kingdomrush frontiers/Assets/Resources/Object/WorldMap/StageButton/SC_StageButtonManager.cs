using UnityEngine;

public class SC_StageButtonManager : MonoBehaviour
{
    [SerializeField] //�̰� � �������� �� ���θ� �ؾ��ҰͰ���.
    private GameObject StageButtonPrefab;

    //�̷����� ���谡 �ɵ�. �׷���... StageButton�� ���� �� ���� ����°� ���� �� �����ϴ�.
    //List<StageButtonPrefab> StageButtons;
    //List<Vector2> StageButtonsPosData;

    // Start is called before the first frame update
    void Start()
    {
        InitStageButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitStageButton()
    {
        //����� ��ġ���� �޾Ƽ� ��ġ ��Ű��

        //GameObject Clone = Instantiate(StageButtonPrefab);
        //TileInfo Tile = Clone.GetComponent<TileInfo>();
        //Button TileButton = Clone.GetComponent<Button>();
        //GridInfo[y].Add(Tile);
        //GridButton[y].Add(TileButton);
        //Tile.Setup(x, y, this);
    }
}
