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
	
	public void MoveMeTo() {
		Go.killAllTweensWithTarget(gameObject);
		
		//	new movement code here.
		
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
