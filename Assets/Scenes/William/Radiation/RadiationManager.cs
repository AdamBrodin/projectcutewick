using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationManager : MonoBehaviour
{
    const int STATELENGTH = 5;
    public enum State { normal, low, medium, high, critical }
    private int currentState = 0;
    [SerializeField]
    [Range(0,16.7f)]
    private float stateSpan = 18;

    [SerializeField]
    private float maxRadiation = 100;
    public float currentRadiation = 0;

    private void Update()
    {
        currentState = Mathf.FloorToInt(currentRadiation / stateSpan);

        switch (currentState)
        {
            case (int)State.normal:
                break;
            case (int)State.low:
                break;
            case (int)State.medium:
                break;
            case (int)State.high:
                break;
            case (int)State.critical:
                break;
        }
    }
}
