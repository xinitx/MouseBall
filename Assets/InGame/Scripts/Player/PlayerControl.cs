using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    
    //�ƶ�
    private Joystick rocker;     //ҡ��
    public int speed;           //�ٶ�
    private Animator anim;    //�������
    private bool can_move = true; //�����ƶ�

    //��ɳ��
    public GameObject ball;    //��ȡɳ��
    public Transform hitpoint; //ɳ��������
    public float force;        //ɳ������������

    private Rigidbody2D rb;
    private void Start()
    {
        rocker = GameObject.Find("Move").GetComponent<Joystick>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (can_move)
            Move();
    }
    //�ƶ�
    private void Move()
    {
        //��ת
        if (rocker.Horizontal > 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (rocker.Horizontal < 0) transform.localScale = new Vector3(1, 1, 0);
        //λ��
        transform.Translate(rocker.Horizontal * Time.deltaTime * speed, rocker.Vertical * Time.deltaTime * speed, 0);

        //������
        anim.SetFloat("SPEED", Mathf.Abs(rocker.Horizontal) + Mathf.Abs(rocker.Vertical));

    }

    //����
    public void Start_Hit()
    {
        
        //����ʱֹͣ�ƶ�,���Ŷ���
        can_move = false;
        anim.SetBool("ISHIT", true);
    }
    public void Hit()
    {
        //����ɳ�����ؼ�֡Ͷɳ��
        GameObject this_ball =  Instantiate(ball, hitpoint.position, hitpoint.rotation);
        Rigidbody2D rb_ball = this_ball.GetComponent<Rigidbody2D>();

        rb_ball.AddForce(new Vector2(-force * transform.localScale.x, force * 0.5f), ForceMode2D.Impulse);
        

    }
    //�����ꡢ������������ƶ�
    public void Continue_move()
    {
        rb.velocity = Vector2.zero;
        can_move = true;
        anim.SetBool("ISHIT", false);
        anim.SetBool("ISINJURED", false);
    }

    //������
    public void BeHitted()
    {
        rb.velocity = Vector2.zero;
        can_move=false;
        anim.SetBool("ISINJURED", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if(collision.GetComponent<Rigidbody2D>() && collision.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
                BeHitted();
        }
    }
}
