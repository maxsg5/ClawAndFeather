using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    #region Inspector
    public GameObject projectilePrefab;
    [Header("Launch Settings")]
    public bool spawnOnStart = false;
    [Min(0)] public float spawnDelay = 1.0f;
    [Min(0)] public float secondsAlive = 1.0f;
    [Space]
    [Range(-180, 180)] public float launchAngle = 0.0f;
    public bool flipX = false;
    [Min(0)] public float launchForce = 1.0f;
    #endregion

    private PrefabPool _projectilePool;
    private bool _spawning;

    private Vector2 LaunchDirection
    {
        get
        {
            int flip = flipX ? -1 : 1;
            return Quaternion.Euler(0, 0, launchAngle * flip) * (Vector2.right * flip);
        }
    }

    private void Awake()
    {
        _projectilePool = Singleton.Global.Prefabs.GetPoolByPrefab(projectilePrefab);
        _spawning = spawnOnStart;
    }

    private void Update()
    {
        if (_spawning)
        {
            SpawnProjectile();
            StartCoroutine(Wait(spawnDelay));
        }
    }

    public void SpawnProjectile()
    {
        var projectile = _projectilePool.Next;
        if (projectile != null && projectile.TryGetComponent(out Rigidbody2D projectileBody))
        {
            projectileBody.AddForce(LaunchDirection * launchForce);
        }
    }

    private IEnumerator Wait(float delay)
    {
        _spawning = false;
        yield return new WaitForSeconds(delay);
        _spawning = true;
    }

    // Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, LaunchDirection * launchForce);
    }
}
