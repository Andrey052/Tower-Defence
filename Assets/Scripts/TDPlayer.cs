using SpaceShooter;
using UnityEngine;
using System;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        [SerializeField] private UpgradeAsset healthUpgrade;
        public static new TDPlayer Instance
        { 
            get
            {
                return Player.Instance as TDPlayer; 
            }            
        }
        private event Action<int> OnGoldUpdate;
        private void Start()
        {            
            var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            TakeDamage(-level * 5);
        }
        public void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }
        public void GoldUpdateUnSubscribe(Action<int> act)
        {
            OnGoldUpdate -= act;
        }
        public void LifeUpdateUnSubscribe(Action<int> act)
        {
            OnLifeUpdate -= act;
        }
        public void ManaUpdateUnSubscribe(Action<int> act)
        {
            OnManaUpdate -= act;
        }

        public event Action<int> OnLifeUpdate;
        public void LifeUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }        
        private event Action<int> OnManaUpdate;
        public void ManaUpdateSubscribe(Action<int> act)
        {
            OnManaUpdate += act;
            act(Instance.m_Mana);
        }
        [SerializeField] private int m_gold = 0;
        public int Gold => m_gold;
        [SerializeField] private int m_Mana = 0;
        public int Mana => m_Mana;
               
        public void ChangeGold(int change)
        {
            m_gold += change;
            OnGoldUpdate(m_gold);
        }
        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }
        public void ChangeMana(int change)
        {
            m_Mana += change;
            OnManaUpdate(m_Mana);
        }
        //[SerializeField] private Tower m_towerPrefab;
        public void TryBuild(TowerAsset m_towerAsset, Transform buildSite)
        {
            ChangeGold(-m_towerAsset.goldCost);
            var tower = Instantiate(m_towerAsset.TowerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(m_towerAsset);

            Destroy(buildSite.gameObject);
        }       
    }
}
