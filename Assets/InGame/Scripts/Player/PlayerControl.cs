using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    
    //移动
    private Joystick rocker;     //摇杆
    public int speed;           //速度
    private Animator anim;    //动画组件
    private bool can_move = true; //控制移动

    //丢沙包
    public GameObject ball;    //获取沙包
    public Transform hitpoint; //沙包丢出点
    public float force;        //沙包丢出的力度

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
    //移动
    private void Move()
    {
        //翻转
        if (rocker.Horizontal > 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (rocker.Horizontal < 0) transform.localScale = new Vector3(1, 1, 0);
        //位移
        transform.Translate(rocker.Horizontal * Time.deltaTime * speed, rocker.Vertical * Time.deltaTime * speed, 0);

        //换动画
        anim.SetFloat("SPEED", Mathf.Abs(rocker.Horizontal) + Mathf.Abs(rocker.Vertical));

    }

    //攻击
    public void Start_Hit()
    {
        
        //攻击时停止移动,播放动画
        can_move = false;
        anim.SetBool("ISHIT", true);
    }
    public void Hit()
    {
        //创建沙包，关键帧投沙包
        GameObject this_ball =  Instantiate(ball, hitpoint.position, hitpoint.rotation);
        Rigidbody2D rb_ball = this_ball.GetComponent<Rigidbody2D>();

        rb_ball.AddForce(new Vector2(-force * transform.localScale.x, force * 0.5f), ForceMode2D.Impulse);
        

    }
    //攻击完、被打中完继续移动
    public void Continue_move()
    {
        rb.velocity = Vector2.zero;
        can_move = true;
        anim.SetBool("ISHIT", false);
        anim.SetBool("ISINJURED", false);
    }

    //被攻击
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
