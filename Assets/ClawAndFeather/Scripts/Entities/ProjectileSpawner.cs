using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    #region Inspector
    [SerializeReference] public GameObject projectilePrefab;
    [Header("Launch Settings")]
    public bool spawnOnStart = false;
    [Min(0)] public float spawnDelay = 4.0f;
    [Tooltip("Time in seconds until the projectile de-spawns."), Min(0)] public float lifeTime = 4.0f;
    [Space]
    [Min(0)] public float launchForce = 2.0f;
    [Range(-180, 180)] public float launchAngle = 45.0f;
    public bool flipX = false;

    [Header("Gizmo Settings")]
    public Color color = Color.red;
    [Space]
    public bool launchVelocity = true;
    [Space]
    public bool trajectory = true;
    [Range(1, 100)] public int precision = 50;
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
        try
        {
            if (projectilePrefab == null)
            {
                throw new System.ArgumentNullException($"No value was assigned to {nameof(projectilePrefab)}. ");
            }

            _projectilePool = Singleton.Global.Prefabs.GetPoolByPrefab(projectilePrefab);
            if (_projectilePool == null)
            {
                throw new System.NullReferenceException($"No prefab pool using prefab {projectilePrefab.name} was found.");
            }

            if (!spawnOnStart)
            {
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
        var projectileObject = _projectilePool.Next;
        if (projectileObject != null && projectileObject.TryGetComponent(out Projectile projectile))
        {
            projectile.Body.AddForce(LaunchDirection * launchForce);
            projectile.DespawnIn(seconds: lifeTime);
        }
    }

    private IEnumerator Wait(float delay)
    {
        _spawning = false;
        yield return new WaitForSeconds(delay);
        _spawning = true;
    }

    // Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        // Direction
        if (launchVelocity)
        {
            Gizmos.DrawRay(transform.position, LaunchDirection * launchForce);
        }
        // Trajectory
        if (trajectory)
        {
            float timeStep = lifeTime * (1f / precision);
            var previousPosition = Projectile.ProjectileMotion(0, LaunchDirection * launchForce, transform.position);
            for (int i = 1; i <= precision; i++)
            {
                var position = Projectile.ProjectileMotion(timeStep * i, LaunchDirection * launchForce, transform.position);
                Gizmos.DrawLine(previousPosition, position);
                previousPosition = position;
            }

            Gizmos.DrawSphere(Projectile.ProjectileMotion(lifeTime, LaunchDirection * launchForce, transform.position), 0.2f);
        }
    }
}
