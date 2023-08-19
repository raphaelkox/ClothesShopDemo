using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D parentCollider;
    [SerializeField] private BoxCollider2D childCollider;

    private void Start() {
        Physics2D.IgnoreCollision(parentCollider, childCollider);
    }
}
