using System.Collections;
using UnityEngine;
using TMPro;

public abstract class PowerupBase: MonoBehaviour
{
    [SerializeField] protected string powerupName;
    [SerializeField] protected float duration;

    [SerializeField] protected TextMeshPro powerupText;

    public virtual void Start() => powerupText.text = powerupName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            StartCoroutine(Powerup(other.gameObject));
    }

    public virtual IEnumerator Powerup(GameObject player)
    {
        yield return null;
    }
}