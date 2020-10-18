using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject platform1, platform2;
    public static float platformSpeed = 5f;

    

    // Update is called once per frame
    void Update()
    {
        if (platform1.transform.position.z > -35.386f)
        {
            platform1.transform.position = Vector3.MoveTowards(platform1.transform.position, new Vector3(0, 0, -35.386f),
                                                      platformSpeed * Time.deltaTime);
        }
        else
        {
            platform1.transform.position = new Vector3(0, 0, platform2.transform.position.z + 35f);
            platform1.GetComponent<ObstacleSpawner>().isFirstPlatform = false;
            platform1.GetComponent<ObstacleSpawner>().Spawn();
        }

        if (platform2.transform.position.z > -35.386f)
        {
            platform2.transform.position = Vector3.MoveTowards(platform2.transform.position, new Vector3(0, 0, -35.386f),
                                                      platformSpeed * Time.deltaTime);
        }
        else
        {
            platform2.transform.position = new Vector3(0, 0, platform1.transform.position.z + 35.386f);
            platform2.GetComponent<ObstacleSpawner>().Spawn();
        }
    }

    public void Reset()
    {
        platformSpeed = 5;
    }
}
