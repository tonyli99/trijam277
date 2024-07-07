using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{

    public class Starfield : MonoBehaviour
    {
        [field: SerializeField] private int MaxStars { get; set; } = 100;
        [field: SerializeField] private float StarSize { get; set; } = 0.1f;
        [field: SerializeField] private float StarSizeRange { get; set; } = 0.5f;
        [field: SerializeField] private float FieldWidth { get; set; } = 20f;
        [field: SerializeField] private float FieldHeight { get; set; } = 25f;
        [field: SerializeField] private float ParallaxFactor { get; set; } = 0f;
        [field: SerializeField] private bool Colorize { get; set; } = false;

        float xOffset;
        float yOffset;

        ParticleSystem Particles;
        ParticleSystem.Particle[] Stars;
        Transform theCamera;


        void Awake()
        {
            theCamera = Camera.main.transform;
            Stars = new ParticleSystem.Particle[MaxStars];
            Particles = GetComponent<ParticleSystem>();

            Assert.IsNotNull(Particles, "Particle system missing from object!");

            xOffset = FieldWidth * 0.5f;                                                                                                        // Offset the coordinates to distribute the spread
            yOffset = FieldHeight * 0.5f;                                                                                                       // around the object's center

            for (int i = 0; i < MaxStars; i++)
            {
                float randSize = Random.Range(1f - StarSizeRange, StarSizeRange + 1f);          // Randomize star size within parameters
                float scaledColor = (true == Colorize) ? randSize - StarSizeRange : 1f;         // If coloration is desired, color based on size

                Stars[i].position = GetRandomInRectangle(FieldWidth, FieldHeight) + transform.position;
                Stars[i].startSize = StarSize * randSize;
                Stars[i].startColor = new Color(1f, scaledColor, scaledColor, 1f);
            }
            Particles.SetParticles(Stars, Stars.Length);                                                                // Write data to the particle system
        }

        void Update()
        {
            for (int i = 0; i < MaxStars; i++)
            {
                Vector3 pos = Stars[i].position + transform.position;

                if (pos.x < (theCamera.position.x - xOffset))
                {
                    pos.x += FieldWidth;
                }
                else if (pos.x > (theCamera.position.x + xOffset))
                {
                    pos.x -= FieldWidth;
                }

                if (pos.y < (theCamera.position.y - yOffset))
                {
                    pos.y += FieldHeight;
                }
                else if (pos.y > (theCamera.position.y + yOffset))
                {
                    pos.y -= FieldHeight;
                }

                Stars[i].position = pos - transform.position;
            }
            Particles.SetParticles(Stars, Stars.Length);

            Vector3 newPos = theCamera.position * ParallaxFactor;                                                       // Caculate the position of the object
            newPos.z = 0;                                                                                                                                       // Force Z-axis to zero, since we're in 2D
            transform.position = newPos;

        }

        // GetRandomInRectangle
        //----------------------------------------------------------
        // Get a random value within a certain rectangle area
        //
        Vector3 GetRandomInRectangle(float width, float height)
        {
            float x = Random.Range(0, width);
            float y = Random.Range(0, height);
            return new Vector3(x - xOffset, y - yOffset, 0);
        }
    }

}
