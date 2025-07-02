using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Pos z x * 170

    [SerializeField] private GameObject Environment;
    [SerializeField] private List<GameObject> levelsPool;

    [Range(2, 10)]
    [SerializeField] private int levelSize;

    private void Start()
    {
        Instantiate(levelsPool[0], Environment.transform);

        int lastIndex = 0;
        int currentIndex = 0;

        for (int i = 0; i < levelSize - 1; i++)
        {
            while (lastIndex == currentIndex)
                currentIndex = (int) Mathf.Floor(Random.Range(0, levelsPool.Count));

            GameObject LevelPiece = Instantiate(levelsPool[currentIndex], Environment.transform);
            
            if (i == levelSize - 2)
            {
                Transform finishLine = LevelPiece.transform.GetChild(LevelPiece.transform.childCount - 1);
                finishLine.gameObject.SetActive(true);
            }

            LevelPiece.transform.position = new Vector3(LevelPiece.transform.position.x, LevelPiece.transform.position.y, 
                LevelPiece.gameObject.transform.position.z + (i + 1) * 170);

            //LevelPiece.SetActive(false);
            lastIndex = currentIndex;

        }
    }
}
