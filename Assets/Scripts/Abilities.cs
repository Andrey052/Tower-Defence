using UnityEngine;
using System;
using SpaceShooter;
using System.Collections;
using UnityEngine.UI;

namespace TowerDefense
{
    public class Abilities : MonoSingleton<Abilities>
    {        
        [Serializable]
        public class FireAbility
        {
            [SerializeField] private int m_Cost = 5;
            [SerializeField] private int m_Damage = 2;
            [SerializeField] private Color m_TargetingColor;
            [SerializeField] private UpgradeAsset requiredUpgrade;
            [SerializeField] private Button m_UseFireButton;

            public void CheckAbility(int money)
            {
                m_UseFireButton.interactable = money >= m_Cost && CanUse();
            }
            public bool CanUse() // Проверка, доступно ли умение
            {
                return Upgrades.GetUpgradeLevel(requiredUpgrade) > 0;
            }
            public void Use()
            {
                if (!CanUse()) return;

                //CheckAbility(TDPlayer.Instance.Mana);
                if (TDPlayer.Instance.Mana >= m_Cost)
                {
                    TDPlayer.Instance.ChangeMana(-m_Cost);
                }

                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, 3))
                    {
                        if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Magic);
                        }
                    }
                });
            }
        }
        [Serializable]
        public class TimeAbility
        {
            [SerializeField] private int m_Cost = 20;
            [SerializeField] private float m_CoolDown = 10f;
            [SerializeField] private float m_Duration = 5;
            [SerializeField] private UpgradeAsset requiredUpgrade;
            [SerializeField] private Button m_UseSlowButton;
            public void CheckAbility(int money)
            {
                m_UseSlowButton.interactable = money >= m_Cost && CanUse();
            }
            public bool CanUse() // Проверка, доступно ли умение
            {
                return Upgrades.GetUpgradeLevel(requiredUpgrade) > 0;
            }
            public void Use()
            {
                if (!CanUse()) return;
                if (TDPlayer.Instance.Mana >= m_Cost)
                {
                    TDPlayer.Instance.ChangeMana(-m_Cost);
                }
                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }
                foreach (var ship in FindObjectsOfType<SpaceShip>())
                    ship.HalfMaxLinearVelocity();

                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.RestoreMaxLinearVelocity();
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }
                Instance.StartCoroutine(Restore());

                IEnumerator TimeAbilityButton()
                {
                    Instance.m_TimeButton.interactable = false;
                    yield return new WaitForSeconds(m_CoolDown);
                    Instance.m_TimeButton.interactable = true;
                }
                Instance.StartCoroutine(TimeAbilityButton());
            }
        }
        
        [SerializeField] private Image m_TargetingCircle;

        [SerializeField] private Button m_TimeButton;
        
        [SerializeField] private Sound m_FreezeSound = Sound.TimeFreeze;

        [SerializeField] private FireAbility m_FireAbility;
        public void UseFireAbility() => m_FireAbility.Use();

        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();
        public void UseFireCheckAbility() => m_FireAbility.CheckAbility(TDPlayer.Instance.Mana);
        public void UseSlowCheckAbility() => m_TimeAbility.CheckAbility(TDPlayer.Instance.Mana);
        private void Start()
        {
            UseFireCheckAbility();
            UseSlowCheckAbility();
        }
    }
}