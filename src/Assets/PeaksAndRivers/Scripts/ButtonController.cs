using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required when using Event data.

public class ButtonController : MonoBehaviour
{
    public string ButtonType;
	public GameboardLogic GameLogicObj;
	
	void OnMouseUp(){
		
	}
	
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			if (ButtonType == "Start"){
				GameLogicObj.OnStartGame();
			}
			if (ButtonType == "Reset"){
				GameLogicObj.OnResetGame();
			}
		}
	}
}
