using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddValueOnAwake : MonoBehaviour
{
    [SerializeField] FloatVariable fireCount;
    [SerializeField] float amountToAdd;

    private void Awake()
    {
        fireCount.value += amountToAdd;
    }
}
