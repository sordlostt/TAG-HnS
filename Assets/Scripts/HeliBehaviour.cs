using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform rotor;

    [SerializeField]
    Transform rearRotor;

    [SerializeField]
    float rotorRPM;

    [SerializeField]
    float rearRotorRPM;

    [SerializeField]
    Transform target;

    private void Update()
    {
        rotor.transform.RotateAround(transform.up, rotorRPM/60.0f * Time.deltaTime);
        rearRotor.RotateAround(transform.right, rearRotorRPM / 60.0f * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
    }

    public bool isOnTarget()
    {
        return Vector3.Distance(transform.position, target.position) < 0.5f;
    }
}
