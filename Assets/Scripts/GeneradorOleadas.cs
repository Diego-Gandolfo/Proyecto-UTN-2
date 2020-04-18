using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneradorOleadas : MonoBehaviour 
{
	//	Dificultad
	private float ContadorTiempo;
	public AnimationCurve CuervaDificultad;

	//	Prefabs a Spawnear
	public List<GameObject> Prefabs;

	//	SpawnPoints donde Spawnear
	public List<GameObject> SpawnPoints;

	//	Area de Spawn
	public Vector3 AreaMinima = Vector3.zero;
	public Vector3 AreaMaxima = Vector3.zero;

	//	Tiempo de Spawn
	public float TiempoInicial;
	public float TiempoMinimo;
	public float TiempoMaximo;

	//	Cantidad de Oleadas
	public int CantidadOleadas;
	public bool OleadaExponencial;
	private int ContadorOleadas;

	//	Cantidad de NPC por Oleada
	public int CantidadMinima;
	public int CantidadMaxima;
	public bool CantidadExponencial;
	private float CantidadNPC;

	// Spawnear solo si esta el Jugador en el Area
	public bool SoloSiJugadorEnArea;
	public float RadioVision;
	public GameObject Personaje;
	public LayerMask LayerPersonaje;
	private bool JugadorCerca = true;
	private bool Instanciando;

	public List<Transform> CaminoSeguir;

	void Update()
	{
		ContadorTiempo += Time.deltaTime;

		if (SoloSiJugadorEnArea)
		{
			Collider[] PersonajeCercano = Physics.OverlapSphere (transform.position, RadioVision, LayerPersonaje);

			if (PersonajeCercano.Length > 0)
			{
				JugadorCerca = true;
				if (!Instanciando)
				{
					ContadorOleadas = 0;
					ContadorTiempo = 0;
					CantidadNPC = 0;
					Instanciando = true;
					Invoke ("InstanciadorOleada", TiempoInicial);
				}
			}
			else
			{
				Instanciando = false;
				JugadorCerca = false;
			}
		}
		else
		{
			if (!Instanciando)
			{
				Instanciando = true;
				Invoke ("InstanciadorOleada", TiempoInicial);
			}
		}
	}

	void InstanciadorOleada ()
	{
		if (JugadorCerca)
		{
			if (CantidadExponencial)
				CantidadNPC = (float)CantidadMinima + (((float)CantidadMaxima - (float)CantidadMinima) * CuervaDificultad.Evaluate(ContadorTiempo));
			else
				CantidadNPC = Random.Range (CantidadMinima, CantidadMaxima + 1);
		
			Vector3 PosicionAleatoria = SpawnPoints[Random.Range (0, SpawnPoints.Count)].transform.position;

			for (int ContadorNPC = 0; ContadorNPC < CantidadNPC; ContadorNPC++)
			{
				PosicionAleatoria.x += Random.Range (AreaMinima.x, AreaMaxima.x);
				PosicionAleatoria.y += Random.Range (AreaMinima.y, AreaMaxima.y);
				PosicionAleatoria.z += Random.Range (AreaMinima.z, AreaMaxima.z);

				GameObject NPC = (GameObject) Instantiate (Prefabs[Random.Range (0, Prefabs.Count)], PosicionAleatoria, transform.rotation);

				MovimientoNPC_MapMesh MovNPC_MapMesh = NPC.GetComponent<MovimientoNPC_MapMesh>();

				if (SoloSiJugadorEnArea)
				{
					MovNPC_MapMesh.Patrullar = false;
					MovNPC_MapMesh.Personaje = Personaje;
				}
				else
				{
					MovNPC_MapMesh.Patrullar = true;
					MovNPC_MapMesh.Personaje = Personaje;
					
					for (int i = 0; i < 10; i++)
					{	// Esto solo funciona porque son el mismo numero de Postas
						MovNPC_MapMesh.Camino[i] = CaminoSeguir[i];
					}
				}

				DisparadorInstaneo DispInst = NPC.GetComponentInChildren<DisparadorInstaneo>();

				DispInst.Personaje = Personaje;
			}
			
			ContadorOleadas++;

			if (ContadorOleadas < CantidadOleadas)
			{
				if (OleadaExponencial)
					Invoke ("InstanciadorOleada", TiempoMaximo - ((TiempoMaximo - TiempoMinimo) * CuervaDificultad.Evaluate(ContadorTiempo)));
				else
					Invoke ("InstanciadorOleada", Random.Range (TiempoMinimo, TiempoMaximo));
			}
		}
	}
}
