using System.Collections;
using UnityEngine;

namespace Car
{
	public class WheelEffects : MonoBehaviour
	{
		public GameObject SkidTrailPrefab;

		public static Transform skidTrailsDetachedParent;

		public ParticleSystem skidParticles;

		private Transform m_SkidTrail;

		private WheelCollider m_WheelCollider;

		public bool skidding { get; private set; }

		public bool PlayingAudio { get; private set; }

		private void Start()
		{
			skidParticles = base.transform.root.GetComponentInChildren<ParticleSystem>();
			if (!(skidParticles == null))
			{
				skidParticles.Stop();
			}
			m_WheelCollider = GetComponent<WheelCollider>();
			PlayingAudio = false;
			if (skidTrailsDetachedParent == null)
			{
				skidTrailsDetachedParent = new GameObject("Skid Trails - Detached").transform;
			}
		}

		public void EmitTyreSmoke()
		{
			skidParticles.transform.position = base.transform.position - base.transform.up * m_WheelCollider.radius;
			skidParticles.Emit(1);
			if (!skidding)
			{
				StartCoroutine(StartSkidTrail());
			}
		}

		public IEnumerator StartSkidTrail()
		{
			skidding = true;
			m_SkidTrail = Object.Instantiate(SkidTrailPrefab).transform;
			while (m_SkidTrail == null)
			{
				yield return null;
			}
			m_SkidTrail.parent = base.transform;
			m_SkidTrail.localPosition = -Vector3.up * m_WheelCollider.radius;
		}

		public void EndSkidTrail()
		{
			if (skidding)
			{
				skidding = false;
				m_SkidTrail.parent = skidTrailsDetachedParent;
				Object.Destroy(m_SkidTrail.gameObject, 10f);
			}
		}
	}
}
