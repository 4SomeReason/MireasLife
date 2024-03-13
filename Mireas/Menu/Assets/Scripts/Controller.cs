using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 300.0f;
    private Rigidbody2D _body;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement1 = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement1; 
        float deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector2 movement2 = new Vector2(_body.velocity.x, deltaY);
        _body.velocity = movement2;
    }
}
