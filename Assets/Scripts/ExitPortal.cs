using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitPortal : MonoBehaviour
{
    public UnityAction SomeoneEntered;

    [SerializeField] private string _tagToCheck;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToCheck))
        {
            SomeoneEntered?.Invoke();
        }
    }
}
