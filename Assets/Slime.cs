using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float detectionRadius = 5.0f; // Zasiêg wykrywania gracza
    [SerializeField] private float escapeSpeedMultiplier = 2.0f; // Mno¿nik prêdkoœci ucieczki
    [SerializeField] private float wanderRadius = 20.0f; // Promieñ obszaru wêdrówki
    [SerializeField] private float waitTimeAtPoint = 4.0f; // Czas oczekiwania w punkcie
    [SerializeField] private float damageEffectDuration = 5f; // Jak d³ugo spowolnienie dzia³a

    [Header("References")]
    [SerializeField] private LayerMask groundLayer; // Warstwa ziemi
    [SerializeField] private LayerMask obstacleMask; // Maskowanie przeszkód (opcjonalne)

    private NavMeshAgent navAgent;
    private Transform player;
    private float waitTimer = 0f;
    private bool isGrounded = false;

    private enum State
    {
        WaitingToStart,
        Wandering,
        Escaping
    }

    private State currentState = State.WaitingToStart;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (navAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
        }

        if (player == null)
        {
            Debug.LogError("Player object with tag 'Player' is missing!");
        }

        navAgent.enabled = false; // Wy³¹cz NavMeshAgent na starcie
    }

    private void Update()
    {
        CheckIfGrounded();

        // Only proceed if the agent is enabled and grounded
        if (currentState == State.WaitingToStart && isGrounded)
        {
            StartAgent();
        }

        // Ensure the NavMeshAgent is enabled before accessing its properties
        if (navAgent.enabled)
        {
            switch (currentState)
            {
                case State.Wandering:
                    Wander();
                    break;
                case State.Escaping:
                    EscapeFromPlayer();
                    break;
            }

            CheckPlayerDistance();
        }
    }


    private void CheckIfGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        if (isGrounded && !navAgent.enabled)
        {
            Debug.Log("Slime is grounded. NavMeshAgent will now start.");
        }
    }

    private void StartAgent()
    {
        navAgent.enabled = true;
        currentState = State.Wandering;
    }

    private void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            currentState = State.Escaping;
        }
        else if (currentState == State.Escaping && distanceToPlayer >= detectionRadius * 1.5f)
        {
            currentState = State.Wandering;
            navAgent.speed /= escapeSpeedMultiplier; // Przywrócenie normalnej prêdkoœci
        }
    }

    private void Wander()
    {
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTimeAtPoint)
            {
                Vector3 newDestination = GetRandomPoint();
                navAgent.SetDestination(newDestination);
                waitTimer = 0f;
            }
        }
    }

    private void EscapeFromPlayer()
    {
        Vector3 directionAwayFromPlayer = transform.position - player.position;
        Vector3 escapeTarget = transform.position + directionAwayFromPlayer.normalized * detectionRadius;

        if (NavMesh.SamplePosition(escapeTarget, out NavMeshHit hit, detectionRadius, NavMesh.AllAreas))
        {
            navAgent.speed *= escapeSpeedMultiplier; // Przyspieszenie ucieczki
            navAgent.SetDestination(hit.position);
        }
    }

    private Vector3 GetRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            Debug.LogWarning("Brak pozycji na NavMesh");
            return transform.position; // Jeœli nie znaleziono pozycji, wróæ do pocz¹tkowej pozycji
        }
    }

/*    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
 
    }*/

    private void OnParticleCollision(GameObject other)
    {
        // Sprawdzenie, czy cz¹steczki pochodz¹ z odpowiedniego systemu
        if (other.CompareTag("ParticleSystem"))
        {
            Debug.Log("Slime hit by particle!");
            ReduceSpeed(damageEffectDuration);
        }
    }

    public void ReduceSpeed(float duration)
    {
        
        if (navAgent != null)
        {
            Debug.Log("prêdkoœæ przed: " + navAgent.speed);
            navAgent.speed /= 2; // Zmniejsz prêdkoœæ o po³owê
            Debug.Log("prêdkoœæ po: " + navAgent.speed);
            StartCoroutine(RestoreSpeedAfterDelay(duration));
        }
    }

    private IEnumerator RestoreSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (navAgent != null)
        {
            navAgent.speed *= 2; // Przywróæ prêdkoœæ po czasie
        }
    }
    }
