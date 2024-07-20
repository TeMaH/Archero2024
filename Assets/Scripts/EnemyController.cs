using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float TimeToUpdateDirection;
    [SerializeField] private float _movingTime;

    private IMovable MovementComponent { get { return _movement = _movement ?? GetComponent<IMovable>(); } }
    private IMovable _movement;

    private Vector2 _direction;

    private bool _isMoving;
    private float _timerToUpdateDirection;

    private void SetRandomTarget()
    {
        _direction = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
    }

    private IEnumerator MoveToTarget(float movingTime)
    {
        MovementComponent.SartMovement(_direction);
        _isMoving = true;

        yield return new WaitForSeconds(movingTime);

        MovementComponent.StopMovement();
        _isMoving = false;
    }

    private void Start()
    {
        _timerToUpdateDirection = TimeToUpdateDirection;
    }
    private void Update()
    {
        if (_isMoving)
        {
            return;
        }

        _timerToUpdateDirection -= Time.deltaTime;

        if (_timerToUpdateDirection <= 0.0f) 
        {
            _timerToUpdateDirection = TimeToUpdateDirection;
            SetRandomTarget();
            StartCoroutine(MoveToTarget(_movingTime));
        }
    }
}
