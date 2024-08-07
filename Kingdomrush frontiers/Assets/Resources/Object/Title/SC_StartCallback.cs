using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCallback : MonoBehaviour
{
    public void OnClickFunc()
    {
        SceneManager.LoadScene("StageScene");
    }
}
