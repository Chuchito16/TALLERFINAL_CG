using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Prefabs de los ítems a generar")]
    public GameObject[] itemPrefabs;  

    [Header("Configuración del spawn")]
    public int itemCount = 20;        
    public Vector3 areaSize = new Vector3(10, 1, 10); 
    public Transform spawnCenter;     

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        if (spawnCenter == null)
            spawnCenter = transform; 

        for (int i = 0; i < itemCount; i++)
        {
          
            Vector3 randomPos = spawnCenter.position + new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                Random.Range(-areaSize.y / 2, areaSize.y / 2),
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );

         
            GameObject prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

          
            Instantiate(prefab, randomPos, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnCenter != null ? spawnCenter.position : transform.position, areaSize);
    }
}
