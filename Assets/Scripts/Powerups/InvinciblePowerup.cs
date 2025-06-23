using System.Collections;
using UnityEngine;

public class InvinciblePowerup : PowerupBase
{

    public override IEnumerator Powerup(GameObject player)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        powerupText.enabled = false;
        player.GetComponent<PlayerController>().IsInvincible = true;        
        yield return new WaitForSeconds(duration);
        player.transform.GetChild(1).gameObject.SetActive(false);
        player.GetComponent<PlayerController>().IsInvincible = false;        
        powerupText.enabled = true;
    }
}