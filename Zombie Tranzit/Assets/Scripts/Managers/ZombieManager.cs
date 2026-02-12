using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager instance;

    [Header("Refs")]
    public GameObject zombiePrefab;
    public Transform player; // arrastar Player aqui

    [Header("Spawn")]
    public float spawnDistance = 15f; // distância atrás do player onde os zumbis surgem
    public float initialDelay = 5f; // demora inicial antes de spawnar
    public float spawnInterval = 3f; // intervalo entre spawns enquanto < max
    public float minSpawnInterval = 1.0f;

    [Header("Count / speed")]
    public int maxZombies = 20; // limite de multiplicação
    public float baseZombieSpeed = 2f; // velocidade inicial
    public float speedIncreasePerSecond = 0.05f; // quanto aumenta por segundo quando acelerando

    // runtime
    [HideInInspector] public float currentZombieSpeed;
    private List<GameObject> zombies = new List<GameObject>();
    private bool accelerating = false;
    private float spawnTimer = 0f;

    void Awake()
    {
        instance = this;
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        currentZombieSpeed = baseZombieSpeed;
    }

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            // se ainda não atingiu o limite, spawna novos zumbis gradualmente
            if (zombies.Count < maxZombies)
            {
                SpawnOne();
                yield return new WaitForSeconds(spawnInterval);
                // opcional diminuir intervalo aos poucos
                spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval);
            }
            else
            {
                // atingiu o limite → começa a acelerar todos os zumbis
                accelerating = true;
                while (zombies.Count >= maxZombies)
                {
                    currentZombieSpeed += speedIncreasePerSecond * Time.deltaTime;
                    yield return null;
                }
                // quando cair abaixo do limite, parar de acelerar e resetar velocidade
                accelerating = false;
            }
        }
    }

    void SpawnOne()
    {
        if (player == null || zombiePrefab == null) return;

        Vector2 offset = Random.insideUnitCircle * 2f; // raio de espalhamento
        Vector3 spawnPos = player.position - player.forward.normalized * spawnDistance;
        spawnPos += new Vector3(offset.x, 0, offset.y);

        GameObject z = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
        zombies.Add(z);

        // seta o manager no zombie para ele ler a velocidade
        ZombieAI zc = z.GetComponent<ZombieAI>();
        if (zc != null) zc.manager = this;
    }

    // chamado por OffscreenKiller ou por defesa para remover um zumbi específico:
    public void RemoveZombie(GameObject z)
    {
        if (z == null) return;

        if (zombies.Contains(z)) zombies.Remove(z);
        Destroy(z);

        // se não tem mais zumbis, resetar ciclo
        if (zombies.Count == 0)
        {
            ResetCycle();
        }
    }

    // chamada pela defesa: remove 'count' zumbis aleatórios (ou os mais próximos)
    public void RemoveRandomZombies(int count)
    {
        for (int i = 0; i < count && zombies.Count > 0; i++)
        {
            int idx = Random.Range(0, zombies.Count);
            GameObject z = zombies[idx];
            zombies.RemoveAt(idx);
            Destroy(z);
        }

        if (zombies.Count == 0) ResetCycle();
    }

    // limpa tudo e reinicia com 1 zumbi atrás do player
    public void ResetCycle()
    {
        // limpa qualquer zumbi sobrando (safety)
        for (int i = zombies.Count - 1; i >= 0; i--)
        {
            if (zombies[i] != null) Destroy(zombies[i]);
        }
        zombies.Clear();

        // resetar parâmetros
        currentZombieSpeed = baseZombieSpeed;
        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval); // opcional
        accelerating = false;

        // spawn inicial de 1 zumbi para recomeçar perseguição
        SpawnOne();
    }

    // se quiser expor o número atual
    public int GetCurrentCount() => zombies.Count;
}