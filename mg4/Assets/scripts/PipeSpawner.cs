using UnityEngine;
using System.Collections;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnX = 10f;
    public float spawnInterval = 1.5f;

    public float minGapHeight = 2f;
    public float maxGapHeight = 3.8f;
    public float gapYOffset = 3f;

    void Start()
    {
        StartCoroutine(SpawnPipesCoroutine());
    }

    IEnumerator SpawnPipesCoroutine()
    {
        while (true)
        {
            GameObject newPipe = Instantiate(pipePrefab, new Vector3(spawnX, 1, 0), Quaternion.identity);
            Transform pipeTop = newPipe.transform.Find("PipeTop");
            Transform pipeBottom = newPipe.transform.Find("PipeBottom");

            float randomGapHeight = Random.Range(minGapHeight, maxGapHeight);
            float randomY = Random.Range(-gapYOffset, gapYOffset);

            float topScaleY = 10 - (randomY + randomGapHeight / 2);
            pipeTop.localScale = new Vector3(pipeTop.localScale.x, topScaleY, pipeTop.localScale.z);

            float bottomScaleY = 10 + (randomY - randomGapHeight / 2);
            pipeBottom.localScale = new Vector3(pipeBottom.localScale.x, bottomScaleY, pipeBottom.localScale.z);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}