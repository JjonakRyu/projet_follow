using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private float m_minSpeed = 1f;
    [SerializeField] private float m_maxSpeed = 10f;
    [SerializeField] private float m_minDistance = 2.5f;
    [SerializeField] private float m_maxDistance = 10f;
    [SerializeField] private AnimationCurve m_speedCurve;

    [Header("Rotation")]
    [SerializeField] private float m_rotationSpeed;
    void Start()
    {

    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, m_target.position);
        if (distance > m_minDistance)
        {
            float distanceRatio = Remap(distance, m_minDistance, m_maxDistance, 0, 1);
            Debug.Log("test" + distanceRatio);
            distanceRatio = Mathf.Clamp(distanceRatio, 0, 1);

            float speedRatio = m_speedCurve.Evaluate(distanceRatio);
            float wantedSpeed = Remap(speedRatio, 0, 1, m_minSpeed, m_maxSpeed);

            transform.position = Vector3.MoveTowards(transform.position, m_target.position, wantedSpeed * Time.deltaTime);

            // Rotation

            Vector3 direction = (m_target.position - transform.position).normalized;
            transform.forward = Vector3.MoveTowards(transform.forward, direction, m_rotationSpeed * Time.deltaTime);
        }
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_target.position, m_maxDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_target.position, m_minDistance);
    }
}