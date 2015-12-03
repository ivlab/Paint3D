using UnityEngine;
using System.Collections;
using MinVR;


/** This is the main class for the Paint3D application.  It listens for VREvents from the
 * MinVR server in order to update the tracker data for the brush, hand, and head each frame.
 * It also uses the FixedUpdate() callback to create new '3D paint strokes' when the brush 
 * button is pressed.
 */
public class Paint3DMain : MonoBehaviour {

	[Tooltip("This GameObject will move around with the tracker attached to the Brush.")]
	public GameObject brushCursor;
	[Tooltip("This GameObject will move around with the tracker attached to the Hand.")]
	public GameObject handCursor;


	void Start () {
		VRMain.VREventHandler += OnVREvent;
	}


	void FixedUpdate () {

		// If brush button is down, then add to the painting
		if (Input.GetMouseButton(0)) {
			// TODO: This creates a new painted object each frame.  Instead, for most brush types, 
			// we probably want to create a new BrushStroke the first time the brush button is 
			// pressed and then add to it each frame until the button is released.
			GameObject paintedObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			paintedObj.transform.parent = GameObject.Find("Painting").transform;
			paintedObj.transform.position = brushCursor.transform.position;
			paintedObj.transform.rotation = brushCursor.transform.rotation;
			paintedObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		}

		// If the hand button is down, then grab on to the painting and move it about
		if (Input.GetMouseButton(1)) {
			Vector3 posChange = handPos - lastHandPos;
			Quaternion rotChange = Quaternion.Inverse(handRot) * lastHandRot;
			GameObject painting = GameObject.Find("Painting");
			painting.transform.position += posChange;
			painting.transform.rotation *= rotChange;
		}


		// Save the current state of the hand
		lastHandPos = handPos;
		lastHandRot = handRot;
	}


	// This function gets called every time a new VREvent is generated.  Typically, VREvents will come
	// from the MinVR server, which polls trackers, buttons, and other input devices for input.  When
	// debugging on your laptop, you can also generate 'fake' VREvents using the VRMain script.
	void OnVREvent(VREvent e) {
		if (e.Name == "Brush_Move") {		
			Matrix4x4 m = VRConvert.ToMatrix4x4(e.DataIndex.GetValueAsDoubleArray("Transform"));
			brushPos = m.GetTranslation();
			brushRot = m.GetRotation();
			brushCursor.transform.position = brushPos;
			brushCursor.transform.rotation = brushRot;
		} 
		else if (e.Name == "Hand_Move") {
			Matrix4x4 m = VRConvert.ToMatrix4x4(e.DataIndex.GetValueAsDoubleArray("Transform"));
			handPos = m.GetTranslation();
			handRot = m.GetRotation();
			handCursor.transform.position = handPos;
			handCursor.transform.rotation = handRot;
		}
		else if (e.Name == "Head_Move") {
			Matrix4x4 m = VRConvert.ToMatrix4x4(e.DataIndex.GetValueAsDoubleArray("Transform"));
			headPos = m.GetTranslation();
			handRot = m.GetRotation();
		}
		else if (e.Name == "Mouse_Down") {
			Debug.Log("mouse down");
		}
		else if (e.Name == "Mouse_Up") {
			Debug.Log("mouse up");
		}
	}


	
	// This defines a public property of the Paint3D class that you can access in other scripts.
	// So, in any other script, you can write Paint3D.BrushPos in order to access the current 
	// position of the brush.
	public Vector3 BrushPos {
		get {
			return brushPos; 
		}
		set {
			brushPos = value;
		}
	}
	private Vector3 brushPos;
	
	// Same for brush rotation
	public Quaternion BrushRot {
		get {
			return brushRot; 
		}
		set {
			brushRot = value;
		}
	}
	private Quaternion brushRot;
	
	// Same for hand position
	public Vector3 HandPos {
		get {
			return handPos; 
		}
		set {
			handPos = value;
		}
	}
	private Vector3 handPos;
	private Vector3 lastHandPos;
	
	// Same for hand rotation
	public Quaternion HandRot {
		get {
			return handRot; 
		}
		set {
			handRot = value;
		}
	}
	private Quaternion handRot;
	private Quaternion lastHandRot;

	// Same for head position
	public Vector3 HeadPos {
		get {
			return headPos; 
		}
		set {
			headPos = value;
		}
	}
	private Vector3 headPos;
	
	// Same for head rotation
	public Quaternion HeadRot {
		get {
			return headRot; 
		}
		set {
			headRot = value;
		}
	}
	private Quaternion headRot;
	
	

}

