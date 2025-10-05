using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public float yOffset = 18f;
    float currentOffset;
    public GameObject[] LevelObjects;
    public Transform[] SpawnPoints;
    //public int levelAmount = 3;
    bool hasLevelLoaded = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasLevelLoaded)
        {
            hasLevelLoaded = true;
            LoadLevel();
        }
    }

    void LoadLevel()
    {
        currentOffset += yOffset;
        foreach (Transform SpawnPoint in SpawnPoints) SpawnPoint.position += Vector3.up * yOffset;

        Instantiate(LevelObjects[Random.Range(0, LevelObjects.Length)], Vector3.up * currentOffset, Quaternion.identity, GameObject.FindGameObjectWithTag("Grid").transform);
        transform.position += Vector3.up * yOffset;
        hasLevelLoaded = false;
    }


}
