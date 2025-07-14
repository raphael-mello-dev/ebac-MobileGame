using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool CanMove { get; set; }
    public bool IsInvincible { get; set; }
    public float SpeedIncrease { get; set; }

    [SerializeField] private Animator animator;

    private Vector2 mouseLastPos;

    [SerializeField] private float speed;
    [SerializeField] private float velocity;

    [SerializeField] private ParticleSystem deathParticle;
    public ParticleSystem DeathParticle { get; private set; }

    private void Awake()
    {
        DeathParticle = deathParticle;
    }

    void Start()
    {
        mouseLastPos = Input.mousePosition;
    }

    private void OnEnable()
    {
        UIStartEnd.OnAnimChanged += ChangeAnim;
        Obstacle.OnAnimChanged += ChangeAnim;
    }

    private void OnDisable()
    {
        UIStartEnd.OnAnimChanged -= ChangeAnim;
        Obstacle.OnAnimChanged -= ChangeAnim;
    }

    void Update()
    {
        if (!CanMove) return;

        GetMovementInput();
        
        transform.Translate(Vector3.forward * (speed + SpeedIncrease) * Time.deltaTime);
    }

    void GetMovementInput()
    {
        if (Input.GetMouseButton(0))
            Movement(Input.mousePosition.x - mouseLastPos.x);
        
        mouseLastPos = Input.mousePosition;
    }

    void Movement(float axis) => transform.Translate(Vector3.right * axis * velocity * Time.deltaTime);

    void ChangeAnim(int anim) => animator.SetInteger("Transition", anim);
}