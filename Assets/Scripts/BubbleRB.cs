using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleRB : MonoBehaviour
{
    private Rigidbody2D rb;

    public float driftStrength = 0.5f;
    public float riseSpeed = 0.6f;

    private float randTime;

    private Vector2 offset;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        offset.x = Random.value * 100f;
        offset.y = Random.value * 100f;

        randTime = Random.value * 100f;

    }
    private void FixedUpdate()
    {
        float t = Time.time + randTime;

        float x = (Mathf.PerlinNoise(offset.x + t, 0f) - 0.5f);
        float y = (Mathf.PerlinNoise(0f, offset.y + t) - 0.5f);

        Vector2 moveForce = new Vector2(x * driftStrength, riseSpeed + y * driftStrength);

        rb.AddForce(moveForce, ForceMode2D.Force);
    }


    private void OnCollisionStay2D(Collision2D collision2D)
    {
        if (collision2D.collider.CompareTag("Bubble"))
        {

            Vector2 direction = (Vector2)(transform.position - collision2D.transform.position);

            float distance = direction.magnitude;

            if (distance > 0.01f)
            {
                rb.AddForce(direction.normalized * 0.2f, ForceMode2D.Force);
            }
        }


    }
}
