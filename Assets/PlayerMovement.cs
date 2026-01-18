using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    [Header("Engine Audio")]
    public float idlePitch = 0.8f;
    public float drivePitch = 1.3f;
    public float pitchSmooth = 5f;

    private Rigidbody2D rb;
    private AudioSource engineAudio;
    private Camera cam;

    private Vector2 movement;
    private Vector2 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        engineAudio = GetComponent<AudioSource>();
        cam = Camera.main;

        targetPosition = rb.position;

        engineAudio.loop = true;
        engineAudio.Play();
    }

    void Update()
    {
        HandleTouchInput();

        Vector2 direction = targetPosition - rb.position;

        if (direction.magnitude > 0.1f)
            movement = direction.normalized;
        else
            movement = Vector2.zero;

        // Rotazione macchina
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
        }

        // Audio motore
        float targetPitch = movement.magnitude > 0.1f ? drivePitch : idlePitch;
        engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, targetPitch, Time.deltaTime * pitchSmooth);
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    void HandleTouchInput()
    {
        if (Pointer.current == null)
            return;

        if (Pointer.current.press.isPressed)
        {
            Vector2 screenPos = Pointer.current.position.ReadValue();
            Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
            targetPosition = new Vector2(worldPos.x, worldPos.y);
        }
    }
}