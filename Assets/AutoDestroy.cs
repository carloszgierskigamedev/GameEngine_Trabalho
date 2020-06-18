using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField]
    private float _time = 2f;

    void Start()
    {
        Destroy(gameObject, _time);
    }
}