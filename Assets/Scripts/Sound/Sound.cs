
namespace TowerDefense
{
    public enum Sound
    {
        Arrow = 0,
        ArrowHit = 1,
        EnemyDie = 2,
        EnemyWin = 3,
        PlayerWin = 4,
        PlayerLose = 5,
        BGM = 6,
        BGM2 = 7,
        BGM3 = 8,
        BGM4 = 9,
        BGM5 = 10,
        BGM6 = 11,
        Knight = 12,
        KnightHit = 13,
        Explosion = 14,
        Tower = 15,
        EnterLevel = 16,
        TimeFreeze = 17,
    }
    public static class SoundExtensions
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}