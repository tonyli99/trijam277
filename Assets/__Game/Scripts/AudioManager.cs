using UnityEngine;

namespace Game
{

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        public AudioClip explodeSound;
        public AudioClip pickupSound;
        public AudioClip landSound;
        public AudioClip buySellSound;

        public AudioSource shootAudioSource;

        private void Awake()
        {
            Instance = this;
        }

        public void Shoot()
        {
            shootAudioSource.Stop();
            shootAudioSource.Play();
        }

        public void Explode() { AudioSource.PlayClipAtPoint(explodeSound, Camera.main.transform.position);  }
        public void Pickup() { AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position); }
        public void Land() { AudioSource.PlayClipAtPoint(landSound, Camera.main.transform.position); }
        public void ChaChing() { AudioSource.PlayClipAtPoint(buySellSound, Camera.main.transform.position); }
    }
}
