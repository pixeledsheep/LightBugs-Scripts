using UnityEngine;
using System.Collections;

public class GenericBubble : MonoBehaviour {
	
	public float StartingSize;
	public float MouthStartingPosY;
	public float MyDelay;
	
	public int CurrentSideIndex;
	public int NextSideIndex;
	
	public GameObject MyTarget;
	public Vector2 MyPosXY;
	
	// Use this for initialization
	void Start () {
		Init();
	}
	
	void Awake() {
		MyTarget = GameObject.Find("BubbleTarget");
	}
	
	/// <summary>
	/// Init this instance.
	/// </summary>
	void Init() {
		CurrentSideIndex = Random.Range(0, 4);
		NextSideIndex = 5;
		//	Debug.Log(GetRandomSide("top"));
	}
	
	public void DecideMeToSide(int _sideIndex) {
		if (_sideIndex == 0) {
			MyPosXY = GetRandomSide("top");
		}
		
		if (_sideIndex == 1) {
			MyPosXY = GetRandomSide("right");
		}
		
		if (_sideIndex == 2) {
			MyPosXY = GetRandomSide("bottom");
		}
		
		if (_sideIndex == 3) {
			MyPosXY = GetRandomSide("left");
		}
	}
	
	/// <summary>
	/// Gets the random side.
	/// </summary>
	/// <returns>
	/// The random side.
	/// </returns>
	/// <param name='_sideName'>
	/// _side name.
	/// </param>
	public Vector2 GetRandomSide(string _sideName) {
		
		
		Vector2 _tempVec2 = new Vector2();
		
		if (_sideName == "left") {
			_tempVec2.x = GameObject.Find("Left_Margin").transform.localPosition.x;
			_tempVec2.y = GetRandomY();
		}
		
		if (_sideName == "right") {
			_tempVec2.x = GameObject.Find("Right_Margin").transform.localPosition.x;
			_tempVec2.y = GetRandomY();
		}
		
		if (_sideName == "top") {
			_tempVec2.x = GetRandomX();
			_tempVec2.y = GameObject.Find("Top_Margin").transform.localPosition.y;
		}
		
		if (_sideName == "bottom") {
			_tempVec2.x = GetRandomX();
			_tempVec2.y = GameObject.Find("Bottom_Margin").transform.localPosition.y;
		}
		
		//	Debug.Log("->	_sideName: " + _sideName + ", _tempVec2: " + _tempVec2);
		return _tempVec2;
	}
	
	public void MoveMeTo() {
		
		CurrentSideIndex = Random.Range(0, 4);
		DecideMeToSide(CurrentSideIndex);
		
		Go.killAllTweensWithTarget(gameObject);
		Go.to(gameObject.transform, 2f, new GoTweenConfig()
			.localPosition(new Vector3(MyPosXY.x, MyPosXY.y, gameObject.transform.localPosition.z))
			.setEaseType(GoEaseType.Linear)
			//	.setEaseType(GoEaseType.SineInOut)
			);
		
		Invoke("MoveMeNow", 2f);
	}
	
	public void MoveMeNow() {
		MoveMeTo();
	}
	
	/// <summary>
	/// Gets the random x.
	/// </summary>
	/// <returns>
	/// The random x.
	/// </returns>
	public float GetRandomX() {
		float _newX = Random.Range(-140f, 140f) / 10f;
		return _newX;
	}
	
	/// <summary>
	/// Gets the random y.
	/// </summary>
	/// <returns>
	/// The random y.
	/// </returns>
	public float GetRandomY() {
		float _newY = Random.Range(-170f, 170f) / 10f;
		return _newY;
	}
	
	/// <summary>
	/// Sets the percent.
	/// </summary>
	/// <returns>
	/// The percent.
	/// </returns>
	/// <param name='_val'>
	/// _val.
	/// </param>
	public float SetPercent(float _val) {
		float newVal = _val * .01f;
		return newVal;
	}
	
	/// <summary>
	/// Sets me up.
	/// </summary>
	public IEnumerator SetMeUp() {
		
		yield return new WaitForSeconds(MyDelay);
		
		gameObject.transform.position = new Vector3(
			Random.Range(-5, 5),
			 MyTarget.transform.position.y,
			 MyTarget.transform.position.z
			);
		
		gameObject.transform.localScale = new Vector3(
			SetPercent(StartingSize), 
			SetPercent(StartingSize), 
			SetPercent(StartingSize)
			);
		
		StartCoroutine("InflateMe");
	}
	
	/// <summary>
	/// Inflates me.
	/// </summary>
	IEnumerator InflateMe() {
		
		MouthStartingPosY = Random.Range(-170f, 170f) / 10f;
		//	Debug.Log(gameObject.transform.name + " ->" + MouthStartingPosY);
		
		Go.to(gameObject.transform, 2f, new GoTweenConfig()
			.localPosition(new Vector3(
					gameObject.transform.localPosition.x,
					MouthStartingPosY,
					gameObject.transform.localPosition.z
					)
				)
			.setEaseType(GoEaseType.ExpoOut)
			);
		
		yield return new WaitForSeconds(.15f);
		
		float _tempSizeindexer = Random.Range(100f, 250f) / 1000f;
		float _newDiameter = SetPercent(StartingSize) + _tempSizeindexer;
		//	Debug.Log("->	_newDiameter: " + _newDiameter);
		
		Go.to(gameObject.transform, 1f, new GoTweenConfig()
			.scale(new Vector3(
				_newDiameter, 
				_newDiameter, 
				_newDiameter
			))
			.setEaseType(GoEaseType.ElasticOut)
		);
		
		Invoke("MoveMeTo", 2f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
