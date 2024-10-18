using System.Collections;
using UnityEngine;

public class CanonController : MonoBehaviour
{

    [SerializeField] private ProjectileController m_projectilePrefab;
    [SerializeField] private Vector2 m_cooldownRange;
    [SerializeField] private Transform m_shootPosition;


    private void Start()
    {
        StartCoroutine(C_Shoot());
    }

    private IEnumerator C_Shoot()
    {
        float cooldown = Random.Range(m_cooldownRange.x, m_cooldownRange.y);
        yield return new WaitForSeconds(cooldown);
    }

    private void ShootProjectile()
    {
        ProjectileController newProjectile = CreateProjectile();

    }

}
