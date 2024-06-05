using System.Linq;
using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Entities/Obstacle")]
public class Obstacle : MonoBehaviour
{
	public Collider2D[] colliders;

    private void Awake()
    {
		try
		{
            if (colliders is null || colliders.Length < 1)
            {
                SetColliders();
            }
        }
		catch (System.Exception ex)
		{
            Debug.LogException(ex);
            Destroy(this);
		}
    }

    [ContextMenu("Get All Colliders")]
    private void SetColliders()
    {
        colliders = GetComponentsInChildren<Collider2D>().Where(c => c.isTrigger).ToArray();
        if (colliders.Length < 1)
        {
            throw new System.Exception($"There are no trigger colliders on {gameObject.name} or it's children.");
        }
    }
}
