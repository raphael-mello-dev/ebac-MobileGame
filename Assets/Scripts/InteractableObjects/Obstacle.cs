using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static event Action OnGameOver;
    public static event Action<int> OnAnimChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<PlayerController>().IsInvincible)
        {
            other.transform.position -= new Vector3(0, 0, 1f);
            other.gameObject.GetComponent<PlayerController>().CanMove = false;
            other.gameObject.GetComponent<PlayerController>().DeathParticle.Play();
            OnAnimChanged?.Invoke(2);
            OnGameOver?.Invoke();
        }
    }
}