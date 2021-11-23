using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private Rigidbody2D rb;     //��ø������
    private float inity;        //��ʼy����ֵ

    
    void Start()
    {
        
        //��ʼy����ֵ
        inity = transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        if (transform.position.y > inity)
            rb.AddForce(new Vector2(0, -10), ForceMode2D.Force);
        //���С�ڵ���Ͷ�����y������ֹͣ
        if (rb && transform.position.y < inity)
        {
            rb.velocity = Vector2.zero;
            Destroy(rb);
            
        }
    }

   
}
