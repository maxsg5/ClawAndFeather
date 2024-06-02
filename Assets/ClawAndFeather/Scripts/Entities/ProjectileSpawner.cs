using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    #region Inspector
    [SerializeReference] public GameObject projectilePrefab;
    [Header("Launch Settings")]
    public bool spawnOnStart = false;
    [Min(0)] public float spawnDelay = 1.15f;
    [Tooltip("Time in seconds until the projectile de-spawns.")]
    [Min(0)] public float lifeTime = 1.15f;
    [Space]
    [Min(0)] public float launchForce = 8.0f;
    [Range(-180, 180)] public float launchAngle = 45.0f;
    public bool flipX = false;

    [Header("Gizmo Settings")]
    public Color color = Color.yellow;
    [Space]
    public bool launchVelocity = false;
    [Space]
    public bool trajectory = true;
    [Range(1, 100)] public int resolution = 25;
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

    private void Start()
    {
        try
        {
            if (projectilePrefab == null)
            {
                throw new System.ArgumentNullException(nameof(projectilePrefab));
            }

            _projectilePool = Singleton.Global.Prefabs.GetPoolByPrefab(projectilePrefab);
            if (_projectilePool == null)
            {
                throw new System.NullReferenceException($"No prefab pool using prefab {projectilePrefab.name} was found.");
            }

            if (spawnOnStart)
            {
                SpawnProjectile();
                StartCoroutine(Wait(spawnDelay));
            }
        }
        catch (System.Exception ex)
        {
            gameObject.SetActive(false);
            Debug.LogException(ex);
        }
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
        _spawning = false;
        var projectileObject = _projectilePool.Next;
        if (projectileObject != null && projectileObject.TryGetComponent(out Projectile projectile))
        {
            projectile.gameObject.SetActive(true);
            projectile.Body.position = transform.position;
            projectile.Body.AddForce(LaunchDirection * launchForce, ForceMode2D.Impulse);
            projectile.DespawnIn(seconds: lifeTime);
        }
    }

    private IEnumerator Wait(float delay)
    {
        yield return new WaitForSeconds(delay);
        _spawning = true;
    }

    // Gizmos
    private void OnDrawGizmosSelected()
    {
        var force = LaunchDirection * launchForce;
        Vector2 velocity = (projectilePrefab != null && projectilePrefab.TryGetComponent(out Projectile projectile))
            ? force / projectile.Body.mass
            : force;

        Gizmos.color = color;
        // Direction
        if (launchVelocity)
        {
            Gizmos.DrawRay(transform.position, velocity);
        }
        // Trajectory
        if (trajectory)
        {
            float timeStep = lifeTime * (1f / resolution);
            var previousPosition = Projectile.ProjectileMotion(0, velocity, transform.position);
            for (int i = 1; i <= resolution; i++)
            {
                var position = Projectile.ProjectileMotion(timeStep * i, velocity, transform.position);
                Gizmos.DrawLine(previousPosition, position);
                previousPosition = position;
            }
            Gizmos.DrawWireSphere(previousPosition, 0.2f);
        }
    }
}
