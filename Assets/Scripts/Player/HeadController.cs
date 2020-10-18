using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
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
        yield return new WaitForSeconds(1.5f);
        
        if (targetPos.x > origin.x)
        {
            targetPos = minLocalTransformation;
        }

        else
        {
            targetPos = maxLocalTransformation;
        }

        StartCoroutine(ChangeTargetPosition());
    }
}
