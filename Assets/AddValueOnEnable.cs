using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddValueOnEnable : MonoBehaviour
{
    [SerializeField] FloatVariable fireCount;
    [SerializeField] float amountToAdd;

    private void OnEnable()
    {
        fireCount.value += amountToAdd;
    }
}
