using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    //public float zDistance; // Distance of each obstacle from each other
    private float zCurrent;
    public bool isFirstPlatform;
    private int elementCount;

    // Start is called before the first frame update
    void Start()
    {
        if (GameController.tutorial)
        {
            elementCount = obstacles.Length;
            SpawnTutorial();
        }

        else
        {
            Spawn();
        }
    }


    public void Spawn()
    {
        if (!GameController.tutorial)
        {
            if (isFirstPlatform)
            {
                return;
            }

            else
            {

                zCurrent = -.35f;
                for (int i = 0; i < Random.Range(2, 4); i++)
                {
                    if (zCurrent < .47f)
                    {

                        var ob = Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(0, 1.7108f, 0),
                                                       Quaternion.identity);
                        ob.transform.SetParent(gameObject.transform);
                        ob.transform.localPosition = new Vector3(ob.transform.localPosition.x, ob.transform.localPosition.y,
                                                                 zCurrent);
                        zCurrent += Random.Range(0.3f, 0.5f);
                    }

                    else break;
                }
            }
        }

        else
        {
            SpawnTutorial();
        }
    }
    

    protected void SpawnTutorial()
    {
        elementCount--;

        if (elementCount >= 0)
        {
            var ob = Instantiate(obstacles[elementCount], new Vector3(0, 1.7108f, 0), Quaternion.identity);
            ob.transform.SetParent(transform);
            ob.transform.localPosition = new Vector3(ob.transform.localPosition.x, ob.transform.localPosition.y, 0);
        }
    }

}
