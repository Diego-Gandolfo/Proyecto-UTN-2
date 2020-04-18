using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static GameManager GM;
	
	public string EscenaVictoria;
	public string EscenaDerrota;

// Valores Iniciales del Nivel
	public int VidasIniciales;
	public int PuntajeInicial;

// Condiciones de Derrota y Victoria
	public int VidasEsperadas;
	public int PuntajeEsperado;

// Contadores
	public int VidasTotales;
	public int PuntajeTotal;

// Opciones
	public int Dificultad;

	public void Awake()
	{
		if(GM == null)
		{
			GM = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void Update()
	{
		if (PuntajeTotal >= PuntajeEsperado)
		{
			VidasTotales = VidasIniciales;
			PuntajeTotal = PuntajeInicial;
			Application.LoadLevel(EscenaVictoria);
		}
		else if (VidasTotales <= VidasEsperadas)
		{
			VidasTotales = VidasIniciales;
			PuntajeTotal = PuntajeInicial;
			Application.LoadLevel(EscenaDerrota);
		}
	}
}