using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public Transform m_enemyPrefab;
	public Transform m_spawnPoint;

	public Text m_waveCountDownText;
	
	public float m_timeBetweenWares = 5f;
	private float m_countDown = 2f;

	private int m_waveNumber = 0;

	private void Update()
	{
		if (m_countDown <= 0f)
		{
			StartCoroutine(SpawnWave());
			m_countDown = m_timeBetweenWares;
		}

		m_countDown -= Time.deltaTime;
		m_waveCountDownText.text = Mathf.Round(m_countDown).ToString();
		
	}

	IEnumerator SpawnWave()
	{
		m_waveNumber++;
		
		for (int i = 0; i < m_waveNumber; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(0.5f);
		}
	}

	private void SpawnEnemy()
	{
		Instantiate(m_enemyPrefab, m_spawnPoint.position, m_spawnPoint.rotation);
	}
}
