using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] FloatVariable playerHealth;
    [SerializeField] GameObject deathVFX;

    // Update is called once per frame
    void Update()
    {
        if ( playerHealth.value <= 0 )
        {
            if ( deathVFX != null)
            {
                Instantiate(deathVFX, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
