using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    [UnityEngine.SerializeField]
    float _speed = 5f;
    // Position to which fish is moving towards
    Vector2 _target;

    // Delegate to notify Fish has reached the target: f(target, position)
    public List<Action<Vector2, Vector2>> DelegateReachedTarget = new List<Action<Vector2, Vector2>>();
    // Function to tell fish where to go next once it reached the target: f(target, position) -> new_target
    Func<Vector2, Vector2, Vector2> GetNewTarget = (target, position) => UnityEngine.Random.insideUnitCircle * 10f;

    // Convert Vector3 position to Vector2
    Vector2 GetPosition() {
        return new Vector2(transform.position.x, transform.position.y);
    }

    // Check if target is within epsilon distance
    bool TargetDistanceCheck(float epsilon = 2e-4f) {
        return Vector2.Dot(_target - GetPosition(), _target - GetPosition()) < epsilon;
    }

    // Move the fish closer to a target
    public void MoveToTarget(float dt) {
        Vector2 dx = _target - new Vector2(transform.position.x, transform.position.y);
        transform.position += Vector3.ClampMagnitude(_speed * dx, _speed) * dt;
    }

    void Update() {
        float dt = UnityEngine.Time.deltaTime;
        if(TargetDistanceCheck()){
            // Notify all listeners than fish has reached target
            DelegateReachedTarget.ForEach(f => f(_target, GetPosition()));
            _target = GetNewTarget(_target, GetPosition());
        }
        MoveToTarget(dt);
    }
}
