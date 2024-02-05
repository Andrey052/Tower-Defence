using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private CircleArea startArea;
        public CircleArea StartArea { get { return startArea; } }

        [SerializeField] private AIPointPatrol[] m_Points;
        public int Length { get => m_Points.Length; }
        public AIPointPatrol this[int i] { get => m_Points[i]; }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            foreach (var point in m_Points)
                Gizmos.DrawSphere(point.transform.position, point.Radius);

            Gizmos.color = Color.green;
            for (int i = 0; i < m_Points.Length - 1; i++)
            {
                Gizmos.DrawLine(m_Points[i].transform.position, m_Points[i + 1].transform.position);
            }
        }
    }
}
