using System.Collections;
using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Entities/Projectile Spawner")]
public class ProjectileSpawner : MonoBehaviour
{
    #region Inspector
    [SerializeReference] public Projectile projectile;
    [Header("Launch Settings")]
    public bool spawnOnStart = false;
    [Min(0)] public float spawnDelay = 1.15f;
    [Tooltip("Time in seconds until the projectile de-spawns.")]
    [Min(0)] public float lifeTime = 1.15f;
    [Space]
    [Min(0)] public float launchForce = 8.0f;
    [Range(-180, 180)] public float launchAngle = 45.0f;
    public bool flipX = false;

    [Header("Warning Settings")]
    public WarningSign warningObject;
    [Min(0)] public float warningTimeBeforeSpawn;
    [Min(0)] public float warningLifeSpan;

    [Header("Gizmo Settings")]
    [SerializeField] Color _color = Color.yellow;
    [Space]
    [SerializeField] bool _showLaunchVelocity = false;
    [Space]
    [SerializeField] bool _showTrajectory = true;
    [SerializeField, Range(1, 100)] int _resolution = 25;
    [Space]
    [SerializeField] bool _showFinalPosition = true;
    [SerializeField, Range(0,1)] float _radius = 0.2f;
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
            if (projectile == null)
            {
                throw new System.ArgumentNullException(nameof(projectile));
            }

            _projectilePool = Singleton.Global.Prefabs.GetPoolByPrefab(projectile.gameObject);
            if (_projectilePool == null)
            {
                throw new System.NullReferenceException($"No prefab pool using prefab {projectile.gameObject.name} was found.");
            }

            if (warningObject != null)
            {
                warningObject.gameObject.SetActive(false);
            }

            if (spawnOnStart)
            {
                StartCoroutine(SpawnProjectile());
            }
            else
            {
                _spawning = true;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
            Destroy(this);
        }
    }

    private void Update()
    {
        if (_spawning)
        {
            StartCoroutine(SpawnProjectile());
        }
    }

    private IEnumerator SpawnProjectile()
    {
        _spawning = false;
        if (warningObject != null)
        {
            float currentTime = 0;
            while (currentTime < spawnDelay)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= spawnDelay - warningTimeBeforeSpawn)
                { warningObject.gameObject.SetActive(true); }
                yield return null;
            }
            warningObject.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(spawnDelay);
        }

        var projectileObject = _projectilePool.Next;
        if (projectileObject != null && projectileObject.TryGetComponent(out Projectile projectile))
        {
            projectile.gameObject.SetActive(true);
            projectile.transform.position = transform.position;
            projectile.Body.AddForce(LaunchDirection * launchForce, ForceMode2D.Impulse);
            projectile.Despawn(lifeTime);
        }
        _spawning = true;
    }

    // Gizmos
    private void OnDrawGizmosSelected()
    {
        var force = LaunchDirection * launchForce;
        Vector2 velocity = (projectile != null)
            ? force / projectile.Body.mass
            : force;

        Gizmos.color = _color;
        // Direction
        if (_showLaunchVelocity)
        {
            Gizmos.DrawRay(transform.position, velocity);
        }
        // Trajectory
        if (_showTrajectory)
        {
            float timeStep = lifeTime * (1f / _resolution);
            var previousPosition = Projectile.ProjectileMotion(0, velocity, transform.position, projectile);
            for (int i = 1; i <= _resolution; i++)
            {
                var position = Projectile.ProjectileMotion(timeStep * i, velocity, transform.position, projectile);
                Gizmos.DrawLine(previousPosition, position);
                previousPosition = position;
            }
        }

        if (_showFinalPosition)
        {
            Gizmos.DrawWireSphere(Projectile.ProjectileMotion(lifeTime, velocity, transform.position, projectile), _radius); 
        }
    }
}
