using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var HealthController = collision.gameObject.GetComponent<HealthController>();

            HealthController.TakeDamage(_damageAmount);
        }
    }
}
