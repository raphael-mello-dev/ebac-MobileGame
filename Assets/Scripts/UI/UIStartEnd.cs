using System;
using UnityEngine;

public class UIStartEnd : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject wonPanel;
    [SerializeField] private GameObject lostPanel;
    
    [SerializeField] private GameObject player;
    private Vector3 playerStartPos;

    [SerializeField] private GameObject coinsParent;

    public static event Action<int> OnAnimChanged;
    public static event Action OnTriggerReset;

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
        OnAnimChanged?.Invoke(1);
    }

    public void OnClickRestart()
    {
        player.transform.position = playerStartPos;
        wonPanel.SetActive(false);
        lostPanel.SetActive(false);
        startPanel.SetActive(true);
        OnAnimChanged?.Invoke(0);

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

    public void GameWon()
    {
        OnAnimChanged?.Invoke(0);
        wonPanel.SetActive(true);
    }

    public void GameOver() => lostPanel.SetActive(true);
}