using UnityEngine;
using CreatorKitCode;

public class SpawnerSample : MonoBehaviour
{
    public GameObject ObjectToSpawn;

    void Start()
    {
        LootAngle myLootAngle = new LootAngle(45);

        SpawnHealthPotion(myLootAngle.NextAngle());
        SpawnHealthPotion(myLootAngle.NextAngle());
        SpawnHealthPotion(myLootAngle.NextAngle());
        SpawnHealthPotion(myLootAngle.NextAngle());
    }

      void SpawnHealthPotion (int angle)
    {
        int radius = 5;

        Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.right;
        Vector3 spawnPosition = transform.position + direction * radius;
        Instantiate(ObjectToSpawn, spawnPosition, Quaternion.identity);
    }
}

public class LootAngle 
{
    int angle; 
    int step; 

    public LootAngle(int increment) 
    {
        step = increment; 
        angle = 0; 
    }

    public int NextAngle()
    {
        int currentAnngle = angle; 
        angle = (angle + step) % 360; 

        return currentAnngle; 
    }
}

