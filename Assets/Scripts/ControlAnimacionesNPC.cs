using UnityEngine;
using System.Collections;

public class ControlAnimacionesNPC : MonoBehaviour 
{
	public Animator AnimatorController;
	
	void Update () 
	{
		NavMeshAgent nma = GetComponent<NavMeshAgent> ();
		MovimientoNPC_MapMesh MovNPC = GetComponent<MovimientoNPC_MapMesh> ();
		DisparadorInstaneo DispInst = GetComponentInChildren<DisparadorInstaneo> ();

		if (Mathf.Approximately(nma.speed, MovNPC.VelocidadCaminar))
			AnimatorController.SetFloat("Velocidad", 0.5f);
		if (Mathf.Approximately(nma.speed, MovNPC.VelocidadCorrer))
			AnimatorController.SetFloat("Velocidad", 1f);
		if (Mathf.Approximately(nma.speed, 0))
			AnimatorController.SetFloat("Velocidad", 0f);

		if (DispInst.Disparando)
			AnimatorController.SetBool("Disparando", true);
		else
			AnimatorController.SetBool("Disparando", false);
	}
}
