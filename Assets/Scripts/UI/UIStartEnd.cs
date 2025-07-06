using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartEnd : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject wonPanel;
    [SerializeField] private GameObject lostPanel;
    
    [SerializeField] private GameObject player;
    private Vector3 playerStartPos;
    private List<GameObject> coinsParent = new List<GameObject>();

    public static event Action<int> OnAnimChanged;
    public static event Action OnTriggerReset;
    public static event Action OnGameStarted;

    private void OnEnable()
    {
        Obstacle.OnGameOver += GameOver;
        EndLine.OnGameWon += GameWon;

        playerStartPos = player.transform.position;

        GameObject[] parents = GameObject.FindGameObjectsWithTag("CoinsParent");

        foreach (GameObject parent in parents)
            coinsParent.Add(parent);
    }

    public void OnClickPlay()
    {
        startPanel.SetActive(false);
        OnGameStarted?.Invoke();
        StartCoroutine(GameStart());
    }

    public void OnClickRestart()
    {
        player.transform.position = playerStartPos;
        wonPanel.SetActive(false);
        lostPanel.SetActive(false);
        startPanel.SetActive(true);
        OnAnimChanged?.Invoke(0);

        foreach(GameObject parent in coinsParent)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                if (!parent.transform.GetChild(i).gameObject.activeInHierarchy)
                    parent.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void OnClickQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSecondsRealtime(LevelManager.startScaleDuration);
        OnAnimChanged?.Invoke(1);
    }

    public void GameWon()
    {
        OnAnimChanged?.Invoke(0);
        wonPanel.SetActive(true);
    }

    public void GameOver() => StartCoroutine(GameEnd());

    private IEnumerator GameEnd()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        player.transform.GetChild(player.transform.childCount - 1).localScale = Vector3.zero;
        lostPanel.SetActive(true);
    }
}