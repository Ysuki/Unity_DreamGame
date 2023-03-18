using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [Header("Skills")]

    public GameObject cooldown_1;
    [SerializeField] private KeyCode skill_1_Input;
    public bool skill_1;
    public int skill_1_Cooldown;
    [SerializeField] private KeyCode skill_2_Input;
    public bool skill_2;
    public int skill_2_Cooldown;
    [SerializeField] private KeyCode skill_3_Input;
    public bool skill_3;
    public int skill_3_Cooldown;
    [SerializeField] private KeyCode skill_4_Input;
    public bool skill_4;
    public int skill_4_Cooldown;







    [Header("Other")]
    [SerializeField] public KeyCode basicAttackInput;
    public bool basicAttack;
    [SerializeField] private KeyCode jumpInput;
    public bool jump;
    [SerializeField] private KeyCode runInput;
    public bool run;

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
