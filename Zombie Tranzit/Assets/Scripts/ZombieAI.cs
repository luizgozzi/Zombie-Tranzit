using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ZombieAI : MonoBehaviour
{
    public ZombieManager manager; // setado pelo manager quando instanciado (ou arrastar no prefab)
    public Transform player;
    public float followOffsetZ = 2f; // quanto fica atrás do player
    // Rigidbody rb;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p) player = p.transform;
        }

        if (manager == null)
            manager = GameObject.FindAnyObjectByType<ZombieManager>();

        // rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player == null) return;

        float speed = manager != null ? manager.currentZombieSpeed : 2f;
        Vector3 target = new Vector3(player.position.x, transform.position.y, player.position.z - followOffsetZ);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // opcional: olhar pro jogador
        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0;
        if (lookDir.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(lookDir);

        // rb.AddForce(transform.forward * speed, ForceMode.Acceleration);

        // rb.velocity = new Vector3(0,0,speed);
    }

  
    private void OnTriggerEnter(Collider other)
    {
        // se encostar no player -> mata o player e remove o zumbi
        if (other.CompareTag("Player"))
        {
            var pm = other.GetComponent<PlayerMovement>();
            if (pm != null) pm.Die();

            // Notifica o manager para remover este zumbi
            if (manager != null) manager.RemoveZombie(gameObject);
            else Destroy(gameObject);
        }
    }
}