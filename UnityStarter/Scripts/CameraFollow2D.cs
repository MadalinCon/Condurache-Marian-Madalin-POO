using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 positionOffset = new Vector3(0f, 1.5f, -10f);
    [SerializeField] private float smoothTimeSeconds = 0.15f;
    [SerializeField] private float maxSpeedUnitsPerSecond = Mathf.Infinity;

    private Vector3 currentVelocity;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 desiredPosition = target.position + positionOffset;
        transform.position = Vector3.SmoothDamp(
            current: transform.position,
            target: desiredPosition,
            currentVelocity: ref currentVelocity,
            smoothTime: smoothTimeSeconds,
            maxSpeed: maxSpeedUnitsPerSecond,
            deltaTime: Time.unscaledDeltaTime
        );
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}