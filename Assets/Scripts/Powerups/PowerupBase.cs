using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public abstract class PowerupBase: MonoBehaviour
{
    [SerializeField] protected string powerupName;
    [SerializeField] protected float duration;

    [SerializeField] protected TextMeshPro powerupText;

    public virtual void Start() => powerupText.text = powerupName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlayerScale(other.gameObject));
            StartCoroutine(Powerup(other.gameObject));
        }
    }

    public virtual IEnumerator Powerup(GameObject player)
    {
        yield return null;
    }

    private IEnumerator PlayerScale(GameObject player)
    {
        player.transform.GetChild(player.transform.childCount - 1).DOScale(1.2f, 0.2f).SetEase(Ease.InBounce);
        yield return new WaitForSecondsRealtime(0.2f);
        player.transform.GetChild(player.transform.childCount - 1).DOScale(1f, 0.2f).SetEase(Ease.InBounce);
    }
}