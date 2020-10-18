using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    public Vector3 minLocalTransformation;
    public Vector3 maxLocalTransformation;
    public float lerpSpeed;
    public Vector3 targetPos;
    public Vector2Int minMaxTime = new Vector2Int(3, 7);
    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        // Set the origin to the starting position
        origin = transform.localPosition;
        targetPos = transform.localPosition;

        StartCoroutine(ChangeTargetPosition());
    }

    private void Update()
    {
        if (transform.localPosition != targetPos)
        {
            float speed = lerpSpeed / 10;

            // Gradually move the transform to the target position
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * speed);
        }
    }

    IEnumerator ChangeTargetPosition()
    {
        // Randomize an amount of time to wait before changing the position
        yield return new WaitForSeconds(.1f);

        if (targetPos.y > origin.y)
        {
            targetPos = minLocalTransformation;
        }

        else
        {
            targetPos = maxLocalTransformation;
        }


        //float x = Random.Range(minLocalTransformation.x, maxLocalTransformation.x);
        //float y = Random.Range(minLocalTransformation.y, maxLocalTransformation.y);
        //float z = Random.Range(minLocalTransformation.z, maxLocalTransformation.z);

        ////Update the target position, by offsetting the origin point.
        //targetPos = origin + new Vector3(x, y, z);

        //Loop
        StartCoroutine(ChangeTargetPosition());
    }
}
