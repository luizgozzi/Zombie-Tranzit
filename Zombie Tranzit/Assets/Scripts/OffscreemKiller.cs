using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenKiller : MonoBehaviour
{
    public Transform player;
    public float offsetBehind = 10f; // distância atrás do player

    void Update()
    {
        if (player == null) return;

        // Segue o player, sempre atrás
        transform.position = new Vector3(
            player.position.x,
            player.position.y,
            player.position.z - offsetBehind
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Destroy(other.gameObject);
            Debug.Log("Zombie removido: " + other.name);
        }
    }
}