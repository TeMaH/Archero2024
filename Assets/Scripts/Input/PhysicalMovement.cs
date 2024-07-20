using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _movementSpeed;

    private Rigidbody RB { get { return _rb = _rb ?? GetComponent<Rigidbody>(); } }
    private Rigidbody _rb;

    public void SartMovement(Vector2 direction)
    {
        RB.velocity = direction * _movementSpeed;
        Debug.LogWarning(RB.velocity);
    }

    public void StopMovement()
    {
        RB.velocity = Vector2.zero;
    }
}
