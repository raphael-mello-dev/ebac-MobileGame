using System.Collections;
using UnityEngine;

public class MagnetPowerup : PowerupBase
{
    public override IEnumerator Powerup(GameObject player)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        player.transform.GetChild(1).gameObject.SetActive(true);
        powerupText.enabled = false;
        yield return new WaitForSeconds(duration);
        player.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        powerupText.enabled = true;
    }
}