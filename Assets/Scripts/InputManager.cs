using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public bool jumpInput;
    public bool runInput;
    public bool inputQ;
    public bool inputE;
    public bool inputF;
    public bool inputR;
    public bool basicAttack;




    void Update()
    {
        MoveInput();
        SkillsInput();
    }

    public void MoveInput()
    {
        runInput = Input.GetKey(KeyCode.LeftShift);
        jumpInput = Input.GetKey(KeyCode.Space);
    }

    public void SkillsInput()
    {
        basicAttack = Input.GetKey(KeyCode.Mouse1);

        inputQ = Input.GetKey(KeyCode.Q);
        inputE = Input.GetKey(KeyCode.E);
        inputF = Input.GetKey(KeyCode.F);
        inputR = Input.GetKey(KeyCode.R);
    }


}
