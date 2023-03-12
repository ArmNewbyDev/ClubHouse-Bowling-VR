using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private int _addForce;
    private Rigidbody Rigidbody;


    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        AddForceM();
    }

    private void AddForceM()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Rigidbody.AddForce(new Vector3(0, 0, _addForce));
        }

    }
}
