using UnityEngine;

public class UIStartEnd : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject endPanel;
    
    [SerializeField] private GameObject player;
    private Vector3 playerStartPos;

    private void OnEnable()
    {
        Obstacle.OnGameOver += GameOver;
        playerStartPos = player.transform.position;
    }

    public void OnClickPlay()
    {
        startPanel.SetActive(false);
        player.GetComponent<PlayerController>().CanMove = true;
    }

    public void OnClickRestart()
    {
        player.transform.position = playerStartPos;
        endPanel.SetActive(false);
        startPanel.SetActive(true);
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