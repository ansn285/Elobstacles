using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StaffController : MonoBehaviour
{
    // Position: .371f 1.415 .465
    // Rotation: 192.593 -71.380 144.272
    // Right Arm IK: Weight: 0.15 - 1

    public TwoBoneIKConstraint rightArmIk;
    private float weight;

    private void Start()
    {
        weight = rightArmIk.weight;        
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    InvokeRepeating("IncreaseWeight", 0, 0.01f);
        //}

        //if (Input.GetKeyDown(KeyCode.LeftControl))
        //{
        //    InvokeRepeating("DecreaseWeight", 0, 0.01f);
        //}

    }



    void IncreaseWeight()
    {
        if (weight < 1)
        {
            weight += 0.05f;
        }
        else
        {
            CancelInvoke("IncreaseWeight");
            InvokeRepeating("DecreaseWeight", .6f, 0.01f);
        }
        rightArmIk.weight = weight;
    }

    void DecreaseWeight()
    {
        if (weight > .2f)
        {
            weight -= 0.05f;
        }
        else
        {
            CancelInvoke("DecreaseWeight");
        }
        rightArmIk.weight = weight;
    }

    public void ChangeWeight()
    {
        InvokeRepeating("IncreaseWeight", 0, 0.01f);
    }

}
