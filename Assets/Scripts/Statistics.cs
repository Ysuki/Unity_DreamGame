using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class Statistics : MonoBehaviour
{



    [Header("Informações")]
    public string thisName = "None";
    [Space(5)]
    [SerializeField] private Class whatThis;
    [Space(5)]
    [SerializeField] private Team thisTeam;
    [Space(5)]
    public string thisIndex = "I'M-TEAM-NAME-INDEX";
    SceneManager sceneManager;

    [Space(20)]
    [Header("Estatisticas Basicas")]

    [Space(5)]
    [Header("Vida/Mana")]
    public int health = 0;
    public int maxHealth;
    public int healthRegen;
    [Space(5)]
    public int mana = 0;
    public int maxMana;
    public int manaRegen;


    [Space(5)]
    [Header("Resistencias")]
    public int armor;
    public int magicResistance;

    [Space(5)]
    [Header("Combate")]
    public int physicalDamage;
    public int magicDamage;
    public float attackSpeed;

    [Space(5)]
    [Header("Locomoção")]
    public float movementSpeed;
    public float MaxSpeed;
    public float Jump;


    private void Start()
    {
        sceneManager = GameObject.FindWithTag("SceneManager").gameObject.GetComponent<SceneManager>();
        thisIndex = GenerateIndex(whatThis, thisTeam);
        sceneManager.AddScenePlayersIds(thisIndex);
        health = maxHealth;
        mana = maxMana;
        InvokeRepeating("Regeneration", 0.0f, 0.85f);
    }
    private void Update()
    {
        if (armor < 0)
        {
            armor = 0;
        }
        if (magicResistance < 0)
        {
            magicResistance = 0;
        }
    }
    private void Regeneration()
    {
        if (health < maxHealth)
        {
            health += healthRegen;
        }
        else
        {
            health = maxHealth;
        }
        if (mana < maxMana)
        {
            mana += manaRegen;
        }
        else
        {
            mana = maxMana;
        }
    }
    public void TakeDamage(int damage, DamageType dType, string name)
    {
        float finalDamage = 0;
        float resistance = 0;
        switch (dType)
        {
            case DamageType.True_Damage:
                finalDamage = damage;
                break;
            case DamageType.Physical_Damage:
                if (armor < 0)
                {
                    armor = 0;
                }
                resistance = armor + 100;
                resistance = 100 / resistance;
                finalDamage = damage * resistance;
                break;
            case DamageType.Magic_Damage:
                if (magicResistance < 0)
                {
                    magicResistance = 0;
                }
                resistance = magicResistance + 100;
                resistance = 100 / resistance;
                finalDamage = damage * resistance;
                break;
        }
        if (finalDamage <= 1)
        {
            finalDamage = 1.0f;
        }
        health -= Mathf.RoundToInt(finalDamage);
        print($"{thisName} sofreu {Mathf.RoundToInt(finalDamage)} de {dType} de {name}");
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    string GenerateIndex(Class whatThis, Team thisTeam)
    {
        string w = whatThis.ToString();
        string t = thisTeam.ToString();
        string index;
        index = $"{w[0]}{t[0]}-{thisName}-";
        for (int i = 0; i < 4; i++)
        {
            int a = Random.Range(0, 9);
            index = index + a;
        }
        if (!sceneManager.scenePlayersIds.Contains(index))
        {
            return index;
        }
        else
        {
            Debug.Log("ESTE INDEX COINCIDIU COM OUTRO, ESTOU CORRIGINDO ISSO...");
            return GenerateIndex(whatThis, thisTeam);
        }
    }
}