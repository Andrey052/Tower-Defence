using UnityEngine;
using SpaceShooter;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        public enum ArmorType { Base = 0, Mage = 1 }
        private static Func<int, TDProjectile.DamageType, int, int>[] ArmorDamageFunctions =
        {
            (int power, TDProjectile.DamageType type, int armor) =>
            {//ArmorType.Base
                switch (type)
                {
                    case TDProjectile.DamageType.Magic: return power;
                    default: return Mathf.Max(power - armor, 1);
                }
            },
            (int power, TDProjectile.DamageType type, int armor) =>
            {//ArmorType.Magic
                if (TDProjectile.DamageType.Base == type)
                    armor = armor / 2;
                return Mathf.Max(power - armor, 1);
            },
        };

        [SerializeField] private int m_damage = 1;
        [SerializeField] private int m_Armor = 1;
        [SerializeField] private int m_gold = 1;
        [SerializeField] private ArmorType m_ArmorType;

        private Destructible m_Destructible;
        private void Awake()
        {
            m_Destructible = GetComponent<Destructible>();
        }

        public event Action OnEnd;
        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }
        public void Use(EnemyAsset asset)
        {
            var m_SpriteR = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            m_SpriteR.color = asset.color;
            m_SpriteR.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            m_SpriteR.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

            GetComponent<SpaceShip>().Use(asset);

            GetComponentInChildren<CircleCollider2D>().radius = asset.radius;

            m_damage = asset.damage;
            m_Armor = asset.armor;
            m_ArmorType = asset.armorType;
            m_gold = asset.gold;
        }
        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_damage);
        }
        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_gold);
        }
        public void TakeDamage(int damage, TDProjectile.DamageType damageType)
        {
            m_Destructible.ApplyDamage(
                ArmorDamageFunctions[(int)m_ArmorType](damage, damageType, m_Armor));
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
        public class EnemyInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                EnemyAsset ea = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
                if (ea)
                {
                    (target as Enemy).Use(ea);
                }
            }
        }    
#endif
}


