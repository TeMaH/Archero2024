using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitPortal : MonoBehaviour
{
    public UnityAction SomeoneEntered;

    [SerializeField] private string _tagToCheck;
    [SerializeField] private GameObject _door;

    public void SetDoorIsActive(bool active)
    {
        if (_door != null)
        {
            _door.SetActive(active);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToCheck))
        {
            SomeoneEntered?.Invoke();
        }
    }
}
