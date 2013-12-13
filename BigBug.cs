using UnityEngine;
using System.Collections;

public class BigBug : MonoBehaviour {
	
	//	33.10
	public GameObject MouthBgRef;
	public GameObject UpperJawRef;
	
	// Use this for initialization
	void Start () {
		Init();
	}
	
	void Init() {
		MouthBgRef.transform.localScale = new Vector3(.75f, 0, 0);
		gameObject.transform.position = new Vector3(
			-20f,
			gameObject.transform.position.y,
			gameObject.transform.position.z
			);
		
		EnterScene();
	}
	
	public void EnterScene() {
		Go.to(gameObject.transform, 1.5f, new GoTweenConfig()
			.position(new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z))
			.setEaseType(GoEaseType.ExpoOut)
			);
		
		//	Invoke("LeaveScene", 3f);
		Invoke("OpenMouth", 1.55f);
	}
	
	public void OpenMouth() {
		
		Go.to(UpperJawRef.transform, 1f, new GoTweenConfig()
			.localPosition(new Vector3(UpperJawRef.transform.localPosition.x, 38.7f, UpperJawRef.transform.localPosition.z))
			.setEaseType(GoEaseType.ElasticOut)
			);
		
		Go.to(MouthBgRef.transform, 1f, new GoTweenConfig()
			.scale(new Vector3(.95f, 1, 1))
			.setEaseType(GoEaseType.ElasticOut)
			);
		
		Invoke("CloseMouth", 3f);
	}
	
	public void CloseMouth() {
		
		Go.to(UpperJawRef.transform, .75f, new GoTweenConfig()
			.localPosition(new Vector3(UpperJawRef.transform.localPosition.x, 6.16f, UpperJawRef.transform.localPosition.z))
			.setEaseType(GoEaseType.BounceOut)
			);
		
		MouthBgRef.transform.localScale = new Vector3(.75f, 0, 0);
		
		Invoke("LeaveScene", .8f);
	}
	
	public void LeaveScene() {
		Go.to(gameObject.transform, 2f, new GoTweenConfig()
			.position(new Vector3(-20f, gameObject.transform.position.y, gameObject.transform.position.z))
			.setEaseType(GoEaseType.ExpoOut)
			);
		
		Invoke("EnterScene", 4f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
