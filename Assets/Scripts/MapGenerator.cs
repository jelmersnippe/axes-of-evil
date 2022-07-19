using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static event Action OnEntitiesSpawned;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [Range(0f, 1f)]
    [SerializeField] private float wallChance;
    private Tile[,] map;
    [SerializeField] private GameObject floorTile;
    [SerializeField] private GameObject wallTile;
    [SerializeField] private PlayerBrain player;
    [SerializeField] private WanderBrain wanderer;

    [SerializeField] private Transform tileContainer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateMap();
            DisplayMap();
            SpawnObject(player.gameObject);
            SpawnObject(wanderer.gameObject);
            OnEntitiesSpawned?.Invoke();
        }
    }

    private void GenerateMap()
    {
        map = new Tile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = new Tile(UnityEngine.Random.Range(0f, 1f) >= wallChance);
            }
        }
        MovementSystem.map = map;
    }

    private void DisplayMap()
    {
        if (tileContainer != null)
        {
            Destroy(tileContainer.gameObject);
        }

        tileContainer = new GameObject().transform;
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Instantiate(map[x, y].walkable ? floorTile : wallTile, new Vector3(x, y), Quaternion.identity, tileContainer);
            }
        }
    }

    private void SpawnObject(GameObject gameObject)
    {
        int randomX = UnityEngine.Random.Range(0, width);
        int randomY = UnityEngine.Random.Range(0, height);

        if (map[randomX, randomY].walkable)
        {
            Instantiate(gameObject, new Vector3(randomX, randomY), Quaternion.identity);
        }
        else
        {
            SpawnObject(gameObject);
        }
    }
}
