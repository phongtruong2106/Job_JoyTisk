using UnityEngine;
using UnityEngine.InputSystem;

public class P_Controller : NewMonoBehaviour
{
    [SerializeField] private Player player;
    private Vector2 moveInput;
    [Header("Run")]
    private bool isRunning;
    private bool isRightMouseHeld;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }
    private void Update()
    {
        UpdateAnim();
        if (Mouse.current != null)
        {
            isRightMouseHeld = Mouse.current.rightButton.isPressed;
        }
    }
    private void LoadPlayer()
    {
        if (this.player != null) return;
        this.player = GetComponent<Player>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        player.Movement.SetInput(moveInput);
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRunning = true;
            player.Movement.SetRunning(true);
        }
        else if (context.canceled)
        {
            isRunning = false;
            player.Movement.SetRunning(false);
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        // Vector2 delta = context.ReadValue<Vector2>();

        // bool isMobile = Touchscreen.current != null;
        // if (isMobile)
        // {
        //     if (delta.sqrMagnitude > 0.001f)
        //         player.CamFollow.SetLookInput(delta, true);
        //     else
        //         player.CamFollow.SetLookInput(Vector2.zero, false);

        //     return;
        // }
        // if (isRightMouseHeld && delta.sqrMagnitude > 0.001f)
        // {
        //     player.CamFollow.SetLookInput(delta, true);
        // }
        // else
        // {
        //     player.CamFollow.SetLookInput(Vector2.zero, false);
        // }
    }
    private void UpdateAnim()
    {
        float speed = moveInput.magnitude;

        float finalSpeed = isRunning ? speed : speed * 0.5f;

        player.Anim.SetFloat("MoveX", moveInput.x, 0.1f, Time.deltaTime);
        player.Anim.SetFloat("MoveY", moveInput.y, 0.1f, Time.deltaTime);
        player.Anim.SetFloat("Speed", finalSpeed, 0.1f, Time.deltaTime);
    }
}