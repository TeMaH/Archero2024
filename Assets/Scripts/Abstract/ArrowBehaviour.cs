using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] private int damageRate;
    public void SetDamage(int skillLevel) 
    { 
        damageRate = skillLevel;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.TryGetComponent<HealthComponent>(out var healthComponent))
        {
            healthComponent.GetDamage(damageRate);
        }

        Destroy(gameObject);
    }
}
