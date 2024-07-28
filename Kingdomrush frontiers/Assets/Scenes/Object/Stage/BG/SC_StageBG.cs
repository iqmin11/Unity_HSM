using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SC_StageBG : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            BGList.Add(Instantiate(BGPrefabs[i]));
            BGList[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStageBG(int CurStage)
    {
        for(int i = 0; i < BGList.Count; i++)
        {
            if(CurStage == i)
            {
                BGList[i].SetActive(true);
                continue;
            }

            BGList[i].SetActive(false);
        }
    }

    [SerializeField]
    private List<GameObject> BGPrefabs;

    private List<GameObject> BGList = new List<GameObject>();
}
