using SpaceShooter;
using UnityEngine;

namespace TowerDefense
{
    public class TDProjectile : Projectile
    {
        public enum DamageType { Base, Magic }
        [SerializeField] private DamageType m_DamageType;

        [SerializeField] private Sound m_ShotSound = Sound.Arrow;
        [SerializeField] private Sound m_HitSound = Sound.ArrowHit;

        private void Awake()
        {
            m_ShotSound.Play();
        }

        protected override void OnHit(RaycastHit2D hit)
        {
            var enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(m_Damage, m_DamageType);
                m_HitSound.Play();
            }
        }
    }
}