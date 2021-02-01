using UnityEngine;
using System.Collections;

public class SpriteGhostTrailRenderer : MonoBehaviour
{
    [SerializeField] ObjectPool pool;
    [SerializeField] Color color;
    [SerializeField] bool isOn;
    [SerializeField] bool singleColorShader;
    [SerializeField] [Range(0.1f, 0.5f)] float updateInterval;
    [SerializeField] [Range(1, 10)] int maxGhosts;
    [SerializeField] SpriteRenderer spriteRenderer;

    float timer = 0f;

    void Update()
    {
        if (isOn)
        {
            TickSpawnTimer();
        }
    }

    #region Public
    public void StartSpawning ()
    {
        isOn = true;

    }

    public void StopSpawning ()
    {
        isOn = false;
    }
    #endregion

    void TickSpawnTimer ()
    {
        if (timer > 0f)
            timer -= Time.deltaTime;
        else 
        {
            SpawnGhost();
            timer = updateInterval;
        }
    }

    void SpawnGhost()
    {
        pool.Spawn(transform.position);
    }
}