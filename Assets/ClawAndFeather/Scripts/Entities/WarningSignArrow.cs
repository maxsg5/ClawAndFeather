using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Entities/Warning Sign")]
public class WarningSignArrow : MonoBehaviour
{
    public Transform target;
    
    private Vector2 _targetDirection;

    private void Update()
    {
        _targetDirection = target.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector2.right, _targetDirection);
    }
}
