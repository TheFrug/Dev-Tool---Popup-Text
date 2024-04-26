using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    private Rigidbody2D _rb;

    void Start()
    {
        //Assign Rigidbody2D component
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
    }

    private void Walk(Vector2 dir)
    {
        _rb.velocity = (new Vector2(dir.x * _speed, _rb.velocity.y));
    }


}
