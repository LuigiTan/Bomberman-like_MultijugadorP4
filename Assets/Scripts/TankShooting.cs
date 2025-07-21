using System.Collections.Generic;
using UnityEngine;
using Mirror;



public class TankShooting : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float bulletSpeed = 20f;

    [Header("Cooldown")]
    public float fireCooldown = 0.5f;//Es en segundos
    private float lastFireTime = 0f;

    void Update()
    {
        if (!isLocalPlayer) return;
        //Estupidamente poco optimo porque estoy guardando constantemente la ultima vez que se disparo
        //Pero como es multiplayer no estoy seguro si usar un yield return perjudique algo
        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + fireCooldown)
        {
            lastFireTime = Time.time;
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

        //Esto deberia asignarle el owner a la bala
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetOwner(gameObject);

        NetworkServer.Spawn(bullet);
    }
}
