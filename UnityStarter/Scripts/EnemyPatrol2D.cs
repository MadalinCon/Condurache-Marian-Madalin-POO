using UnityEngine;

public class EnemyPatrol2D : MonoBehaviour
{
    [SerializeField] private Transform waypointA;
    [SerializeField] private Transform waypointB;
    [SerializeField] private float moveSpeedUnitsPerSecond = 2.5f;
    [SerializeField] private float arriveThreshold = 0.05f;
    [SerializeField] private bool faceMovementDirection = true;

    private Transform currentTarget;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        currentTarget = waypointA != null ? waypointA : transform;
    }

    private void Update()
    {
        if (waypointA == null || waypointB == null)
        {
            return;
        }

        Vector3 direction = (currentTarget.position - transform.position);
        float distance = direction.magnitude;

        if (distance <= arriveThreshold)
        {
            currentTarget = currentTarget == waypointA ? waypointB : waypointA;
            direction = (currentTarget.position - transform.position);
        }

        Vector3 step = direction.normalized * moveSpeedUnitsPerSecond * Time.deltaTime;
        transform.position += step;

        if (faceMovementDirection && spriteRenderer != null)
        {
            if (step.x > 0.01f)
            {
                spriteRenderer.flipX = false;
            }
            else if (step.x < -0.01f)
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (waypointA != null && waypointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(waypointA.position, waypointB.position);
        }
    }
}