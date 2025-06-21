using UnityEngine;

public class UIStartEnd : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject wonPanel;
    [SerializeField] private GameObject lostPanel;
    
    [SerializeField] private GameObject player;
    private Vector3 playerStartPos;

    [SerializeField] private GameObject coinsParent;

    private void OnEnable()
    {
        Obstacle.OnGameOver += GameOver;
        EndLine.OnGameWon += GameWon;

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
        wonPanel.SetActive(false);
        lostPanel.SetActive(false);
        startPanel.SetActive(true);

        for (int i = 0; i < coinsParent.transform.childCount; i++)
        {
            if (!coinsParent.transform.GetChild(i).gameObject.activeInHierarchy)
                coinsParent.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void OnClickQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void GameWon() => wonPanel.SetActive(true);

    public void GameOver() => lostPanel.SetActive(true);
}