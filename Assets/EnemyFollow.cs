using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float baseSpeed = 2f;
    public float speedIncreasePerPoint = 0.1f;

    private Transform player;
    private float currentSpeed;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
        else
            Debug.LogError("PLAYER NON TROVATO");
    }

    void Update()
    {
        if (player == null) return;

        // Calcolo velocità in base al punteggio
        currentSpeed = baseSpeed + (GameManager.instance.score * speedIncreasePerPoint);

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * currentSpeed * Time.deltaTime);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
        }
    }
}