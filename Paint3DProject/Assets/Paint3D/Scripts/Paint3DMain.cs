using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using MinVR;
using UnityEngine.UI;


/** This is the main class for the Paint3D application.  It listens for VREvents from the
 * MinVR server in order to update the tracker data for the brush, hand, and head each frame.
 * It also uses the FixedUpdate() callback to create new '3D paint strokes' when the brush 
 * button is pressed.
 */
public class Paint3DMain : MonoBehaviour
{

	[Tooltip("This GameObject will move around with the tracker attached to the Brush.")]
	public GameObject
		brushCursor;
	[Tooltip("This GameObject will move around with the tracker attached to the Hand.")]
	public GameObject
		handCursor;

	public GameObject
		headObj;

    // just show where other user's brush locate
    public GameObject
        brushCursor2;

    public Text info;
    public Text difference;
    public Text mPos;

    // Menu 
    public Canvas mainMenu;
    public Canvas subMenu1;
    //------------------- menu items ----------------------
    public Image brushTypeButton;
    public Image colorChangeButton;
    public Image envChangeButton;
    public Image cubeBrushButton;
    public Image cylinderBrushButton;
    public Image sphereBrushButton;

    // record the position of pen when menu is open
    private Vector3 btnClickPos; // record position of pen when menu button is clicked
    private Color inactiveBtnColor = new Color(0.5F, 0.5F, 0.5F, 0.5F); // default button color
    public Color btnHoverColor = new Color(0.0F, 0.0F, 0.7F, 0.5F);     // button color when hover on 
    //-----------------------------------------------------
    // brush type index: 1 -> cube; 2 -> sphere; cylinder -> 3
    private int user1BrushType = 1;
    private int user2BrushType = 1;

    // indicate whether to draw in this update call
    private bool user1StartDrawing = false;
    private bool user2StartDrawing = false;

    private Painting user1Painting;
    private Painting user2Painting;

    // Menu Flags
    private bool isOpenUser1Menu = false;
    private bool isOpenUser2Menu = false;
    private bool showMenu = false;
    private bool showSubMenu1 = false;
    private bool showSubMenu2 = false;
    private bool showSubMenu3 = false;
    private int mainMenuIndex = -1;
    private int subMenu1Index = -1;
    private int subMenu2Index = -1;
    private int subMenu3Index = -1;

    // determine user: TODO: send signal to server and get feedback from server to determine the user id
    public int user_id = 1; // default: user 1


    void Start ()
	{
		VRMain.VREventHandler += OnVREvent;
        mainMenu.enabled = false;
        subMenu1.enabled = false;
        btnClickPos = new Vector3(0, 0, 0);
        user1Painting = gameObject.AddComponent<Painting>();
        user1Painting.CurrentBrush = "LineBrush";
        user2Painting = gameObject.AddComponent<Painting>();
        user2Painting.CurrentBrush = "LineBrush";
    }


	void FixedUpdate ()
	{
        /*

		// If brush button is down, then add to the painting
		if (Input.GetMouseButton (0)) {
			// TODO: This creates a new painted object each frame.  Instead, for most brush types, 
			// we probably want to create a new BrushStroke the first time the brush button is 
			// pressed and then add to it each frame until the button is released.
			GameObject paintedObj = GameObject.CreatePrimitive (PrimitiveType.Cube);
			paintedObj.transform.parent = GameObject.Find ("Painting").transform;
			paintedObj.transform.position = brushCursor.transform.position;
			paintedObj.transform.rotation = brushCursor.transform.rotation;
			paintedObj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		}

		// If the hand button is down, then grab on to the painting and move it about
		if (Input.GetMouseButton (1)) {
			Vector3 posChange = handPos - lastHandPos;
			Quaternion rotChange = Quaternion.Inverse (handRot) * lastHandRot;
			GameObject painting = GameObject.Find ("Painting");
			painting.transform.position += posChange;
			painting.transform.rotation *= rotChange;
		}*/
		
		//
		if (user1StartDrawing && !showMenu && !showSubMenu1) {
            
            Vertex v = new Vertex();
            v.position = brushCursor.transform.position;
            v.orientation = brushCursor.transform.rotation;

            user1Painting.AddVertex(v);
        }

        if (user2StartDrawing && !showMenu && !showSubMenu1)
        {
            Vertex v = new Vertex();
            v.position = brushCursor2.transform.position;
            v.orientation = brushCursor2.transform.rotation;

            user2Painting.AddVertex(v);
        }

        // Show or Hide menu depends on Menu Flags
	    mainMenu.enabled = showMenu;
	    subMenu1.enabled = showSubMenu1;


        // Save the current state of the hand
        lastHandPos = handPos;
		lastHandRot = handRot;
	}


	// This function gets called every time a new VREvent is generated.  Typically, VREvents will come
	// from the MinVR server, which polls trackers, buttons, and other input devices for input.  When
	// debugging on your laptop, you can also generate 'fake' VREvents using the VRMain script.
	void OnVREvent (VREvent e)
	{
        // Cave system to Unity Virtual Environment Ratio
	    int ratio = 160;

		if (e.Name == "Brush_Move") {	// for faster user 2 	
			Matrix4x4 m = VRConvert.ToMatrix4x4 (e.DataIndex.GetValueAsDoubleArray ("Transform"));
			brushPos = m.GetTranslation ();
			brushRot = m.GetRotation ();
			brushCursor.transform.position = brushPos;
			brushCursor.transform.rotation = brushRot;
		} else if (e.Name == "Hand_Move") {
			Matrix4x4 m = VRConvert.ToMatrix4x4 (e.DataIndex.GetValueAsDoubleArray ("Transform"));
			handPos = m.GetTranslation ();
			handRot = m.GetRotation ();
			handCursor.transform.position = handPos;
			handCursor.transform.rotation = handRot;
		} else if (e.Name == "Head1_Move" && user_id == 1)
        { // for cave painting system user 1
            Matrix4x4 m = VRConvert.ToMatrix4x4(e.DataIndex.GetValueAsDoubleArray("Transform"));
            headPos = m.GetTranslation();
            headRot = m.GetRotation();
            //headObj.transform.position = headPos;
            //headObj.transform.rotation = headRot;
        }
        else if (e.Name == "Head2_Move" && user_id == 2)
        { // for cave painting system user 2
          //Matrix4x4 m = VRConvert.ToMatrix4x4 (e.DataIndex.GetValueAsDoubleArray ("Transform"));
          //headPos = m.GetTranslation ();
          //headRot = m.GetRotation ();
          //headObj.transform.position = headPos;
          //headObj.transform.rotation = headRot;
        }
        else if (e.Name == "Head_Move") {
			Matrix4x4 m = VRConvert.ToMatrix4x4 (e.DataIndex.GetValueAsDoubleArray ("Transform"));
			headPos = m.GetTranslation ();
			handRot = m.GetRotation ();
		} else if (e.Name == "Mouse_Down") {	// TODO : useless for now
			Debug.Log ("mouse down");
		} else if (e.Name == "Mouse_Up") {
			Debug.Log ("mouse up");
        }
        //========================= Button down and up events==============================
        else if (e.Name == "stylus1_btn0_down")
        {        
            // stylus 1 : red pen
            // user 1 start to draw
            if (user_id == 1)
            {
                if (!showMenu && !showSubMenu1)
                {
                    user1StartDrawing = true;
                    user1Painting.StartNewStroke();
                    // add first vertex to painting
                    Vertex v = new Vertex();
                    v.position = brushCursor.transform.position;
                    v.orientation = brushCursor.transform.rotation;

                    user1Painting.AddVertex(v);
                }
                else
                {
                    // ensure drawing is disabled
                    user1StartDrawing = false;
                    user1Painting.EndStroke();
                }
                Debug.Log("Red User 1 drawing down");
            } else if (user_id == 2)
            {
                if (!isOpenUser1Menu)
                {
                    user1StartDrawing = true;
                    user1Painting.StartNewStroke();
                    // add first vertex to painting
                    Vertex v = new Vertex();
                    v.position = brushCursor.transform.position;
                    v.orientation = brushCursor.transform.rotation;
                    user1Painting.AddVertex(v);
                }
            }

        }
        else if (e.Name == "stylus1_btn0_up")
        {
            // ensure user 1 drawing is disabled
            //user1StartDrawing = false;
            user1Painting.EndStroke();

            if (user_id == 1)
            {
                // button click according to which menu user in 
                if (showMenu)
                {
                    if (mainMenuIndex == 1)
                    {
                        showSubMenu1 = true;
                        showMenu = false;
                    }
                }
                else if (showSubMenu1)
                {
                    if (subMenu1Index == 1)
                    {
                        //user1BrushType = 1;
                        user1Painting.CurrentBrush = "LineBrush";
                    }
                    else if (subMenu1Index == 2)
                    {
                        //user1BrushType = 2;
                        user1Painting.CurrentBrush = "TubeBrush";
                    }
                    else if (subMenu1Index == 3)
                    {
                        //user1BrushType = 3;
                        user1Painting.CurrentBrush = "BubbleBrush";
                    }

                    showSubMenu1 = false;
                    isOpenUser1Menu = false;
                }
                Debug.Log("Red User 1 drawing up");
            }
        }
        else if (e.Name == "stylus1_btn1_down")
        {    // controll what user 1 see
             // TODO : manipulate User 2 camera
            Debug.Log("Red User 1 control down");

        }
        else if (e.Name == "stylus1_btn1_up")
        {
            if (user_id == 1)
            {
                if (!showMenu && !showSubMenu1)
                {
                    showMenu = true;
                    isOpenUser1Menu = true;
                    Vector3 temp = brushCursor.transform.position;
                    btnClickPos = temp;
                } else if (showMenu)
                {
                    showMenu = false;
                    isOpenUser1Menu = false;
                } else if (showSubMenu1)
                {
                    showSubMenu1 = false;
                    isOpenUser1Menu = false;
                }
                
            }
            Debug.Log("Red User 1 control up");

        }
        else if (e.Name == "stylus0_btn0_down")
        {
            // stylus 0: blue pen 
            // user 2 start to draw
            if (user_id == 2)
            {
                if (!showMenu && !showSubMenu1)
                {
                    user2StartDrawing = true;
                    user2Painting.StartNewStroke();
                    // add first vertex to painting
                    Vertex v = new Vertex();
                    v.position = brushCursor.transform.position;
                    v.orientation = brushCursor.transform.rotation;
                    user2Painting.AddVertex(v);
                }
                else
                {
                    // ensure drawing is disabled
                    user2StartDrawing = false;
                    user2Painting.EndStroke();
                }
                Debug.Log("Red User 1 drawing down");
            }
            else if (user_id == 1)
            {
                if (!isOpenUser2Menu)
                {
                    user2StartDrawing = true;
                    user2Painting.StartNewStroke();
                    // add first vertex to painting
                    Vertex v = new Vertex();
                    v.position = brushCursor.transform.position;
                    v.orientation = brushCursor.transform.rotation;
                    user2Painting.AddVertex(v);
                }
            }

        }
        else if (e.Name == "stylus0_btn0_up")
        {
            // ensure user 2 drawing is disabled
            user2StartDrawing = false;
            user2Painting.EndStroke();

            if (user_id == 2)
            {
                // button click according to which menu user in 
                if (showMenu)
                {
                    if (mainMenuIndex == 1)
                    {
                        showSubMenu1 = true;
                        showMenu = false;
                    }
                }
                else if (showSubMenu1)
                {
                    if (subMenu1Index == 1)
                    {
                        //user2BrushType = 1;
                        user2Painting.CurrentBrush = "LineBrush";
                    }
                    else if (subMenu1Index == 2)
                    {
                        user2BrushType = 2;
                        user2Painting.CurrentBrush = "TubeBrush";
                    }
                    else if (subMenu1Index == 3)
                    {
                        user2BrushType = 3;
                        user2Painting.CurrentBrush = "BubbleBrush";
                    }

                    showSubMenu1 = false;
                    isOpenUser2Menu = false;
                }
                Debug.Log("Blue User 2 drawing up");
            }

        }
        else if (e.Name == "stylus0_btn1_down")
        {    // control what user 2 see
             // TODO : manipulate User 2 camera
            Debug.Log("Blue User 2 control down");

        }
        else if (e.Name == "stylus0_btn1_up")
        {
            if (user_id == 2)
            {
                showMenu = !showMenu;
                mainMenu.enabled = showMenu;
                Vector3 temp = brushCursor2.transform.position;
                btnClickPos = temp;
            }
            Debug.Log("Blue User 2 control up");

        }

        //======================================================================
        else if (e.Name == "Brush1_Move" )
        {
            Matrix4x4 m = VRConvert.ToMatrix4x4(e.DataIndex.GetValueAsDoubleArray("Transform"));
            brushPos = m.GetTranslation();
            brushRot = m.GetRotation();
            brushCursor.transform.position = brushPos;
            brushCursor.transform.rotation = brushRot;
            if (user_id == 1)
            {
                if (showMenu)
                { 
                    brushCursor.SetActive(false);   // hide brush object
                    float ydiff = btnClickPos.y - brushPos.y;
                    float zdiff = btnClickPos.z - brushPos.z;

                    info.text = "btnClick.z: " + btnClickPos.z.ToString();
                    mPos.text = "Mouse Pos.z: " + brushPos.z;
                    difference.text = "diff: " + zdiff;

                    // select item according to y position of pen
                    if (ydiff < 0)
                    {
                        mainMenuIndex = 1;
                        brushTypeButton.color = btnHoverColor;

                        // reset other panels colors
                        colorChangeButton.color = inactiveBtnColor;
                        envChangeButton.color = inactiveBtnColor;

                    }
                    else if ((ydiff >= 0) && (ydiff < 160 / ratio))
                    {
                        mainMenuIndex = 2;
                        colorChangeButton.color = btnHoverColor;

                        // reset other panels colors
                        brushTypeButton.color = inactiveBtnColor;
                        envChangeButton.color = inactiveBtnColor;

                    }
                    else if (ydiff >= 160 / ratio)
                    {
                        mainMenuIndex = 3;
                        envChangeButton.color = btnHoverColor;

                        // reset other panels and buttons colors
                        colorChangeButton.color = inactiveBtnColor;
                        brushTypeButton.color = inactiveBtnColor;

                    }

                    /*
                    Vector3 rotatedZ = headObj.transform.TransformVector(new Vector3(0f, 0f, 1f)).normalized;

                    Vector3 delta = brushPos - oldBrushPos;


                    does not work for now!!!
                    if (zdiff < -160/ratio)
                    {
                        if (mainMenuIndex == 1 && showMenu)
                        {
                                //showSubMenu1 = true;
                                subMenu1.enabled = true;
                        }
                        showMenu = false;

                    } else if (zdiff > 160/ratio)
                    {
                        if (subMenu1)
                        {
                            //showSubMenu1 = false;
                            subMenu1.enabled = false;
                            showMenu = true;
                        }

                    }*/
                } else if (showSubMenu1)
                {
                    brushCursor.SetActive(false);   // hide brush object
                    float ydiff = btnClickPos.y - brushPos.y;
                    float zdiff = btnClickPos.z - brushPos.z;

                    info.text = "btnClick.z: " + btnClickPos.z.ToString();
                    mPos.text = "Mouse Pos.z: " + brushPos.z;
                    difference.text = "diff: " + zdiff;

                    // select item according to y position of pen
                    if (ydiff < 0)
                    {
                        subMenu1Index = 1;
                        cubeBrushButton.color = btnHoverColor;

                        // reset other panels colors
                        sphereBrushButton.color = inactiveBtnColor;
                        cylinderBrushButton.color = inactiveBtnColor;

                    }
                    else if ((ydiff >= 0) && (ydiff < 160 / ratio))
                    {
                        subMenu1Index = 2;
                        sphereBrushButton.color = btnHoverColor;

                        // reset other panels colors
                        cubeBrushButton.color = inactiveBtnColor;
                        cylinderBrushButton.color = inactiveBtnColor;

                    }
                    else if (ydiff >= 160 / ratio)
                    {
                        subMenu1Index = 3;
                        cylinderBrushButton.color = btnHoverColor;

                        // reset other panels and buttons colors
                        sphereBrushButton.color = inactiveBtnColor;
                        cubeBrushButton.color = inactiveBtnColor;

                    }
                }
                else
                {
                    brushCursor.SetActive(true);    // show brush
                }
            }
        } else if (e.Name == "Brush2_Move")
        {
            Matrix4x4 m = VRConvert.ToMatrix4x4(e.DataIndex.GetValueAsDoubleArray("Transform"));
            brushPos = m.GetTranslation();
            brushRot = m.GetRotation();
            brushCursor2.transform.position = brushPos;
            brushCursor2.transform.rotation = brushRot;
            if (user_id == 2)
            {
                if (showMenu)
                {
                    brushCursor2.SetActive(false);   // hide brush object
                    float ydiff = btnClickPos.y - brushPos.y;
                    float zdiff = btnClickPos.z - brushPos.z;

                    info.text = "btnClick.z: " + btnClickPos.z.ToString();
                    mPos.text = "Mouse Pos.z: " + brushPos.z;
                    difference.text = "diff: " + zdiff;

                    // select item according to y position of pen
                    if (ydiff < 0)
                    {
                        mainMenuIndex = 1;
                        brushTypeButton.color = btnHoverColor;

                        // reset other panels colors
                        colorChangeButton.color = inactiveBtnColor;
                        envChangeButton.color = inactiveBtnColor;

                    }
                    else if ((ydiff >= 0) && (ydiff < 160 / ratio))
                    {
                        mainMenuIndex = 2;
                        colorChangeButton.color = btnHoverColor;

                        // reset other panels colors
                        brushTypeButton.color = inactiveBtnColor;
                        envChangeButton.color = inactiveBtnColor;

                    }
                    else if (ydiff >= 160 / ratio)
                    {
                        mainMenuIndex = 3;
                        envChangeButton.color = btnHoverColor;

                        // reset other panels and buttons colors
                        colorChangeButton.color = inactiveBtnColor;
                        brushTypeButton.color = inactiveBtnColor;

                    }

                    /*
                    Vector3 rotatedZ = headObj.transform.TransformVector(new Vector3(0f, 0f, 1f)).normalized;

                    Vector3 delta = brushPos - oldBrushPos;


                    does not work for now!!!
                    if (zdiff < -160/ratio)
                    {
                        if (mainMenuIndex == 1 && showMenu)
                        {
                                //showSubMenu1 = true;
                                subMenu1.enabled = true;
                        }
                        showMenu = false;

                    } else if (zdiff > 160/ratio)
                    {
                        if (subMenu1)
                        {
                            //showSubMenu1 = false;
                            subMenu1.enabled = false;
                            showMenu = true;
                        }

                    }*/
                }
                else if (showSubMenu1)
                {
                    brushCursor2.SetActive(false);   // hide brush object
                    float ydiff = btnClickPos.y - brushPos.y;
                    float zdiff = btnClickPos.z - brushPos.z;

                    info.text = "btnClick.z: " + btnClickPos.z.ToString();
                    mPos.text = "Mouse Pos.z: " + brushPos.z;
                    difference.text = "diff: " + zdiff;

                    // select item according to y position of pen
                    if (ydiff < 0)
                    {
                        subMenu1Index = 1;
                        cubeBrushButton.color = btnHoverColor;

                        // reset other panels colors
                        sphereBrushButton.color = inactiveBtnColor;
                        cylinderBrushButton.color = inactiveBtnColor;

                    }
                    else if ((ydiff >= 0) && (ydiff < 160 / ratio))
                    {
                        subMenu1Index = 2;
                        sphereBrushButton.color = btnHoverColor;

                        // reset other panels colors
                        cubeBrushButton.color = inactiveBtnColor;
                        cylinderBrushButton.color = inactiveBtnColor;

                    }
                    else if (ydiff >= 160 / ratio)
                    {
                        subMenu1Index = 3;
                        cylinderBrushButton.color = btnHoverColor;

                        // reset other panels and buttons colors
                        sphereBrushButton.color = inactiveBtnColor;
                        cubeBrushButton.color = inactiveBtnColor;

                    }
                }
                else
                {
                    brushCursor2.SetActive(true);    // show brush
                }
            }
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

