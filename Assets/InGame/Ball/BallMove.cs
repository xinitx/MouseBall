using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private Rigidbody2D rb;     //获得刚体组件
    private float inity;        //初始y坐标值

    
    void Start()
    {
        
        //初始y坐标值
        inity = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        if (transform.position.y > inity)
            rb.AddForce(new Vector2(0, -10), ForceMode2D.Force);
        //如果小于等于投出点的y坐标则停止
        if (rb && transform.position.y < inity)
        {
            rb.velocity = Vector2.zero;
            Destroy(rb);
            
        }
    }

   
}
