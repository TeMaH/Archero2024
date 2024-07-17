using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTarget : MonoBehaviour
{
    public float detectionRadius = 20.0f;
    public float viewRadius = 10.0f;
    public LayerMask targetLayer;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestTarget();
    }
    void FindNearestTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        float nearestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (Collider col in colliders)
        {
            float distance = Vector3.Distance(transform.position, col.transform.position);

            if (distance < viewRadius && distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTarget = col.gameObject;
            }
        }

        target = nearestTarget;
        if (target != null)
        {
            Attack attackScript = GetComponent<Attack>();
            if (attackScript != null)
            {
                attackScript.SetTarget(target);
            }
        }
    }
}
