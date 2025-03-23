using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public int numberOfEnemiesToSpawn = 5; // Number of enemies to spawn
    public float spawnRadius = 10f; // Radius within which enemies will spawn

    [Header("Debug")]
    public bool drawGizmos = true; // Draw spawn radius in the editor

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            // Calculate a random position within the spawn radius
            Vector3 randomPosition = GetRandomPositionWithinRadius();

            // Spawn the enemy at the random position
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionWithinRadius()
    {
        // Generate a random point inside a circle (X and Z axes)
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;

        // Convert the 2D circle to a 3D position (keep the Y position the same as the spawner)
        Vector3 randomPosition = new Vector3(randomCircle.x, transform.position.y, randomCircle.y) + transform.position;

        // Ensure the position is on the NavMesh (if using NavMeshAgent)
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, spawnRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        // Fallback to the original random position if NavMesh sampling fails
        return randomPosition;
    }

    private void OnDrawGizmosSelected()
    {
        if (drawGizmos)
        {
            // Draw a wireframe sphere to visualize the spawn radius
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}