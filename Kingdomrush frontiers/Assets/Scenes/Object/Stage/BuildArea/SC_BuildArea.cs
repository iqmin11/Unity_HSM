using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BuildArea : MonoBehaviour
{
    private void Awake()
    {
        ButtonInst = Instantiate(ButtonPrefab, gameObject.transform);
        ButtonSetting = ButtonInst.GetComponent<SC_BuildAreaButton>();
        ButtonSetting.Click = () =>
        {
            ChildTowerInst = Instantiate(ArtilleryTowerPrefab, gameObject.transform);
            ButtonInst.SetActive(false);
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    private GameObject ButtonPrefab;
    private GameObject ButtonInst;
    private SC_BuildAreaButton ButtonSetting;

    [SerializeField]
    private GameObject RangedTowerPrefab;
    [SerializeField]
    private GameObject MagicTowerPrefab;
    [SerializeField]
    private GameObject ArtilleryTowerPrefab;

    private GameObject ChildTowerInst;
}
