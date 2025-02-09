using UnityEngine;

public class CollisionActivator : MonoBehaviour
{
    public GameObject objectToDeactivate;
    public GameObject objectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Adjust tag as needed
        {
            if (objectToDeactivate != null)
                objectToDeactivate.SetActive(false);

            if (objectToActivate != null)
                objectToActivate.SetActive(true);

            other.gameObject.GetComponent<FireHealth>()!.ResetHealth();
        }
    }
}
