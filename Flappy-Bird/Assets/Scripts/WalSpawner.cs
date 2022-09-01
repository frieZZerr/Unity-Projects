using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalSpawner : MonoBehaviour
{
    public static GameObject pipe;
    public float spawnTime = 1f;

    float timer = 0f;
    float height = 0.5f;

    private void Update()
    {
        if( timer > spawnTime )
        {
            GameObject newWall = Instantiate(pipe);
            newWall.transform.position = transform.position + new Vector3( 0, Random.Range(-height, height ), 0);
            Destroy(newWall, 5);

            timer = 0f;
        }

        timer += Time.deltaTime;
    }
}
