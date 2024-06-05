using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Entities/Alien Cat")]
[RequireComponent(typeof(Rigidbody2D))]
public class Alien : MonoBehaviour
{
    #region Inspector
    public Transform target;
    [SerializeField] private bool _autoDetectPlayer = true;

    [Header("Movement Settings")]
    [Min(0)] public float moveSpeed = 5.0f;
    [Min(1)] public float maxSpeed = 10.0f;
    [Range(0, 360)] public float turnSpeed = 30.0f;

    [Header("Detection Settings")]
    [Min(0)] public float detectionRange = 3.0f;
    [Min(0)] public float pursuitTime = 1.0f;

    [Header("Gizmo Settings")]
    [SerializeField] private Color _colour = Color.white;
    [SerializeField] private bool _drawAlways = false;
    [Space]
    [SerializeField] private bool _showDetectionRange = true;
    [SerializeField] private bool _showTargetLine = false;
    #endregion

    private Rigidbody2D _rb;
    private float _pursuitTimer = 0.0f;

    public bool TargetInRange => Vector3.Distance(transform.position, target.position) < detectionRange;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_autoDetectPlayer)
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

    private void OnDrawGizmosSelected()
    {
        if (!_drawAlways)
        {
            DrawGizmos();
        }
    }

    private void DrawGizmos()
    {
        Gizmos.color = _colour;
        if (_showDetectionRange)
        {
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

        if (_showTargetLine)
        {
            Gizmos.DrawLine(transform.position, target.position);
        }
    }

    private void OnDrawGizmos()
    {
        if (_drawAlways)
        {
            DrawGizmos();
        }
    }
}
