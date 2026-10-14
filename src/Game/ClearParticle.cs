using UnityEngine;

public class ClearParticle : MonoBehaviour
{
	private ParticleSystem particleSystem;

	private ParticleSystem.Particle[] particles;

	private void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		particles = new ParticleSystem.Particle[particleSystem.particleCount];
		particleSystem.GetParticles(particles);
		for (int i = 0; i < particles.Length; i++)
		{
			if (Physics.CheckSphere(base.transform.localToWorldMatrix.MultiplyPoint(particles[i].position), 0.04f, LayerMask.GetMask("RainBrush")))
			{
				particles[i].remainingLifetime = 0f;
			}
		}
		particleSystem.SetParticles(particles, particles.Length);
	}
}
