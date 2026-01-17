using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    [Header("Engine Audio")]
    public float idlePitch = 0.8f;
    public float drivePitch = 1.3f;
    public float pitchSmooth = 5f;

    private Rigidbody2D rb;
    private AudioSource engineAudio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        engineAudio = GetComponent<AudioSource>();

        engineAudio.loop = true;
        engineAudio.Play(); // parte subito al minimo
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);
        rb.velocity = movement * speed;

        // Rotazione
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
        }

        // AUDIO MOTORE CON PITCH DINAMICO
        float targetPitch = movement.magnitude > 0.1f ? drivePitch : idlePitch;
        engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, targetPitch, Time.deltaTime * pitchSmooth);
    }
}