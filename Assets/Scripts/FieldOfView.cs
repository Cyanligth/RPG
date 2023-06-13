using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField, Range(0, 360)] float angle;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    private float aaa;
    private void Awake()
    {
        aaa = Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }

    private void Update()
    {
        FindTarget();   
    }

    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);
        foreach (Collider collider in colliders)
        {
            Vector3 targetDir = (collider.transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, targetDir) < aaa)
                continue;
            float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
            if (Physics.Raycast(transform.position, targetDir, distanceToTarget, obstacleMask))
                continue;

            Debug.DrawRay(transform.position, (targetDir) * distanceToTarget, Color.yellow);
        }
    }
    private void OnDrawGizmosSelected()
    {
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
