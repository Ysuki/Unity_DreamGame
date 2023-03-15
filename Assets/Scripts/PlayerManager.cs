using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{


    [SerializeField] private KeyCode jumpInput;
    [SerializeField] private KeyCode runInput;
    [SerializeField] private KeyCode skill_1_Input;
    [SerializeField] private KeyCode skill_2_Input;
    [SerializeField] private KeyCode skill_3_Input;
    [SerializeField] private KeyCode skill_4_Input;
    [SerializeField] private KeyCode basicAttackInput;

    public bool jump;
    public bool run;
    public bool skill_1;
    public bool skill_2;
    public bool skill_3;
    public bool skill_4;
    public bool basicAttack;







    void Update()
    {
        MoveInput();
        SkillsInput();
    }

    public void MoveInput()
    {
        run = Input.GetKey(runInput);
        jump = Input.GetKey(jumpInput);
    }

    public void SkillsInput()
    {
        basicAttack = Input.GetKey(basicAttackInput);

        skill_1 = Input.GetKey(skill_1_Input);
        skill_2 = Input.GetKey(skill_2_Input);
        skill_3 = Input.GetKey(skill_3_Input);
        skill_4 = Input.GetKey(skill_4_Input);

    }

}
