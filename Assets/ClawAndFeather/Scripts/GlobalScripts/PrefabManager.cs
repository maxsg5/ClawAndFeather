using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Scripts/Claw and Feather/Global/Prefab Manager")]
public class PrefabManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _activePrefabs;
    private int[] _prefabIDs;
    private PrefabPool[] _pools;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _prefabIDs = new int[_activePrefabs.Length];
        _pools = FindObjectsOfType<PrefabPool>();
        LinkPrefabPools();
    }
    // Links the prefab pools to the appropriate active prefabs selected.
    private void LinkPrefabPools()
    {
        for (int i = 0; i < _pools.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < _activePrefabs.Length && !found; j++)
            {
                if (_pools[i].prefab == _activePrefabs[j])
                {
                    found = true;
                    _prefabIDs[j] = i;
                }
            }
        }
    }
    /// <summary>
    /// Grab an active pool inside the scene based on the <paramref name="prefab"/> given.
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns>Selected <see cref="PrefabPool" /> or <see langword="null" /> if it cannot find it.</returns>
    public PrefabPool GetPoolByPrefab(GameObject prefab) => GetPoolByPrefab(prefab.name);
    public PrefabPool GetPoolByPrefab(Transform prefab) => GetPoolByPrefab(prefab.name);
    public PrefabPool GetPoolByPrefab(string prefabName)
    {
        bool found = false;
        PrefabPool foundItem = null;
        for (int i = 0; i < _activePrefabs.Length && !found; i++)
        {
            if (prefabName == _activePrefabs[i].name)
            {
                found = true;
                foundItem = _pools[_prefabIDs[i]];
            }
        }
        return foundItem;
    }
}
