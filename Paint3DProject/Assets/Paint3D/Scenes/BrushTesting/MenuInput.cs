using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MenuTile
{
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

    MenuNode menuRoot;

    MenuNode menuContext;

    bool menuVisible = false;

    private void PopulateBrushMenu()
    {
        menuRoot = new MenuNode(null);
        menuRoot.Name = "Root";
        // brush selection
        MenuNode brushes = menuRoot.AddChild("brushes", "Brushes", new Color(0,0,1,0.5f), Color.black);

        List<string> brushNames = BrushManager.AvailableBrushes;

        foreach (string name in brushNames)
        {
            Vector3 v = UnityEngine.Random.insideUnitSphere;

            MenuTile tile = brushes.AddBranch(menuRoot, name, new Color(v.x, v.y, v.z, 0.5f), Color.black);


            // get options
        }
    }

    public void ChangeBrush(string name)
    {

    }

	// Use this for initialization
	void Start () {
        canvas = GetComponent<Canvas>();
        holder = GameObject.Find("MenuHolder");
        holderRT = holder.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
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
        }
	}
}
