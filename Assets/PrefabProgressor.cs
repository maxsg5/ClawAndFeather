using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabProgressor : MonoBehaviour
{
    public float scrollSpeed;

    private void Update()
    {
        transform.Translate(0, -scrollSpeed * Time.deltaTime, 0);
    }
}
