using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Entities/Warning Sign")]
public class WarningSign : MonoBehaviour
{
    public Transform target;
    [Space]
    [SerializeReference] private GameObject Arrow;
    [SerializeField, Range(-360, 360)] private float _arrowAngle = 0.0f;
    [Header("Gizmo Settings")]
    public Color color = Color.white;

    private Vector2 _targetDirection;

    private Transform Hinge => Arrow.transform.parent;

    private void Update()
    {
        if (target == null)
        {
            Hinge.rotation = Quaternion.Euler(0, 0, _arrowAngle);
        }
        else
        {
            _targetDirection = target.position - transform.position;
            Hinge.rotation = Quaternion.FromToRotation(Vector2.right, _targetDirection);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Hinge.rotation = Quaternion.Euler(0, 0, _arrowAngle);
        if (target != null)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
