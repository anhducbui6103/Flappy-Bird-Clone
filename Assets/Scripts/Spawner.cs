using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 1.0f;
    public float minHeight = -1.1f;
    public float maxHeight = 2.2f;

    public float minGap = 8.6f;
    public float maxGap = 10.0f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    void Spawn()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = Random.Range(minHeight, maxHeight);

        GameObject pipes = Instantiate(prefab, newPosition, Quaternion.identity);

        Transform topPipe = pipes.transform.Find("Top");
        Transform bottomPipe = pipes.transform.Find("Bot");

        if (topPipe != null && bottomPipe != null)
        {
            float gap = Random.Range(minGap, maxGap);

            topPipe.position = new Vector3(topPipe.position.x, newPosition.y + gap / 2, topPipe.position.z);
            bottomPipe.position = new Vector3(bottomPipe.position.x, newPosition.y - gap / 2, bottomPipe.position.z);
        }
    }
}
