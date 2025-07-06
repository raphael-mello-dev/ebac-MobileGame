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

    [SerializeField] Color[] colors;

    [SerializeField] private float scaleDuration;

    public static float startScaleDuration { get; private set; }

    private void Awake() => startScaleDuration = scaleDuration;

    private void OnEnable()
    {
        UIStartEnd.OnGameStarted += PlayerScaleCallBack;
    }

    private void OnDisable()
    {
        UIStartEnd.OnGameStarted -= PlayerScaleCallBack;
    }

    private void Start()
    {
        int lastIndex = 0, currentIndex = 0;
        int floorColorIndex, borderColorIndex;
        GameObject LevelPiece;

        floorColorIndex = Random.Range(0, colors.Length - 1);
        
        do { borderColorIndex = Random.Range(0, colors.Length - 1); }
        while (floorColorIndex == borderColorIndex);

        for (int i = 0; i < levelSize - 1; i++)
        {
            while (lastIndex == currentIndex)
                currentIndex = (int) Mathf.Floor(Random.Range(0, levelsPool.Count));

            if (i == 0)
                LevelPiece = Instantiate(levelsPool[0], Environment.transform);
            else
                LevelPiece = Instantiate(levelsPool[currentIndex], Environment.transform);
            
            LevelPiece.transform.GetChild(0).GetComponent<Renderer>().material.color = colors[floorColorIndex];
            LevelPiece.transform.GetChild(1).GetComponent<Renderer>().material.color = colors[borderColorIndex];
            LevelPiece.transform.GetChild(2).GetComponent<Renderer>().material.color = colors[borderColorIndex];

            if (i == levelSize - 2)
            {
                Transform finishLine = LevelPiece.transform.GetChild(LevelPiece.transform.childCount - 1);
                finishLine.gameObject.SetActive(true);
            }

            LevelPiece.transform.position = new Vector3(LevelPiece.transform.position.x, LevelPiece.transform.position.y, 
                LevelPiece.gameObject.transform.position.z + i * 169);

            //LevelPiece.SetActive(false);
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
}