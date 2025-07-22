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
        //Tal vez poco optimo porque estoy guardando constantemente la ultima vez que se disparo
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

        //[HERE] Se van a modificar y añadir lineas para agregar el power up. Las voy a marcar para quitarlas o cambiarlas si es necesario
        PlayerStats stats = GetComponent<PlayerStats>();//Referencia a los stats
        //Antes no estaba 


        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);


        //[AQUI]   Esto antes estaba en donde esta comentado ahora
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        //

        //[AQUI ]Esta seccion acontinuacion antes no estaba
        int currentLife = stats.GetCurrentBulletLife(); // Evalúa si hay power-up activo
        bulletScript.SetBulletLife(currentLife);
        stats.UseBullet(); // Disminuye los disparos restantes del power-up
        //

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);

        //[AQUI]  Esto deberia asignarle el owner a la bala
        //Bullet bulletScript = bullet.GetComponent<Bullet>();
        
        //Regresar lo de arriba de ser necesario

        bulletScript.SetOwner(gameObject);

        NetworkServer.Spawn(bullet);
    }
}
