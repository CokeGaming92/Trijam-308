using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockLocalPosition : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.zero;
    }
}
