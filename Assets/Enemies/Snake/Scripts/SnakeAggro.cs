using UnityEngine;
using System;

public class SnakeAggro : MonoBehaviour
{
    public event Action<Transform> OnAggro = delegate { };
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();

        if (player != null)
        {
            OnAggro(player.transform);
        }
    }
}
