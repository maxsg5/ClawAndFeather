using UnityEngine;

public class PrefabProgressor : MonoBehaviour
{
    [Min(0)] public float scrollSpeed;

    private void Update()
    {
        transform.Translate(0, -scrollSpeed * Time.deltaTime, 0);
    }
}
