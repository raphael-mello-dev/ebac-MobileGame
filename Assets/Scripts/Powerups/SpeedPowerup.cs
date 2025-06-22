using System.Collections;
using UnityEngine;

public class SpeedPowerup : PowerupBase
{
    [SerializeField] private float speedIncrease;

    public override IEnumerator Powerup(GameObject player)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        player.GetComponent<PlayerController>().SpeedIncrease = speedIncrease;
        powerupText.enabled = false;
        yield return new WaitForSeconds(duration);
        player.GetComponent<PlayerController>().SpeedIncrease = 0;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        powerupText.enabled = true;
    }
}