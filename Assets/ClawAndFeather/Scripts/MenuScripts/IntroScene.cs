using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/MenuScripts/IntroScene")]
public class IntroScene : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void EndScene()
    {
        Debug.Log("WOW");
    }
}
