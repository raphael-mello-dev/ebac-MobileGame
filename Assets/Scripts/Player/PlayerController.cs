using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool CanMove { get; set; }
    public float SpeedIncrease { get; set; }

    private Vector2 mouseLastPos;

    [SerializeField] private float speed;
    [SerializeField] private float velocity;

    void Start()
    {
        mouseLastPos = Input.mousePosition;
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
}