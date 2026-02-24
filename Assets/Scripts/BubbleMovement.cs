using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public float speed = 1f;
    public float noiseScale = 1f;
    public float moveStrength = 1f;

    private Vector2 noiseOffset;
    void Start()
    {
        noiseOffset = Random.insideUnitCircle * 100f;
    }

    // Update is called once per frame
    void Update()
    {
        float noiseX = Mathf.PerlinNoise(noiseOffset.x, Time.time * noiseScale) - 0.5f;
        float noiseY = Mathf.PerlinNoise(noiseOffset.y, Time.time * noiseScale) - 0.5f;

        Vector2 moveVector = new Vector2(noiseX, noiseY) * moveStrength;
        transform.Translate(speed * Time.deltaTime * moveVector);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
