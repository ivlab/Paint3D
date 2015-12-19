using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class MenuTile
{
    public static GameObject prefab;
    private GameObject go;
    private Image image;
    //private

    private bool isEnabled = true;
    public bool Enabled {
        get { return isEnabled && Destination != null; }
        set { isEnabled = value; }
    }

    public string Label { get; private set; }

    public Color Background { get; private set; }

    public Color TextColor { get; private set; }

    public MenuNode Destination { get; private set; }

    public Action<string> Action { get; set; }

    private static float hoverTime = 0.5f;
    bool isHovering;
    float hoverStart;
    public void Hover(bool isOver, float time)
    {
        if (isOver)
        {
            if (isHovering)
            {
                if (time - hoverStart >= hoverTime)
                {
                    Press();
                }
            }
            else
            {
                isHovering = true;
                hoverStart = time;
                Color b = Background;
                b.a = 0.9f;
                Background = b;
                image.color = Background;
            }
        }
        else
        {
            isHovering = false;
            Color b = Background;
            b.a = 0.5f;
            Background = b;
            image.color = Background;
        }
    }

    public void Press()
    {
        if (Action != null)
        {
            Action.Invoke(Label); 
        }
    }

    public MenuTile(MenuNode dest, string label, Color bkg, Color fore)
    {
        Destination = dest;
        Label = label;
        Background = bkg;
        TextColor = fore;
    }

    public MenuTile CopyLook(MenuNode newDest)
    {
        return new MenuTile(newDest, Label, Background, TextColor);
    }

    public void CreatePanel(GameObject parent, Vector2 pos)
    {
        //GameObject.CreatePrimitive(PrimitiveType.pa)
        Vector3 newpos = pos;
        go = (GameObject)GameObject.Instantiate(prefab, newpos, Quaternion.identity);
        image = go.GetComponent<Image>();
    }
}

public class MenuNode
{
    public bool isRoot;

    public string Name { get; set; }

    public MenuTile Center { get; set; }

    public MenuTile Root { get; set; }

    public MenuTile Parent { get; set; }

    public List<MenuTile> Children { get; set; }

    public MenuNode(MenuNode parent)
    {
        if (parent == null)
        {
            isRoot = true;
            Center = new MenuTile(null, "Menu", new Color(0, 1, 0.047f, 0.392f), Color.black);
        }

        if (parent != null)
        {
            Parent = parent.Center.CopyLook(parent);
            if (parent.isRoot)
            {
                Root = Parent.CopyLook(parent);
            }
            else
            {
                Root = parent.Root;
            }
        }
    }

    public MenuNode AddChild(string name, string label, Color bkg, Color fore)
    {
        MenuNode node = new MenuNode(this);
        node.Name = name;
        node.Center = new MenuTile(null, label, bkg, fore);
        Children.Add(new MenuTile(node, label, bkg, fore));
        return node;
    }

    public MenuTile AddBranch(MenuNode node, string label, Color bkg, Color fore)
    {
        MenuTile tile = new MenuTile(node, label, bkg, fore);
        Children.Add(tile);
        //tile.Press.
        return tile;
    }
}

public class MenuInput : MonoBehaviour {

    Canvas canvas;
    RectTransform holderRT;
    GameObject holder;
    RectTransform lineRT;

    Painting painting;

    MenuNode menuRoot;
    MenuNode menuContext;
    bool menuVisible = false;

    RectTransform lineBtnRT;
    RectTransform tubeBtnRT;
    RectTransform bubbleBtnRT;

    RectTransform redRT;
    RectTransform greenRT;
    RectTransform blueRT;

    private void PopulateBrushMenu()
    {
        menuRoot = new MenuNode(null);
        menuRoot.Name = "Root";
        // brush selection
        MenuNode brushes = menuRoot.AddChild("brushes", "Brushes", new Color(0, 0, 1, 0.5f), Color.black);

        List<string> brushNames = BrushManager.AvailableBrushes;

        foreach (string name in brushNames)
        {
            Vector3 v = UnityEngine.Random.insideUnitSphere;

            MenuTile tile = brushes.AddBranch(menuRoot, name, new Color(v.x, v.y, v.z, 0.5f), Color.black);
            tile.Action = ChangeBrush;
            // get options
        }

        //menuRoot.Center.CreatePanel()
    }

    public void ChangeBrush(string name)
    {
        painting.CurrentBrush = name;
    }

    void Awake()
    {
    }

	// Use this for initialization
	void Start () {

        painting = GameObject.Find("PaintHolder").GetComponent<Painting>();
        canvas = GetComponent<Canvas>();
        holder = GameObject.Find("MenuHolder");
        holderRT = holder.GetComponent<RectTransform>();
        MenuTile.prefab = GameObject.Find("MarkButton");
        lineRT = GameObject.Find("Line").GetComponent<RectTransform>();

        lineBtnRT = GameObject.Find("LineButton").GetComponent<RectTransform>();
        tubeBtnRT = GameObject.Find("TubeButton").GetComponent<RectTransform>();
        bubbleBtnRT = GameObject.Find("BubbleButton").GetComponent<RectTransform>();

        lineBtnRT.localPosition = new Vector3(100, 0, 0);
        tubeBtnRT.localPosition = (new Vector3(Mathf.Cos(2 * Mathf.PI / 3), Mathf.Sin(2 * Mathf.PI / 3), 0f))*100;
        bubbleBtnRT.localPosition = (new Vector3(Mathf.Cos(2 * Mathf.PI / 3), -Mathf.Sin(2 * Mathf.PI / 3), 0f))*100;

        redRT = GameObject.Find("RedButton").GetComponent<RectTransform>();
        greenRT = GameObject.Find("GreenButton").GetComponent<RectTransform>();
        blueRT = GameObject.Find("BlueButton").GetComponent<RectTransform>();

        redRT.localPosition = (new Vector3(Mathf.Cos(Mathf.PI / 4), Mathf.Sin(Mathf.PI / 4), 0f) * 100);
        greenRT.localPosition = (new Vector3(100, 0, 0));
        blueRT.localPosition = (new Vector3(Mathf.Cos(Mathf.PI / 4), -Mathf.Sin(Mathf.PI / 4), 0f) * 100);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 mPos = Input.mousePosition;

        if (!menuVisible)
        {
            if(Input.GetButtonDown("Fire2"))
            {
                holderRT.anchoredPosition = Input.mousePosition;
                Debug.Log(Input.mousePosition.ToString());
                canvas.enabled = true;
                menuVisible = true;
            }
        }
        else
        {
            if (Input.GetButtonUp("Fire2"))
            {
                canvas.enabled = false;
                menuVisible = false;
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                float t = Time.time;
            }

            Vector3 delta = mPos - holderRT.position;
            lineRT.position = holderRT.position;
            lineRT.sizeDelta = new Vector2(3, delta.magnitude);
            lineRT.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg);
        }
	}

    Color newColor;

    public void SetColor(string s)
    {
        switch (s)
        {
            case "Blue":
                newColor = Color.blue;
                break;
            case "Red":
                newColor = Color.red;
                break;
            case "Green":
                newColor = Color.green;
                break;
            default:
                newColor = Color.gray;
                break;
        }
        painting.CurrentBrush = "LineBrush";
        painting.Options[painting.CurrentBrush]["StartColor"] = newColor;
        Debug.Log(painting.Options[painting.CurrentBrush]["StartColor"]);
    }

    public void ChangeColor()
    {
        //painting.Options[painting.CurrentBrush]["StartColor"] = newColor;
    }
}
