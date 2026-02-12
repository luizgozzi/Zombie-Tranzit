using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;

    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            groundSpawner.SpawnTile(true);
            Destroy(gameObject, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField] GameObject obstaclePrefab;

    public void SpawnObstacle()
    {
        // Choose a random point to spawn the obstacle
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        // Spawn the obstacle at the position
        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
    }

    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject nitroPrefab;
    [SerializeField] GameObject fuelPrefab;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform centerPoint;
    [SerializeField] Transform rightPoint;
    [SerializeField] private float heightOffset = 0.1f;
    [SerializeField] private float checkRadius = 1.5f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private int maxTries = 5;

    public void SpawnCoinLine(int length = 5, float spacing = 1.2f)
    {
        // escolhe qual faixa usar como base — aqui faremos na faixa central ocupando todas as faixas
        for (int i = 0; i < length; i++)
        {
            // spawna 3 moedas por "coluna": left, center, right
            Vector3 basePos = transform.position + Vector3.forward * (i * spacing + 1f); // ajuste z conforme seu tile

            int randomLane = Random.Range(0, 3);

            if (randomLane == 0)
                Instantiate(coinPrefab, leftPoint.position + Vector3.forward * (i * spacing), coinPrefab.transform.rotation, transform);

            if (randomLane == 1)
                Instantiate(coinPrefab, centerPoint.position + Vector3.forward * (i * spacing), coinPrefab.transform.rotation, transform);

            if (randomLane == 2)
                Instantiate(coinPrefab, rightPoint.position + Vector3.forward * (i * spacing), coinPrefab.transform.rotation, transform);
        }
    }


    public void SpawnFuel()
    {
        for (int attempt = 0; attempt < maxTries; attempt++)
        {
            int randomLane = Random.Range(0, 3);
            Vector3 spawnPos = Vector3.zero;

            if (randomLane == 0) spawnPos = leftPoint.position;
            if (randomLane == 1) spawnPos = centerPoint.position;
            if (randomLane == 2) spawnPos = rightPoint.position;

            spawnPos += Vector3.up * heightOffset;

            // ✅ Checa se tem algo no local
            Collider[] hits = Physics.OverlapSphere(spawnPos, checkRadius, obstacleLayer);
            if (hits.Length == 0)
            {
                Instantiate(fuelPrefab, spawnPos, fuelPrefab.transform.rotation, transform);
                return; // deu certo, sai da função
            }
        }

    }

    public void SpawnNitro()
    {
        for (int attempt = 0; attempt < maxTries; attempt++)
        {
            int randomLane = Random.Range(0, 3);
            Vector3 spawnPos = Vector3.zero;

            if (randomLane == 0) spawnPos = leftPoint.position;
            if (randomLane == 1) spawnPos = centerPoint.position;
            if (randomLane == 2) spawnPos = rightPoint.position;

            spawnPos += Vector3.up * heightOffset;

            // ✅ Checa se tem algo no local
            Collider[] hits = Physics.OverlapSphere(spawnPos, checkRadius, obstacleLayer);
            if (hits.Length == 0)
            {
                Instantiate(nitroPrefab, spawnPos, nitroPrefab.transform.rotation, transform);
                return; // deu certo, sai da função
            }
        }
    }
}