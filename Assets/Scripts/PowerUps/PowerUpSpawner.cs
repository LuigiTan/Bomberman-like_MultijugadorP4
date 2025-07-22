using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Collections;



public class PowerUpSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject powerupPrefab;
    [SerializeField] private float spawnCooldown = 30f;

    private Transform[] spawnPoints;
    private GameObject currentPowerup;

    void Start()
    {
        if (!isServer) return;

        // Obtiene todos los puntos de spawn hijos
        spawnPoints = GetComponentsInChildren<Transform>();
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (true)
        {
            if (currentPowerup == null)
            {
                Transform spawnPoint = spawnPoints[Random.Range(1, spawnPoints.Length)];//Me daba error, creo que es porque el spawnPoints[0] es e mismo PowerupManager
                currentPowerup = Instantiate(powerupPrefab, spawnPoint.position, Quaternion.identity);
                NetworkServer.Spawn(currentPowerup);
                Debug.Log("PowerUp Spawneado");
            }

            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
