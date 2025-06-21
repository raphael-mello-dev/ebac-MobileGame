using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static event Action OnGameOver;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().CanMove = false;
            OnGameOver.Invoke();
        }
    }
}
