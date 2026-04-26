using UnityEngine;

public class CameraFollow : NewMonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Offset")]
    [SerializeField] private Vector2 offsetXY = new Vector2(0, 5); // X, Y
    [SerializeField] private float zoom = 6f;

    [Header("Rotate")]
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private Player player;


    private float yaw;
    private float pitch = 20f;

    private void LateUpdate()
    {
        if (target == null) return;

        RotateCamera();
        FollowTarget();

    }

    private void RotateCamera()
    {
        if (target == null) return;

        float targetYaw = target.eulerAngles.y;

        // 🎯 chỉ follow khi KHÔNG đi lùi
        bool isMovingBackward = false;

        // bạn cần lấy input từ player (cách clean nhất)
        if (target.TryGetComponent<Player>(out var p))
        {
            isMovingBackward = p.Movement.GetInput().y < -0.1f;
        }

        if (!isMovingBackward)
        {
            yaw = Mathf.LerpAngle(yaw, targetYaw, rotateSpeed * Time.deltaTime);
        }

        pitch = Mathf.Clamp(pitch, 10f, 60f);
    }

    private void FollowTarget()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 height = Vector3.up * offsetXY.y;
        Vector3 back = rotation * Vector3.back * zoom;
        Quaternion yawOnly = Quaternion.Euler(0, yaw, 0);
        Vector3 side = yawOnly * Vector3.right * offsetXY.x;

        Vector3 desiredPosition = target.position + height + back + side;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSpeed * Time.deltaTime
        );

        transform.LookAt(target.position + Vector3.up * 1.5f);
    }

    public Vector3 GetForward()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    public Vector3 GetRight()
    {
        Vector3 right = transform.right;
        right.y = 0;
        return right.normalized;
    }
}
