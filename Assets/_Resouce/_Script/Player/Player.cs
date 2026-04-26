using UnityEngine;

public class Player : NewMonoBehaviour
{
    [SerializeField] private P_Movement movement;
    public P_Movement Movement => movement;
    [SerializeField] private CharacterController controller;
    public CharacterController Controller => controller;
    [SerializeField] private Animator anim;
    public Animator Anim => anim;
    [SerializeField] private CameraFollow camFollow;
    public CameraFollow CamFollow => camFollow;
    [SerializeField] private P_Controller p_Controller;
    public P_Controller ControllerInput => p_Controller;
    [SerializeField] private CameraRotate cameraRotate;
    public CameraRotate _cameraRotate => cameraRotate;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMovement();
        this.LoadController();
        this.LoadAnim();
        this.LoadCameraFollow();
        this.LoadP_Controller();
        this.LoadC_R();
    }
    private void LoadC_R()
    {
        if (this.cameraRotate != null) return;
        this.cameraRotate = FindAnyObjectByType<CameraRotate>();
    }
    private void LoadP_Controller()
    {
        if (this.p_Controller != null) return;
        this.p_Controller = gameObject.GetComponent<P_Controller>();
    }
    private void LoadCameraFollow()
    {
        if (this.camFollow != null) return;
        this.camFollow = FindAnyObjectByType<CameraFollow>();
    }
    private void LoadMovement()
    {
        if (this.movement != null) return;
        this.movement = gameObject.GetComponentInChildren<P_Movement>();
    }
    private void LoadController()
    {
        if (this.controller != null) return;
        this.controller = gameObject.GetComponent<CharacterController>();
    }
    private void LoadAnim()
    {
        if (this.anim != null) return;
        this.anim = gameObject.GetComponentInChildren<Animator>();
    }
}