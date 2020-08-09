using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public string DiceState;
	float DiceAngle1;
	float DiceAngle2;
	
	// Start is called before the first frame update
    void Start()
    {
        DiceState = "Display1";
    }
	
	
    // Update is called once per frame
    void FixedUpdate()
    {
        switch (DiceState){
			case "StartSpin":
			DiceAngle1 = transform.localEulerAngles.x;
			DiceAngle2 = transform.localEulerAngles.y;
			DiceState = "Spin";
			break;
			case "Spin":
			DiceAngle1 -= Time.deltaTime * 1200;
			DiceAngle2 += Time.deltaTime * 900;
			transform.localEulerAngles = new Vector3(DiceAngle1,DiceAngle2,0f);
			break;
			case "Display1":
			transform.localEulerAngles = new Vector3(0f,180f,0f);
			DiceState = "DisplayWait";
			break;
			case "Display2":
			transform.localEulerAngles = new Vector3(0f,270,270f);
			DiceState = "DisplayWait";
			break;
			case "Display3":
			transform.localEulerAngles = new Vector3(0f,270,0f);
			DiceState = "DisplayWait";
			break;
			case "Display4":
			transform.localEulerAngles = new Vector3(0f,90f,0f);
			DiceState = "DisplayWait";
			break;
			case "Display5":
			transform.localEulerAngles = new Vector3(0f,270,90f);
			DiceState = "DisplayWait";
			break;
			case "Display6":
			transform.localEulerAngles = new Vector3(0f,0f,0f);
			DiceState = "DisplayWait";
			break;
			case "DisplayWait":
			break;
		}
    }
}
