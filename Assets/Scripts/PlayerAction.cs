using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    float h;
    float v;
    public float speed;
    bool isHorizonMove;
    Vector3 dirVec;

    Rigidbody2D rigid;
    Animator anim;
    GameObject scanObject;
    public GameManager manager;

    //Mobile Key Value
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;

    bool up_Down;
    bool down_Down;
    bool left_Down;
    bool right_Down;

    bool down_Up;
    bool up_Up;
    bool left_Up;
    bool right_Up;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move Value
        //PC & Mobile
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + right_Value + left_Value;
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical") + up_Value + down_Value;

        //Check Button Down & Up
        //PC & Mobile
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical") || up_Up || down_Up;
        
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove =  h != 0;

        //Animation
        if(anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetTrigger("isChange");
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetTrigger("isChange");
            anim.SetInteger("vAxisRaw", (int)v);
        }

        //Direction
        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        //Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
        }

        //Mobile Value Init(false)
        up_Down = false;
        down_Down = false;
        left_Down = false;
        right_Down = false;

        down_Up = false;
        up_Up = false;
        left_Up = false;
        right_Up = false;
    }

    private void FixedUpdate()
    {
        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h,0) : new Vector2(0,v);
        rigid.velocity = moveVec * speed;

        //Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if(rayhit.collider != null)
        {
            scanObject = rayhit.collider.gameObject;
        }
        else
            scanObject = null;
    }

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 1;
                up_Down = true;
                break;
            case "D":
                down_Value = -1;
                down_Down = true;
                break;
            case "L":
                left_Value = -1;
                left_Down = true;
                break;
            case "R":
                right_Value = 1;
                right_Down = true;
                break;
            case "A":
                if (scanObject != null)
                    manager.Action(scanObject);
                break;
            case "E":
                manager.SubMenuActive();
                break;

        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 0;
                up_Up = true;
                break;
            case "D":
                down_Value = 0;
                down_Up = true;
                break;
            case "L":
                left_Value = 0;
                left_Up = true;
                break;
            case "R":
                right_Value = 0;
                right_Up = true;
                break;
        }
    }
}
