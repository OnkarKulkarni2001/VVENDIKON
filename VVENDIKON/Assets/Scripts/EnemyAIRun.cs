using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Fleeing,
    Passive,
    Scared
}


public class EnemyAIRun : MonoBehaviour
{
    [Header("AI Settings")]
    public EnemyType enemyType;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    [Header("Behavior Toggles")]
    public bool canPatrol = true;
    public bool canFlee = true;
    public float safeDistance = 15f;

    [Header("Movement Settings")]
    public float patrolSpeed = 2f;
    public float fleeSpeed = 6f;
    public float walkPointRange = 10f;
    public float fleePointRange = 20f;

    [Header("Detection Settings")]
    public float sightRange = 10f;

    [Header("Health")]
    public float health = 100f;

    [Header("Sound Settings")]
    public AudioClip detectionSound;
    public AudioClip fleeSound;
    public AudioClip patrolSound;
    public AudioClip footstepSound;
    [Range(0, 1)] public float soundVolume = 0.7f;

    public float minSoundDelay = 3f;
    public float maxSoundDelay = 8f;

    private AudioSource audioSource;
    private float nextSoundTime;
    private bool wasPlayerInSightLastFrame;
    private float nextFootstepTime;

    [Header("3D Sound Settings")]
    [Tooltip("Minimum distance where sound is at full volume")]
    public float soundMinDistance = 1f;

    [Tooltip("Maximum distance where sound can be heard")]
    public float soundMaxDistance = 15f;

    [Range(0f, 1f)] public float spatialBlend = 0.8f; // 0=2D, 1=3D
    public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;

    // Private state variables
    private Vector3 walkPoint;
    private bool walkPointSet;
    private bool playerInSightRange;
    private int footstepInterval;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        InitializeEnemyType();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = spatialBlend;
        audioSource.rolloffMode = rolloffMode;
        audioSource.minDistance = soundMinDistance;
        audioSource.maxDistance = soundMaxDistance;
        audioSource.dopplerLevel = 0.1f; // Reduce Doppler effect
        audioSource.spread = 45f; // Narrower sound cone

        nextSoundTime = Time.time + Random.Range(minSoundDelay, maxSoundDelay);
    }

    private void InitializeEnemyType()
    {
        switch (enemyType)
        {
            case EnemyType.Fleeing:
                canPatrol = true;
                canFlee = true;
                break;
            case EnemyType.Passive:
                canPatrol = true;
                canFlee = false;
                break;
            case EnemyType.Scared:
                canPatrol = false;
                canFlee = true;
                break;
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Check detection range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        HandleSounds();

        if (playerInSightRange && canFlee)
        {
            FleeFromPlayer();
        }
        else if (canPatrol)
        {
            Patroling();
        }
        else
        {
            // Idle behavior
            agent.ResetPath();
        }
    }

    private void HandleSounds()
    {
        // Play detection sound once when player is spotted
        if (playerInSightRange && !wasPlayerInSightLastFrame)
        {
            PlaySound(detectionSound);
        }

        // Periodic sounds
        if (Time.time >= nextSoundTime)
        {
            if (playerInSightRange && canFlee)
            {
                PlaySound(fleeSound);
            }
            else if (canPatrol)
            {
                PlaySound(patrolSound);
            }

            nextSoundTime = Time.time + Random.Range(minSoundDelay, maxSoundDelay);
        }

        wasPlayerInSightLastFrame = playerInSightRange;
    }

    private void PlayFootsteps()
    {
        if (Time.time >= nextFootstepTime)
        {
            PlaySound(footstepSound);
            nextFootstepTime = Time.time + footstepInterval;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            if (TrySearchWalkPoint())
            {
                walkPointSet = true;
            }
            else
            {
                return;
            }
        }

        agent.speed = patrolSpeed;

        if (agent.isOnNavMesh && agent.isActiveAndEnabled)
        {
            agent.SetDestination(walkPoint);
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            walkPointSet = false;
        }
    }

    private bool TrySearchWalkPoint()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * walkPointRange;
            randomDirection += transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkPointRange, NavMesh.AllAreas))
            {
                walkPoint = hit.position;
                return true;
            }
        }
        return false;
    }

    private void FleeFromPlayer()
    {
        agent.speed = fleeSpeed;
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 fleePosition = transform.position + fleeDirection * safeDistance;

        // Find valid flee position on NavMesh
        if (NavMesh.SamplePosition(fleePosition, out NavMeshHit hit, fleePointRange, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}