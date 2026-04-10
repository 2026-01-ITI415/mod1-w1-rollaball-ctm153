using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public Text countText;
	public Text winText;

	public GameObject platform;
	public float tiltSpeed = 30f;
	public float maxTiltAngle = 25f;

	private Rigidbody rb;
	private int count;
	private float tiltX = 0f;
	private float tiltZ = 0f;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		tiltZ = Mathf.Lerp(tiltZ, -moveHorizontal * maxTiltAngle, tiltSpeed * Time.deltaTime);
		tiltX = Mathf.Lerp(tiltX, moveVertical * maxTiltAngle, tiltSpeed * Time.deltaTime);

		platform.transform.rotation = Quaternion.Euler(tiltX, 0f, tiltZ);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 6) 
		{
			winText.text = "You Win!";
		}
	}
}