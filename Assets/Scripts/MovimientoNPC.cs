using UnityEngine;
using System.Collections;

public class MovimientoNPC : MonoBehaviour 
{
	public float Gravedad;
	private float FuerzaGravedad;
	private float EnElPiso;
	public float velocidad;

	void Awake()
	{
		FuerzaGravedad = -Gravedad;
	}

	void Update()
	{
		transform.Translate (0, 0, velocidad * Time.deltaTime);
	}

	void FixedUpdate ()
	{
		transform.Translate (0, FuerzaGravedad * Time.deltaTime, 0);

		if(EnElPiso < 1)
			FuerzaGravedad -= Gravedad * Time.deltaTime;

		if(EnElPiso >= 1)
		{
			FuerzaGravedad = 0;
		}
	}

	void OnCollisionEnter(Collision ObjCol)
	{
		if (ObjCol.gameObject.name == "Piso")
		{
			EnElPiso++;
		}
	}

	void OnCollisionExit(Collision ObjCol)
	{
		if (ObjCol.gameObject.name == "Piso")
		{
			EnElPiso--;
		}
	}
}
