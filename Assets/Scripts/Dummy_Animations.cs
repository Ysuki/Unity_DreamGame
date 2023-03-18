using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy_Animations : MonoBehaviour
{


    Dummy_Manager dummy;
    // Start is called before the first frame update
    void Start()
    {
        dummy = GetComponentInParent<Dummy_Manager>();
    }

    // Update is called once per frame
 
    public void BasicAttackEvent()
    {
        dummy.BasicAttackMelee();
        dummy.damaged.Clear();
        dummy.damaged.Add(this.gameObject);
    }
}
