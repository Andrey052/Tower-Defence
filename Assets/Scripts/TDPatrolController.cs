using SpaceShooter;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    public class TDPatrolController : AIController
    {
        private Path m_path;
        private int m_PathIndex;
        [SerializeField] private UnityEvent OnEndPath;
        public void SetPath(Path newpath)
        {
            m_path = newpath;
            m_PathIndex = 0;
            SetPatrolBehaviour(m_path[m_PathIndex]);
        }
        protected override void GetNewPoint()
        {            
            if (m_path.Length > ++ m_PathIndex)
            {
                SetPatrolBehaviour(m_path[m_PathIndex]);
            }
            else
            {
                OnEndPath.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
