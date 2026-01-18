using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    public float blinkSpeed = 1f;
    private TextMeshProUGUI text;
    private float timer;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        float alpha = Mathf.Abs(Mathf.Sin(timer * blinkSpeed));
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}