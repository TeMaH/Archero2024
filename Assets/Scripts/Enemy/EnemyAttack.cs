using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnProjectiles;
    public float attackInterval = 2.0f;
    private float attackTimer;

    private SearchTarget searchTarget;

    private void Start()
    {
        searchTarget = GetComponent<SearchTarget>();
        attackTimer = attackInterval;
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0.0f)
        {
            GameObject target = searchTarget.GetTarget();

            if (target != null)
            {
                FireProjectile(target.transform.position);
                attackTimer = attackInterval;
            }
        }
    }

    private void FireProjectile(Vector3 targetPosition)
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnProjectiles.position, spawnProjectiles.rotation);
        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.SetTarget(targetPosition);
    }
}
