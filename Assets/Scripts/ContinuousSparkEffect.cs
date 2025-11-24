using UnityEngine;

public class ContinuousSparkEffect : MonoBehaviour
{
    public ParticleSystem sparkEffect; // assign a spark particle prefab in inspector
    public float interval = 0.5f; // time between sparks

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            PlaySparks();
            timer = 0f;
        }
    }

    void PlaySparks()
    {
        if (sparkEffect != null)
        {
            sparkEffect.Play();
        }
        else
        {
            Debug.LogWarning("⚡ Spark particle system not assigned!");
        }
    }
}
