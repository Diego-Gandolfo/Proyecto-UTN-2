using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetonadorBomba : MonoBehaviour 
{
	public GameObject Personaje;
	public GameObject goTextoDetonar;
	public GameObject goTextoCorrer;

	public float TiempoDetonacion;
	public float CantidadOndas;
	public float RadioOnda;
	public float DañoPorOnda;
	public LayerMask LayerPersonaje;
	private float RadioActual;

	void OnTriggerEnter(Collider ObjetoColisionado)
	{
		if (ObjetoColisionado.name == "Personaje")
		{
			goTextoDetonar.SetActive(true);
			goTextoCorrer.SetActive(false);
		}
	}

	void OnTriggerStay(Collider ObjetoColisionado)
	{
		if ((ObjetoColisionado.name == "Personaje")&&(Input.GetKey(KeyCode.B)))
		{
			goTextoDetonar.SetActive(false);
			goTextoCorrer.SetActive(true);
			Invoke("Detonar", TiempoDetonacion);
		}
	}

	void OnTriggerExit(Collider ObjetoColisionado)
	{
		if (ObjetoColisionado.name == "Personaje")
		{
			goTextoDetonar.SetActive(false);
			goTextoCorrer.SetActive(false);
		}
	}

	public void Detonar()
	{
		RadioActual = 0;

		for (int i = 0; i > CantidadOndas; i++)
		{
			RadioActual += RadioOnda;
			Collider[] PersonajeCercano = Physics.OverlapSphere (transform.position, RadioActual, LayerPersonaje);

			if (PersonajeCercano.Length > 0)
			{
				Vida VidaPJ = Personaje.GetComponent<Vida>();
				VidaPJ.VidaActual -= DañoPorOnda;
				print ("Detonar");
			}
		}

		GameManager.GM.PuntajeTotal = 500;
	}
}
