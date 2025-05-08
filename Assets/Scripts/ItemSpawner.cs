using UnityEngine;
using System.Collections.Generic;

// Create an empty GameObject in the Game Scene and attach this script
public class ItemSpawner: MonoBehaviour
{
    public GameObject safeSnackPrefab;
    public GameObject dangerousSnackPrefab;
    public GameObject specialSnackPrefab;
    public GameObject splatPrefab;
    public GameObject birdPrefab;
    public float spawnRate = 1f;
    public float spawnRangeX = 5f; // Adjust the horizontal spawn range

    private float nextSpawnTime = 0f;
    private List<GameObject> itemPrefabs = new List<GameObject>();

    // TODO: randomize spawn times
    // TODO: lists for each type of item
    // TODO: handle bird spawning in a seperate class

    void Start()
    {
        // Add item prefabs to the list
        itemPrefabs.Add(safeSnackPrefab);
        itemPrefabs.Add(dangerousSnackPrefab);
        itemPrefabs.Add(specialSnackPrefab);
        itemPrefabs.Add(splatPrefab);
        itemPrefabs.Add(birdPrefab);
    }

    void Update()
    {
        if(Time.time >= nextSpawnTime)
        {
            SpawnRandomItem();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnRandomItem()
    {
        if(itemPrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Count);
            GameObject itemToSpawn = itemPrefabs[randomIndex];
            if(itemToSpawn != null)
            {
                float randomX = Random.Range(-spawnRangeX, spawnRangeX);
                Vector3 spawnPosition = new Vector3(randomX, 10f, 0f); // Adjust the Y spawn position
                Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            Debug.LogWarning("No item prefabs assigned to the ItemSpawner!");
        }
    }
}