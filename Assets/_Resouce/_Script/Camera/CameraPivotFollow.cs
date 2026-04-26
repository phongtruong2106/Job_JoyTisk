using UnityEngine;

public class CameraPivotFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Header("Follow")]
    [SerializeField] private float followSpeed = 5f;

    [Header("Lead Effect")]
    [SerializeField] private float yawOffset = 15f;
    [SerializeField] private float moveThreshold = 0.1f;

    private float currentYaw;

    private void Start()
    {
        currentYaw = player.eulerAngles.y;
    }

    private void Update()
    {
        float playerYaw = player.eulerAngles.y;
        float targetYaw = playerYaw;

        float delta = Mathf.DeltaAngle(currentYaw, playerYaw);

        if (Mathf.Abs(delta) > moveThreshold)
        {
            targetYaw = playerYaw + Mathf.Sign(delta) * yawOffset;
        }
        currentYaw = Mathf.LerpAngle(
            currentYaw,
            targetYaw,
            Time.deltaTime * followSpeed
        );

        transform.rotation = Quaternion.Euler(0, currentYaw, 0);
    }
}