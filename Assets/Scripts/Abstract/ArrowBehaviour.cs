using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    float damageRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDamage(float skillLevel) 
    { 
        damageRate = skillLevel;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //other.GetComponent<TakeDamage>().TakeDamage(damageRate);
            Debug.Log("Strike!!!");
            Destroy(gameObject);
        }
    }
    private void OnCollisionStay(Collision other)
    {
    }
    private void OnCollisionExit(Collision other)
    {
    }
}
