using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    Collider2D collider;
    List<Collider2D> collisions = new List<Collider2D>(1);   
    ContactFilter2D filter;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        filter = new ContactFilter2D();
        filter.NoFilter();

    }

    void Update()
    {
        collider.OverlapCollider(filter , collisions);

        foreach (var c in collisions)
        {
            var obj = c.GetComponent<Interactable>();
            if (obj != null)
            
                if (Input.GetKeyDown(KeyCode.E))
                    obj.Interact();
            
        }

    }
}

