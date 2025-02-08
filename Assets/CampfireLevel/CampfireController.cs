using System.Collections;
using UnityEngine;

public class CampfireController : MonoBehaviour
{
    public enum FireState { High, Mid, Low, Dead }
    public FireState currentState = FireState.High;

    public GameObject highState;
    public GameObject midState;
    public GameObject lowState;

    public float health = 100f;
    private float decayRate = 5f; // Health decay per second
    private int currentIndex = 0;
    private float cycleTime = 0.5f; // Time between flame model changes
    private bool isBurning = true;

    void Start()
    {
        midState.SetActive(false);
        lowState.SetActive(false);
        ActivateState(currentState);
        StartCoroutine(CycleFire());
        StartCoroutine(DecayFire());
    }

    IEnumerator CycleFire()
    {
        while (isBurning)
        {
            yield return new WaitForSeconds(cycleTime);
            CycleThroughModels();
        }
    }

    IEnumerator DecayFire()
    {
        while (isBurning)
        {
            yield return new WaitForSeconds(1f);
            health -= decayRate;

            if (health <= 0)
            {
                ChangeState(FireState.Dead);
                break;
            }
            else if (health <= 30)
            {
                ChangeState(FireState.Low);
            }
            else if (health <= 60)
            {
                ChangeState(FireState.Mid);
            }
        }
    }

    void CycleThroughModels()
    {
        Transform activeState = GetActiveState();
        if (activeState == null || activeState.childCount == 0) return;

        foreach (Transform child in activeState)
        {
            child.gameObject.SetActive(false);
        }

        currentIndex = (currentIndex + 1) % activeState.childCount;
        activeState.GetChild(currentIndex).gameObject.SetActive(true);
    }

    void ActivateState(FireState state)
    {
        highState.SetActive(false);
        midState.SetActive(false);
        lowState.SetActive(false);

        currentIndex = 0;
        if (state != FireState.Dead)
        {
            Transform activeState = GetActiveState();
            if (activeState != null && activeState.childCount > 0)
            {
                activeState.gameObject.SetActive(true);
                activeState.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void ChangeState(FireState newState)
    {
        currentState = newState;
        ActivateState(currentState);

        if (currentState == FireState.Dead)
        {
            isBurning = false;
            StopAllCoroutines();
        }
    }

    public void AddFuel(float amount)
    {
        if (currentState == FireState.Dead) return;

        health = Mathf.Min(100, health + amount);
        if (health > 60)
            ChangeState(FireState.High);
        else if (health > 30)
            ChangeState(FireState.Mid);
        else
            ChangeState(FireState.Low);
    }

    private Transform GetActiveState()
    {
        return currentState switch
        {
            FireState.High => highState.transform,
            FireState.Mid => midState.transform,
            FireState.Low => lowState.transform,
            _ => null
        };
    }
}
