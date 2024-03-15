using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private float drag = 0.3f;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    public float verticalVelocity;
    private Player Player;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Awake()
    {
        Player = GetComponent<Player>();
    }

    void Update()
    {
        if (Player.stateMachine.GetCurrentState() == Player.stateMachine.FallState)
        {
            verticalVelocity = Physics.gravity.y * 0.3f;
        }
        else if (verticalVelocity < 0f)
        {
            // Physics.gravity.y = - 9.7
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
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
        verticalVelocity += jumpForce;
        // 위로 10만큼 캐릭터 y 0=>10
    }
}