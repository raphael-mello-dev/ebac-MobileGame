using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Pos z x * 170

    [SerializeField] private GameObject Environment;
    [SerializeField] private List<GameObject> levelsPool;

    [Range(2, 10)]
    [SerializeField] private int levelSize;

    [SerializeField] Color[] colors;

    private void Start()
    {

        int lastIndex = 0, currentIndex = 0;
        int floorColorIndex, borderColorIndex;
        GameObject LevelPiece;

        for (int i = 0; i < levelSize - 1; i++)
        {
            while (lastIndex == currentIndex)
                currentIndex = (int) Mathf.Floor(Random.Range(0, levelsPool.Count));

            if (i == 0)
                LevelPiece = Instantiate(levelsPool[0], Environment.transform);
            else
                LevelPiece = Instantiate(levelsPool[currentIndex], Environment.transform);
            
            floorColorIndex = Random.Range(0, colors.Length - 1);
            LevelPiece.transform.GetChild(0).GetComponent<Renderer>().material.color = colors[floorColorIndex];

            do { borderColorIndex = Random.Range(0, colors.Length - 1); }
            while (floorColorIndex == borderColorIndex);

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
}
