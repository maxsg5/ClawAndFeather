using System.Linq;
using UnityEngine;

[AddComponentMenu("Scripts/Entities/Obstacle")]
public class Obstacle : MonoBehaviour
{
	public Collider2D[] colliders;

    private void Awake()
    {
		try
		{
            if (colliders is null || colliders.Length < 1)
            {
                colliders = GetComponentsInChildren<Collider2D>().Where(c => c.isTrigger == false).ToArray();
                if (colliders.Length < 1)
                {
                    throw new System.Exception($"There are no non-trigger colliders on {gameObject.name} or it's children.");
                }
            }
        }
		catch (System.Exception ex)
		{
            Debug.LogException(ex);
            Destroy(this);
		}
    }
}
