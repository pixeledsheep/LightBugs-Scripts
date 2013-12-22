using UnityEngine;
using System.Collections;

public class BigBug : MonoBehaviour {
	
	//	UpperJaw: 6.16, open: 38.8
	public GameObject MouthBgRef;
	public GameObject UpperJawRef;
	public GameObject LowerJawRef;
	
	public float JawDifY;
	
	public Vector2 CameraZoom {get; set;}
	public Vector2 CameraPosXY;
	
	// Use this for initialization
	void Start () {
		Init();
	}
	
	/// <summary>
	/// Init this instance.
	/// </summary>
	void Init() {
		MouthBgRef.transform.localScale = new Vector3(.75f, 0, 0);
		gameObject.transform.position = new Vector3(
			-20f,
			gameObject.transform.position.y,
			gameObject.transform.position.z
			);
		
		CameraZoom = new Vector2(11.6f, 0);
		CameraPosXY = new Vector2(1.55f, 0);
		
		EnterScene();
	}
	
	/// <summary>
	/// Playing BUG Enters the scene.
	/// </summary>
	public void EnterScene() {
		Go.to(gameObject.transform, 1.5f, new GoTweenConfig()
			.position(new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z))
			.setEaseType(GoEaseType.ExpoOut)
			);
		
		SetCameraZoom(9.25f, 1.75f, 1.5f);
		SetCameraPosXY(new Vector2(0, .93f), 1.5f);
		
		Invoke("OpenMouth", 1.55f);
		//	Invoke("LeaveScene", 3f);
	}
	
	/// <summary>
	/// Playing BUG opens the mouth.
	/// </summary>
	public void OpenMouth() {
		
		Go.to(UpperJawRef.transform, 1f, new GoTweenConfig()
			.localPosition(new Vector3(UpperJawRef.transform.localPosition.x, 38.7f, UpperJawRef.transform.localPosition.z))
			.setEaseType(GoEaseType.ElasticOut)
			);
		
		//	Invoke("CloseMouth", 3f);
	}
	
	/// <summary>
	/// Playing BUG closes the mouth.
	/// </summary>
	public void CloseMouth() {
		
		Go.to(UpperJawRef.transform, .75f, new GoTweenConfig()
			.localPosition(new Vector3(UpperJawRef.transform.localPosition.x, 6.16f, UpperJawRef.transform.localPosition.z))
			.setEaseType(GoEaseType.BounceOut)
			);
		
		CameraZoom = new Vector2(11.6f, 0);
		CameraPosXY = new Vector2(1.55f, 0);
		
		Invoke("LeaveScene", .8f);
	}
	
	public void LeaveScene() {
		Go.to(gameObject.transform, 2f, new GoTweenConfig()
			.position(new Vector3(-20f, gameObject.transform.position.y, gameObject.transform.position.z))
			.setEaseType(GoEaseType.ExpoOut)
			);
		
		Invoke("EnterScene", 4f);
	}
	
	/// <summary>
	/// Playing BUG syncs the mouth background.
	/// </summary>
	public void SyncMouthBg() {
		JawDifY = UpperJawRef.transform.position.y - LowerJawRef.transform.position.y;
		
		if (JawDifY / 13.1f < .55f) {
			MouthBgRef.transform.localScale = new Vector3(.75f, 0, 0);
		} else {
			MouthBgRef.transform.localScale = new Vector3(
				.95f,
				JawDifY / 13.1f,
				MouthBgRef.transform.localScale.z
				);
		}
	}
	
	/// <summary>
	/// Sets the camera zoom.
	/// </summary>
	/// <param name='_newZoom'>
	/// _new zoom.
	/// </param>
	/// <param name='_zoomDelay'>
	/// _zoom delay.
	/// </param>
	/// <param name='_zoomSpeed'>
	/// _zoom speed.
	/// </param>
	public void SetCameraZoom(float _newZoom, float _zoomDelay, float _zoomSpeed) {
		Go.to(GetComponent<BigBug>(), _zoomSpeed, new GoTweenConfig()
			.vector2Prop("CameraZoom", new Vector2(_newZoom, 0))
			.setDelay(_zoomDelay)
			.setEaseType(GoEaseType.ElasticOut)
			);
	}
	
	/// <summary>
	/// Sets the camera position X,Y
	/// </summary>
	/// <param name='_newPosXY'>
	/// new position X.
	/// new position Y.
	/// </param>
	public void SetCameraPosXY(Vector2 _newPosXY, float _delay) {
		// .93
		Go.to(GameObject.Find("Main Camera").transform, 2f, new GoTweenConfig()
			.position(new Vector3(
				_newPosXY.x,
				_newPosXY.y,
				GameObject.Find("Main Camera").transform.position.z
			))
			.setDelay(_delay)
			.setEaseType(GoEaseType.ExpoOut)
			);
	}
	
	/// <summary>
	/// Updates the camera zoom.
	/// </summary>
	public void UpdateCameraZoom() {
		GameObject.Find("Main Camera").camera.orthographicSize = CameraZoom.x;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCameraZoom();
		SyncMouthBg();
	}
}
