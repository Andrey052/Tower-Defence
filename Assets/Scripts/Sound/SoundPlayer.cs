using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoSingleton<SoundPlayer>
    {
		private AudioSource m_AS;
		[SerializeField] private Sounds m_Sounds;
        [SerializeField] private AudioClip m_BGM;
        
		private new void Awake()
		{
			base.Awake();

			m_AS = GetComponent<AudioSource>();

			Instance.m_AS.clip = m_BGM;
			Instance.m_AS.Play();
		}
		public void Play(Sound sound)
		{
			m_AS.PlayOneShot(m_Sounds[sound]);
		}
	}
}