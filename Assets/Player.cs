using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Transform m_target;
	
	[Header("Attrbutes")]
	
	[Range(5, 15)]
	public float m_range = 15f;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Unity Setup Fields")]
	
	public Transform m_partToRotate;
	public float turnSpeed = 10f;

	public GameObject m_bulletPrefab;
	public Transform m_firePoint;
	
	private void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		var enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (var enemy in enemies)
		{
			var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= m_range)
		{
			m_target = nearestEnemy.transform;
		}
		else
		{
			m_target = null;
		}
	}

	private void Update()
	{
		if (m_target == null)
		{
			return;
		}
		
		//Target Lock On
		// var dir = m_target.position - transform.position;
		// var lookRotation = Quaternion.LookRotation(dir);
		// var rotation = Quaternion.Lerp(m_partToRotate.rotation,lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		// m_partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / fireRate;
		}

		fireCountdown -= Time.deltaTime;

	}

	void Shoot()
	{
		var bulletGO = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
		var bUllet = bulletGO.GetComponent<BUllet>();

		if (bUllet != null)
		{
			bUllet.Seek(m_target);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, m_range);
	}
}
