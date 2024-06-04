using System.Collections;
using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Menu/Menus")]
public class Menus : MonoBehaviour
{
    public float startingTime;
    private GameObject[] _menus;
    private void Awake()
    {
        _menus = new GameObject[transform.childCount];
        for (int i = 0; i < _menus.Length; i++)
        { 
            _menus[i] = transform.GetChild(i).gameObject;
            _menus[i].SetActive(false);
        }
        StartCoroutine(Initialize(startingTime));
    }
    private IEnumerator Initialize(float time)
    {
        yield return new WaitForSeconds(time);
        _menus[0].SetActive(true);
    }
}
