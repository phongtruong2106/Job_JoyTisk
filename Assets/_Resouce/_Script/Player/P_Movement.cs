using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class P_Movement : NewMonoBehaviour
{

    [SerializeField] private Player player;
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;

    private bool isRunning;
    [SerializeField] private Transform cam;
    private Vector2 input;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }
    protected override void Awake()
    {
        base.Awake();
        this.LoadPlayer();
    }
    private void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = gameObject.GetComponentInParent<Player>();
    }
    private void Update()
    {
        Move();
        Rotate();
    }
    public void SetRunning(bool value)
    {
        isRunning = value;
    }
    public void SetInput(Vector2 moveInput)
    {
        input = Vector2.ClampMagnitude(moveInput, 1f);
    }
    private void Move()
    {
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;

        Vector3 move = forward * input.y + right * input.x;

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        player.Controller.Move(move * currentSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;

        Vector3 move = forward * input.y + right * input.x;

        if (move.sqrMagnitude < 0.001f) return;

        move.Normalize();

        Quaternion targetRot = Quaternion.LookRotation(move);

        transform.parent.rotation = Quaternion.Slerp(
            transform.parent.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }
    public Vector2 GetInput()
    {
        return input;
    }

}