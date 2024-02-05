using UnityEngine;

namespace SpaceShooter
{
    public class StendUp : MonoBehaviour
    {
        private Rigidbody2D m_Rig;
        private SpriteRenderer m_SpriteR;

        private void Start()
        {
            m_Rig = transform.root.GetComponent<Rigidbody2D>();
            m_SpriteR = GetComponent<SpriteRenderer>();
        }
        private void LateUpdate()
        {
            transform.up = Vector2.up;
            var xMotion = m_Rig.velocity.x;
            if (xMotion > 0.01f)
                m_SpriteR.flipX = false;
            else if (xMotion < 0.01f)
                m_SpriteR.flipX = true;
        }
    }
}