# BomberTANK

**BomberTANK** es un juego multijugador en Unity desarrollado con **Mirror**. Es un juego PvP free-for-all en el que controlas un tanque que puede moverse, disparar balas destructivas, destruir paredes del mapa, eliminar a otros jugadores y recoger un powerUP que aumenta la penetración de tus disparos.
El juego fue realizado para la materia de Multijugador para Videojuegos de 6to semestre de Ingeniería de Programación de Videojuegos 
---

## Características del juego
- Hecho en Unity 6
- Multijugador en red usando Mirror.
- Movimiento y rotación tipo tanque con colisiones.
- Las balas que se disparan tienen vida útil la cual puede ser modificada
- Estás a su vez destruyen paredes que se denominen como "Destructibles" con su respectiva Tag
- Las balas también tratarán de dañar cualquier objeto con la tag de Player y reconocen cuál player fue el que las disparó para que no te hagas daño a ti mismo.
- Los jugadores pueden morir y respawnear después de un rato establecido en el RespawnManager (tiene un bug por el momento que no pude arreglar)
- Power Ups: tiene un sistema de spawn de power ups que va a spawnear un prefab establecido en cualquier punto al azar basándose en los empties que tenga de hijos el manager.
- Por el momento el único power up que hay es uno que sube la vida útil de las balas para poder hacer daño a varias cosas a la vez

- Así como hay paredes destructibles, se puede poner la tag de "Indestructible" a un objeto para que la bala no le haga nada y esta se destruya al tocarlo
- Tiene un sistema de leaderboard la cual por el momento utiliza la ID de los jugadores para mostrar quien es quien pero en un futuro quiero hacer que se pueda poner un username personalizado

---

## Estructura de Scripts

| Script | Función |
|--------|---------|
| `Tanks_NetworkManager.cs` | Controlador principal de red. Gestiona conexiones, spawns y sincronizacion. |
| `PlayerTanksController.cs` | Movimiento del jugador. |
| `TankShooting.cs` | Sistema de disparo. Instancia las balas y asigna el `owner` de estas para tanto la leaderboard como para evitar que tus balas te hagan daño a ti|
| `Bullet.cs` | Comportamiento de la bala. Detecta la colisión y aplica el daño a la pared o jugador. |
| `TankHealth.cs` | Maneja la vida del tanque y el reconocimiento de cuando te mueres  |
| `TankRespawnManager.cs` | Se encarga de respawnear a los jugadores (el bug está aquí)|
| `PenetrationPowerUp.cs` | Aqui se aplica y está el funcionamiento de el efecto de penetración de las balas .|
| `PowerUpSpawner.cs` | Spawnea aleatoriamente power-ups en el mapa basándose en los hijos del empty al que se lo pongas. |
| `DestructibleObjectHealth.cs` | Vida de objetos del mapa que pueden ser destruidos. Tiene una función disponible por si se quiere añadir un efecto al destruir las paredes o por si se quieren respawnear como los jugadores.|
| `PlayerStats.cs` | Aqui se registran las kills de los jugadores y se le habla a la leaderboard. Además aquí se verifica y establece cuánta vida útil van a tener las balas normales, cuánta vida extra les va a dar el power up y la cantidad de balas extra que se van a recibir por power up.|
| `LeaderBoardManager.cs` | Administra y muestra la puntuación en pantalla. Principalmente se encarga de que la leaderboard aparezca de mayor número de kills a menor|

---

## Cómo jugar

1. Asegúrate de tener **Mirror** instalado en tu proyecto Unity así como el Multiplayer Play Mode.
2. Ejecuta la escena principal donde se encuentra el `Tanks_NetworkManager`.
3. Se puede abrir el juego a local también para tratar de hacer un server, pero las interacciones no están testeadas para eso.
4. Mueve el tanque, dispara, destruye objetos y recoge el power-up para mejorar tus disparos. Será una plataforma con 3 balas rojas en ella

---

## Requisitos

- Unity 6000.0.40f o superior (de preferencia usa la misma que si no truena)
- Mirror Networking (instalado desde Unity Asset Store o GitHub)

---

## Ideas a futuro y pendientes 

- Más tipos de power-ups
- Efectos visuales 
- Sistema para que al llegar a un cierto número de kills alguien gane
- Mejora del UI
- Arreglo de bugs al respawnear 

---

## Autor
Este proyecto fue realizado por Gibran García.
. El sistema de red está basado en Mirror, y los todos los assets fueron sacados de la unity Asset Store

---
