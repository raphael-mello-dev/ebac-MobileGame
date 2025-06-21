using UnityEngine;

public class UIStartEnd : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject endPanel;

    public void OnClickPlay()
    {
        startPanel.SetActive(false);
    }

    public void OnClickRestart()
    {

    }

    public void OnClickQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void GameOver() => endPanel.SetActive(true);
}