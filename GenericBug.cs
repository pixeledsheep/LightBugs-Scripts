using UnityEngine;
using System.Collections;

public class GenericBug : MonoBehaviour {
	
	public Vector3 MyNewPosXY;
	
	// Use this for initialization
	void Start () {
		Invoke("GetNewPosXY", 2f);
	}
	
	public void GetNewPosXY() {
		MyNewPosXY = new Vector3(
			Random.Range(-1200f, 1200f) / 100f,
			Random.Range(-1000f, 1000f) / 100f,
			gameObject.transform.position.z
		);
		
		MoveMeTo();
	}
	
	public void MoveMeTo() {
		
		float _walkingTime = Random.Range(1000f, 3000f) / 1000f;
		
		Go.to(gameObject.transform, _walkingTime, new GoTweenConfig()
			.position(MyNewPosXY)
			.setEaseType(GoEaseType.QuadInOut)
			);
		
		gameObject.transform.LookAt(MyNewPosXY);
		Invoke("GetNewPosXY", _walkingTime + .15f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
