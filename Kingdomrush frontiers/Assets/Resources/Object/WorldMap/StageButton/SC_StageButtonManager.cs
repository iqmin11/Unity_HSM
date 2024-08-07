using UnityEngine;

public class SC_StageButtonManager : MonoBehaviour
{
    [SerializeField] //이건 어떤 역할인지 좀 공부를 해야할것같음.
    private GameObject StageButtonPrefab;

    //이런식의 설계가 될듯. 그러면... StageButton에 대한 것 먼저 만드는게 맞을 것 같습니다.
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
        //만들고 위치정보 받아서 위치 시키기

        //GameObject Clone = Instantiate(StageButtonPrefab);
        //TileInfo Tile = Clone.GetComponent<TileInfo>();
        //Button TileButton = Clone.GetComponent<Button>();
        //GridInfo[y].Add(Tile);
        //GridButton[y].Add(TileButton);
        //Tile.Setup(x, y, this);
    }
}
