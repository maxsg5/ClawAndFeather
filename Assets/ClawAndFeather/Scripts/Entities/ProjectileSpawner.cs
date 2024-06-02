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

    [Header("Gizmo Settings")]
    public bool showTrajectory = true;
    [Range(1, 100)] public int precision = 50;
    public Color trajectoryColour = Color.red;
    [Space]
    public bool showDirection = true;
    public Color directionColour = Color.green;
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
        var projectileObject = _projectilePool.Next;
        if (projectileObject != null && projectileObject.TryGetComponent(out Projectile projectile))
        {
            projectile.Body.AddForce(LaunchDirection * launchForce);
            projectile.DespawnIn(seconds: secondsAlive);
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
        // Direction
        if (showDirection)
        {
            Gizmos.color = directionColour;
            Gizmos.DrawRay(transform.position, LaunchDirection * launchForce);
        }
        // Trajectory
        if (showTrajectory)
        {
            Gizmos.color = trajectoryColour;

            Vector2[] points = new Vector2[precision];
            float timeIncrement = secondsAlive / precision;

            Projectile.ProjectileMotion(0, LaunchDirection * launchForce, out points[0]);
            for (int i = 1; i < precision; i++)
            {
                Projectile.ProjectileMotion(i * timeIncrement, LaunchDirection * launchForce, out points[i]);
                Gizmos.DrawLine(points[i - 1], points[i]);
            }
        }
    }
}
