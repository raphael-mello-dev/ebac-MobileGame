using System;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    public static event Action OnGameWon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().CanMove = false;
            OnGameWon?.Invoke();
        }
    }
}