using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    KeyCode up = KeyCode.W;
    KeyCode down = KeyCode.S;
    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;
    private Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(up))
        {

            _anim.SetBool("turn_left", false);
            
        }
        if (Input.GetKeyUp(up))
        {
            _anim.SetBool("turn_right", false);
        }
        if (Input.GetKeyDown(down))
        {

            _anim.SetBool("turn_left", false);
            
        }
        if (Input.GetKeyUp(down))
        {
            _anim.SetBool("turn_right", false);
        }
        if (Input.GetKeyDown(left))
        {
            _anim.SetBool("turn_left", true);
            

        }
        if (Input.GetKeyUp(left))
        {
            _anim.SetBool("turn_left", false);
        }
        if (Input.GetKeyDown(right))
        { 
            _anim.SetBool("turn_right", true);
            
        }
        if (Input.GetKeyUp(right))
        {
            _anim.SetBool("turn_right", false);
        }

    }

}
