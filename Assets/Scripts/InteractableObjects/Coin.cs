using UnityEngine;

public class Coin : MonoBehaviour
{
    private Vector3 initialPos;

    private bool magnetOn;

    private GameObject target;

    private void Awake() => initialPos = transform.position;

    private void OnEnable() => transform.position = initialPos;

    private void OnDisable()
    {
        magnetOn = false;
        target = null;
    }

    private void Update()
    {
        if (magnetOn)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 1.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Player"))
            gameObject.SetActive(false);
       else if (other.gameObject.CompareTag("Magnet"))
        {
            magnetOn = true;
            target = other.gameObject;
        }
    }
}