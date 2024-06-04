using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
