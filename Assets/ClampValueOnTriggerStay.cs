using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampValueOnTriggerStay : MonoBehaviour
{
    [SerializeField] FloatVariable floatToClamp;
    [SerializeField] float clampValue;

    private void OnTriggerStay(Collider other)
    {
        floatToClamp.value = clampValue;
    }
}
