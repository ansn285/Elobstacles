using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAndShoulders : MonoBehaviour
{
    // LH IK Target: -0.092 1.234 .3596 ; rotation: 85.215 40.779 0
    // LH IK Target: -.485 1.234 .3596 ; rotation: 85.215 -32.815 0
    // LH IK Target: -.248 1.032 .3596 ; rotation: 85.215 18.724 0

    // Head Target: 0.419 -.522 1.171
    // Head Target: -.426 -.304 1.171
    // Head Target: 0.153 -.469 1.171

    public Transform headAim, leftArmAim;

    private List<Vector3> leftArmTransforms = new List<Vector3>();
    private List<Vector3> leftArmRotations = new List<Vector3>();
    private List<Vector3> headAimTransforms = new List<Vector3>();

    private Vector3 lArmTargetPos, headTargetPos, lArmTargenRot;
    private int randomIndex;
    private float speed = .2f;

    private void Start()
    {
        leftArmTransforms.Add(new Vector3(-.092f, 1.234f, .3596f));
        leftArmTransforms.Add(new Vector3(-.485f, 1.234f, .3596f));
        leftArmTransforms.Add(new Vector3(-.248f, 1.032f, .3596f));

        leftArmRotations.Add(new Vector3(85.215f, 40.779f, 0));
        leftArmRotations.Add(new Vector3(85.215f, -32.815f, 0));
        leftArmRotations.Add(new Vector3(85.215f, 18.724f, 0));

        headAimTransforms.Add(new Vector3(.419f, -.522f, 1.171f));
        headAimTransforms.Add(new Vector3(-.426f, -.304f, 1.171f));
        headAimTransforms.Add(new Vector3(.153f, -.469f, 1.171f));

        InvokeRepeating("IntGenerator", .6f, 12f);
    }

    private void Update()
    {
        headAim.localPosition = Vector3.Lerp(headAim.localPosition, headTargetPos, Time.deltaTime * speed);
        leftArmAim.localPosition = Vector3.Lerp(leftArmAim.localPosition, lArmTargetPos, Time.deltaTime * speed);
        leftArmAim.localRotation = Quaternion.Slerp(leftArmAim.localRotation, Quaternion.Euler(lArmTargenRot), Time.deltaTime * speed);
    }

    protected void IntGenerator()
    {
        randomIndex = Random.Range(0, 3);
        StartCoroutine(MoveLimbs());
    }

    IEnumerator MoveLimbs()
    {
        yield return null;

        headTargetPos = headAimTransforms[randomIndex];
        lArmTargenRot = leftArmRotations[randomIndex];
        lArmTargetPos = leftArmTransforms[randomIndex];

        StartCoroutine(MoveLimbs());
    }
}
