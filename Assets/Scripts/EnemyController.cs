using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform Target;

    [SerializeField] private float _timeToUpdateDirection;
    [SerializeField] private float _movingTime;

    private IMovable MovementComponent { get { return _movement = _movement ?? GetComponent<IMovable>(); } }
    private IMovable _movement;

    private bool _isMoving;
    private float _timerToUpdateDirection;

    private void SetRandomTarget()
    {
        Target.position = new Vector3(Random.Range(-8, 9), 0.0f, Random.Range(-13, 14));
    }

    private IEnumerator MoveToTarget(float movingTime)
    {
        MovementComponent.StartMovement(Target.position);
        _isMoving = true;

        yield return new WaitForSeconds(movingTime);

        MovementComponent.StopMovement();
        _isMoving = false;
    }

    private void Start()
    {
        _timerToUpdateDirection = _timeToUpdateDirection;
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
            _timerToUpdateDirection = _timeToUpdateDirection;
            SetRandomTarget();
            StartCoroutine(MoveToTarget(_movingTime));
        }
    }
}
