using UnityEngine;
using System.Collections;

public class ControlAnimaciones : MonoBehaviour 
{
	public Animator AnimatorController;

	void Update () 
	{
		if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S)))
		{
			if (Input.GetKey(KeyCode.LeftShift))
			{
				AnimatorController.SetFloat("Velocidad", 1f);
			}
			else
			{
				AnimatorController.SetFloat("Velocidad", 0.5f);
			}
		}
		else
		{
			AnimatorController.SetFloat("Velocidad", 0f);
		}

		if(Input.GetKey(KeyCode.Mouse0))
		{
			AnimatorController.SetBool("Disparando", true);
		}
		else
		{
			AnimatorController.SetBool("Disparando", false);
		}
	}
}
