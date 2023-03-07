using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DamageType { True_Damage, Physical_Damage, Magic_Damage };
public enum CharacterClass { None, Warrior, Tank, Suport, Assasin, Mage };
public class SceneManager : MonoBehaviour
{
    public string[] scenePlayersIds = new string[] { };
    public void AddScenePlayersIds(string index)
    {
        List<string> myList = new List<string>(scenePlayersIds);
        myList.Add(index);
        scenePlayersIds = myList.ToArray();
    }
}
