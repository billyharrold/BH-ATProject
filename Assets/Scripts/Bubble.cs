using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bubble : MonoBehaviour
{
    // this is gonna be the definitive bubble class - movement and spawning Lerps.
    // trying a differnt way instead of Rigidbody cuz it isnt working how I want.


    [Header("Movement")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float driftStrength = 1f;

    [Header("Spawn Animation")] 
    [SerializeField] private AnimationCurve spawnCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
    [SerializeField] private float spawnMinTime = 0.3f;
    [SerializeField] private float spawnMaxTime = 0.7f;

    private float offset;
    private Vector3 prefabScale;
    private bool isActive;
    private float radius;

    [SerializeField] private float repulseForce = 0.5f;

    public static List<Bubble> activeBubbles = new List<Bubble>();

    private void Awake()
    {
        offset = Random.Range(0f, 100f);
        prefabScale = transform.localScale;
        radius = prefabScale.x / 2f;
        transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        activeBubbles.Add(this);
    }

    private void OnDisable()
    {
        activeBubbles.Remove(this);
    }


    private void Start()
    {
        float spawnTime = Random.Range(spawnMinTime, spawnMaxTime);
        StartCoroutine(SpawnBubble(spawnTime));
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        MoveBubble();
        BubbleCollision();
    }

    private IEnumerator SpawnBubble(float duration)
    {
        yield return inflateBubble(duration);
        isActive = true;
    }

    private IEnumerator inflateBubble(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float time = Mathf.Clamp01(elapsedTime / duration);
            float scale = spawnCurve.Evaluate(time);
            transform.localScale = prefabScale * scale;
            yield return null;
        }
    }

    private void MoveBubble()
    {
        float noiseX = Mathf.PerlinNoise(offset + Time.time * driftStrength, 0f);
        float noiseY = Mathf.PerlinNoise(0f, offset + Time.time * driftStrength);

        Vector2 direction = new Vector2(noiseX * 2f - 1f, noiseY * 2f - 1f);

        transform.Translate(speed * Time.deltaTime * direction);
    }

    private void BubbleCollision()
    {
        foreach (Bubble other in activeBubbles)
        {
            if (other == this)
            {
                continue;
            }

            Vector2 differenceDistance = transform.position - other.transform.position;
            float distance = differenceDistance.magnitude;

            float total = radius + other.radius;

            if (distance < total && distance > 0.1f)
            {
                float overlap = total - distance;
                transform.Translate(differenceDistance.normalized * overlap * repulseForce * Time.deltaTime);
            }

        }
    }
}
