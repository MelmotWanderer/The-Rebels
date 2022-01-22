using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private DogKnightController _dogKnightController;
    void OnTriggerEnter()
    {
        _dogKnightController.EnemyTargetFound();
    }
}
