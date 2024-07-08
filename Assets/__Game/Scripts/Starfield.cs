using UnityEngine;

namespace Game
{

    public class Starfield : MonoBehaviour
    {
        [field: SerializeField] private float Width { get; set; } = 40f;
        [field: SerializeField] private float Height { get; set; } = 45f;
        [field: SerializeField] private float Depth { get; set; } = 0f;
        [field: SerializeField] private int NumStars { get; set; } = 100;
        [field: SerializeField] private float MinSize { get; set; } = 0.05f;
        [field: SerializeField] private float MaxSize { get; set; } = 0.2f;

        private Transform myTransform;
        private Transform mainCameraTransform;
        private ParticleSystem myParticleSystem;
        private ParticleSystem.Particle[] particles;
        private Vector2 offset;

        private void Awake()
        {
            myTransform = transform;
            var pos = myTransform.position;
            mainCameraTransform = Camera.main.transform;
            offset = new Vector2(Width / 2, Height / 2);
            myParticleSystem = GetComponent<ParticleSystem>();
            particles = new ParticleSystem.Particle[NumStars];
            for (int i = 0; i < NumStars; i++)
            {
                particles[i].position = new Vector3(
                    pos.x + Random.value * Width - offset.x,
                    pos.y + Random.value * Height - offset.y,
                    pos.z);
                particles[i].startSize = Random.Range(MinSize, MaxSize);
                particles[i].startColor = Color.white;
            }
            myParticleSystem.SetParticles(particles, particles.Length);                                                                // Write data to the particle system
        }

        private void Update()
        {
            for (int i = 0; i < NumStars; i++)
            {
                var pos = particles[i].position + myTransform.position;

                if (pos.x < (mainCameraTransform.position.x - offset.x))
                {
                    pos.x += Width;
                }
                else if (pos.x > (mainCameraTransform.position.x + offset.x))
                {
                    pos.x -= Width;
                }

                if (pos.y < (mainCameraTransform.position.y - offset.y))
                {
                    pos.y += Height;
                }
                else if (pos.y > (mainCameraTransform.position.y + offset.y))
                {
                    pos.y -= Height;
                }

                particles[i].position = pos - myTransform.position;
            }
            myParticleSystem.SetParticles(particles, particles.Length);

            Vector3 newPos = mainCameraTransform.position * Depth;                                                       // Caculate the position of the object
            newPos.z = 0;                                                                                                                                       // Force Z-axis to zero, since we're in 2D
            myTransform.position = newPos;

        }

    }

}
