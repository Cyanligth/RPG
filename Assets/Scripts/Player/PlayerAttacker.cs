using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] int damage;
    [SerializeField, Range(0, 360)] float angle;
    [SerializeField] bool debug;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
    private void OnAttack()
    {
        Attack();
    }
    public void AttackTiming()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders) 
        {
            Vector3 targetDir = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, targetDir) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
                continue;
            IHittable hittable = collider.GetComponent<IHittable>();
            hittable?.TakeHit(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.cyan;
        Vector3 rightDIr = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDIr = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
        Gizmos.DrawRay(transform.position, rightDIr * range);
        Gizmos.DrawRay(transform.position, leftDIr * range);
    }
    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
