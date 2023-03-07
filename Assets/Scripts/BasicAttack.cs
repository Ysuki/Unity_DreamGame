using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{

    HashSet<GameObject> damaged = new HashSet<GameObject>();

    [SerializeField] bool canAttack = true;
    [SerializeField] bool drawn;
    [SerializeField] float gizmoSize;
    [SerializeField] Color color;
    [SerializeField] Color hitColor;
    Color originalColor;
    [SerializeField] Transform autoAttPos;

    Statistics statistics;

    private void OnDrawGizmos()
    {
        if (drawn)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(autoAttPos.position, gizmoSize);
            Gizmos.color = color * new Color(0.5f, 0, 0, 2);
            Gizmos.DrawWireSphere(autoAttPos.position, gizmoSize);
        }
    }
    private void Start()
    {
        originalColor = color;
        statistics = GetComponent<Statistics>();
        damaged.Add(this.gameObject);

    }
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (canAttack)
            {
                BasicAttackMelee();
            }
        }
    }

    private void BasicAttackMelee()
    {
        color = hitColor;
        canAttack = false;
        Collider[] colliders = Physics.OverlapSphere(autoAttPos.position, gizmoSize);
        StartCoroutine(ResetAttackCooldown());

        foreach (Collider c in colliders)
        {
            if (c.gameObject.GetComponentInParent<Statistics>() != null && !damaged.Contains(c.gameObject))
            {
                Statistics currentStats = c.gameObject.GetComponentInParent<Statistics>();
                bool yourTeam;
                if (statistics.thisIndex[1] != 'N')
                {

                    yourTeam = currentStats.thisIndex[1] == statistics.thisIndex[1];
                }
                else
                {
                    yourTeam = false;

                }

                if (currentStats.thisIndex != statistics.thisIndex && yourTeam == false)
                {
                    currentStats.TakeDamage(statistics.physicalDamage, DamageType.Physical_Damage);
                    damaged.Add(c.gameObject); ;

                }
            }
        }
    }
    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(statistics.attackSpeed);
        canAttack = true;
        damaged.Clear();
        damaged.Add(this.gameObject);
        color = originalColor;

    }
}
