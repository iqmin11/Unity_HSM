using UnityEngine;

public class ExitCallback : MonoBehaviour
{
    public void OnClickFunc()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
