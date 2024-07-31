using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCallback : MonoBehaviour
{
    public void OnClickFunc()
    {
        SceneManager.LoadScene("StageScene");
    }
}
