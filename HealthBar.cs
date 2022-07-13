using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public float maxValue = 20;
	Color color = Color.red;
	public Slider slider;

	private static float curHealth;

	void Start()
	{
		slider.fillRect.GetComponent<Image>().color = color;

		slider.maxValue = maxValue;
		slider.minValue = 0;
		curHealth = maxValue;
	}

	public static float currentValue
	{
		get { return curHealth; }
	}

	void Update()
	{
		if (curHealth < 0) { curHealth = 0; }
		if (curHealth > maxValue) { curHealth = maxValue; }
		slider.value = curHealth;
	}

	public void PlayerHealth(float _health)
	{
		curHealth += _health;
	}
}