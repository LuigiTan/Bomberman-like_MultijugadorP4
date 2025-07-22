using System.Collections.Generic;
using UnityEngine;
using Mirror;



public class PenetrationPowerUp : NetworkBehaviour
{
    //public int extraBulletLife = 1;
    public int NumberOfPoweredUpShots = 5;

    void OnTriggerEnter(Collider other)
    {
        if (!isServer) return;

        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {

                stats.ActivateTemporaryPowerup(NumberOfPoweredUpShots);

                //stats.UpgradeBulletLife(extraBulletLife);
                NetworkServer.Destroy(gameObject);
            }
        }
    }
}
