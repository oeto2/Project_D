using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private float drag = 0.3f;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    private Rigidbody _rigidbody;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (verticalVelocity < 0f)
        //{
        //    // Physics.gravity.y = - 9.7
        //    verticalVelocity = Physics.gravity.y * Time.deltaTime;
        //}
        //else
        //{
        //    verticalVelocity += Physics.gravity.y * Time.deltaTime;
        //}
        //
        //impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void Reset()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    public void Jump(float jumpForce)
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}