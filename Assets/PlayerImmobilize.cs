using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImmobilize : MonoBehaviour
{
    [SerializeField] FloatVariable firesLeft;
    [SerializeField] InputMovement movementScript;


    // Update is called once per frame
    void Update()
    {
        if ( firesLeft.value <= 0 )
        {
            if (movementScript != null)
                movementScript.enabled = false;
        }
    }
}
