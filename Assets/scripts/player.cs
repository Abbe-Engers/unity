using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour {
    public float moveSpeed = 10.0f;
    bool facingRight = true;
    public int level = 1;
    public float jumpSpeed = 300.0f;

    public Text levelCount;

    public float speed = 10.0f;

    public float smoothTimeY = 0.1f;
    public float smoothTimeX = 0.1f;

    GameObject Camera;
    Animator anim;
    Rigidbody2D rigid;
    Vector2 cameraVelocity;

    // Use this for initialization
    void Start () {
        UpdateCounter();

        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void SetTransformXY(float x, float y)
    {
        transform.position = new Vector3(x, y);
    }

    void UpdateCounter()
    {
        levelCount.text = level.ToString();
    }

    void FixedUpdate() {
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));

        rigid.velocity = new Vector2(move * moveSpeed, rigid.velocity.y);

        if (move > 0 && !facingRight)
        {
            FlipFacing();
        }
        else if (move < 0 && facingRight)
        {
            FlipFacing();
        }

        if (Input.GetButtonDown("Jump") && rigid.velocity.y < 1)
        {
            rigid.AddForce(Vector2.up * jumpSpeed);
        }
    }

    void FlipFacing()
    {
        facingRight = !facingRight;
        Vector3 charScale = transform.localScale;
        charScale.x = charScale.x * -1;
        transform.localScale = charScale;
    }

    // Update is called once per frame
    void Update () {

        UpdateCounter();

        float posX = Mathf.SmoothDamp(Camera.transform.position.x, transform.position.x, ref
        cameraVelocity.x, smoothTimeX);

        float posY = Mathf.SmoothDamp(Camera.transform.position.y, transform.position.y, ref
        cameraVelocity.y, smoothTimeY);

        //check if win
        if (level == 1)
        {
            if (transform.position.y < 0.8 && transform.position.y > 0 && transform.position.x > 107.2 && transform.position.x < 120.27)
            {
                //win
                SetTransformXY(156.4f, 4.19f);
                rigid.velocity = new Vector2(0, 0);
                Camera.transform.position = new Vector3(156.4f, 4.19f, Camera.transform.position.z);
                level = 2;
            }

            if (transform.position.y < -2)
            {
                SetTransformXY(15.01f, 4.19f);
                rigid.velocity = new Vector2(0, 0);
                level = 1;
            }
        }
        if (level == 2)
        {
            if (transform.position.y < 0.8 && transform.position.y > 0 && transform.position.x > 107.2 && transform.position.x < 120.27)
            {
                //win
                SetTransformXY(156.4f, 4.19f);
                rigid.velocity = new Vector2(0, 0);
                Camera.transform.position = new Vector3(156.4f, 4.19f, Camera.transform.position.z);
                level = 3;
            }

            if (transform.position.y < -5)
            {
                SetTransformXY(156.4f, 4.19f);
                rigid.velocity = new Vector2(0, 0);
                level = 1;
            }
        }
        

        if (posY < 3)
        {
            Camera.transform.position = new Vector3(posX, posY, Camera.transform.position.z);
        }

        else
        {
            Camera.transform.position = new Vector3(posX, 3, Camera.transform.position.z);
        }
    }
}
