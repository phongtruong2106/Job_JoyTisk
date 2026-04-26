using UnityEngine;

public class CameraRotate : NewMonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private float followSpeed = 5f;

    private float yaw;

    protected override void Start()
    {
        yaw = transform.eulerAngles.y;
    }

    public void AddInput(float deltaX)
    {
        yaw += deltaX * sensitivity;
    }

    private void LateUpdate()
    {
        Vector3 moveDir = player.forward;

        if (moveDir.sqrMagnitude > 0.001f)
        {
            float targetYaw = Quaternion.LookRotation(moveDir).eulerAngles.y;

            yaw = Mathf.LerpAngle(
                yaw,
                targetYaw,
                Time.deltaTime * followSpeed
            );
        }

        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}