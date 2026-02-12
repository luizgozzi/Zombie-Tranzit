using UnityEngine;

public class GroundSpawner : MonoBehaviour 
{
    [SerializeField] GameObject groundTile;
    Vector3 nextSpawPoint;

    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            if (i < 3)
            {
                SpawnTile(false);
            }
            else
            {
                SpawnTile(true);
            }
        }
    }

    public void SpawnTile(bool spawnItems)
    {
        GameObject temp = Instantiate(groundTile, nextSpawPoint, Quaternion.identity);
        nextSpawPoint = temp.transform.GetChild(1).transform.position;//

        if (spawnItems)
        {
            temp.GetComponent<GroundTile>().SpawnObstacle();
            temp.GetComponent<GroundTile>().SpawnCoinLine();
            temp.GetComponent<GroundTile>().SpawnFuel();
            temp.GetComponent<GroundTile>().SpawnNitro();
        }
    }
}