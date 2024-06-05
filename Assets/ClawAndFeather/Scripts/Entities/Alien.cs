using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Entities/Turbine")]
[RequireComponent(typeof(Rigidbody2D))]
public class Alien : MonoBehaviour
{
    #region Inspector
    public Transform target;

    [Header("Movement Settings")]
    [Min(0)] public float moveSpeed = 5.0f;
    [Min(1)] public float maxSpeed = 10.0f;
    [Range(0, 360)] public float turnSpeed = 30.0f;

    [Header("Detection Settings")]
    [Min(0)] public float detectionRange;
    [Min(0)] public float pursuitTime = 1.0f;
    [SerializeField] private float _pursuitTimer = 0.0f;
    #endregion

    private Rigidbody2D _rb;

    public bool TargetInRange => Vector3.Distance(transform.position, target.position) < detectionRange;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void FixedUpdate()
    {
        if (TargetInRange)
        {
            if (_pursuitTimer < pursuitTime)
            {
                // Rotate towards target
                Vector2 displacement = target.position - transform.position;
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    Quaternion.FromToRotation(Vector2.right, displacement),
                    turnSpeed);

                // move
                _rb.AddForce(moveSpeed * Time.fixedDeltaTime * transform.right);
                _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);
            }
            _pursuitTimer += Time.fixedDeltaTime;
        }
        else
        {
            _pursuitTimer = 0;
        }
    }
}
