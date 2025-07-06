using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject Environment;
    [SerializeField] private GameObject playerReference;
    [SerializeField] private List<GameObject> levelsPool;
    
    [Range(2, 10)]
    [SerializeField] private int levelSize;

    int floorColorIndex, borderColorIndex;
    [SerializeField] Color[] colors;

    [SerializeField] private float scaleDuration;

    public static float startScaleDuration { get; private set; }

    private void Awake() => startScaleDuration = scaleDuration;

    private void OnEnable()
    {
        UIStartEnd.OnGameStarted += PlayerScaleCallBack;
        UIStartEnd.OnGameStarted += LevelsColorChange;
    }

    private void OnDisable()
    {
        UIStartEnd.OnGameStarted -= PlayerScaleCallBack;
        UIStartEnd.OnGameStarted -= LevelsColorChange;
    }

    private void Start()
    {
        int lastIndex = 0, currentIndex = 0;
        GameObject LevelPiece;

        floorColorIndex = Random.Range(0, colors.Length - 1);
        
        do { borderColorIndex = Random.Range(0, colors.Length - 1); }
        while (floorColorIndex == borderColorIndex);

        for (int i = 0; i < levelSize; i++)
        {
            while (lastIndex == currentIndex)
                currentIndex = (int) Mathf.Floor(Random.Range(0, levelsPool.Count));

            if (i == 0)
                LevelPiece = Instantiate(levelsPool[0], Environment.transform);
            else
                LevelPiece = Instantiate(levelsPool[currentIndex], Environment.transform);

            if (i == levelSize - 2)
            {
                Transform finishLine = LevelPiece.transform.GetChild(LevelPiece.transform.childCount - 1);
                finishLine.gameObject.SetActive(true);
            }

            LevelPiece.transform.position = new Vector3(LevelPiece.transform.position.x, LevelPiece.transform.position.y, 
                LevelPiece.gameObject.transform.position.z + i * 169);

            lastIndex = currentIndex;
        }
    }

    private void PlayerScaleCallBack() => StartCoroutine(PlayerScale());

    private IEnumerator PlayerScale()
    {
        playerReference.transform.GetChild(playerReference.transform.childCount - 1).DOScale(1, scaleDuration);
        yield return new WaitForSecondsRealtime(scaleDuration);
        playerReference.GetComponent<PlayerController>().CanMove = true;
    }

    private void LevelsColorChange()
    {
        for (int i = 0; i < Environment.transform.childCount; i++)
        {
            Environment.transform.GetChild(i).transform.GetChild(0).GetComponent<Renderer>().material.DOColor(colors[floorColorIndex], 2f);
            Environment.transform.GetChild(i).transform.GetChild(1).GetComponent<Renderer>().material.DOColor(colors[borderColorIndex], 2f);
            Environment.transform.GetChild(i).transform.GetChild(2).GetComponent<Renderer>().material.DOColor(colors[borderColorIndex], 2f);
        }
    }
}