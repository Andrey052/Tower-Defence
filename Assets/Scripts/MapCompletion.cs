using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{
    public class MapCompletion : MonoSingleton<MapCompletion>
    {
        public const string fileName = "completion.dat";
        
        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }
        [SerializeField] private EpisodeScore[] completionData;
        //[SerializeField] private int totalScore;
        //[SerializeField] private EpisodeScore[] brunchComplitionData;
        public int TotalScore { private set; get; }

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(fileName, ref completionData);
            foreach (var episodeScore in completionData)
            {
                TotalScore += episodeScore.score;
            }
        }
        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                foreach (var item in Instance.completionData)
                { // Сохранение новых очков прохождения
                    if (item.episode == LevelSequenceController.Instance.CurrentEpisode)
                    {
                        if (levelScore > item.score)
                        {
                            Instance.TotalScore += levelScore - item.score;
                            item.score = levelScore;
                            Saver<EpisodeScore[]>.Save(fileName, Instance.completionData);
                        }
                    }
                }
            }
            else
            {
                Debug.Log($"Episode completed with score {levelScore}");
            }
        }        
        public int GetEpisodeScore(Episode m_episode)
        {
            foreach (var data in completionData)
            {
                if (data.episode == m_episode)
                    return data.score;
            }
            return 0;
        }
    }
}