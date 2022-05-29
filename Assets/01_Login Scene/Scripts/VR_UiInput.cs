using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class VR_UiInput : MonoBehaviour
{
    public SteamVR_LaserPointer[] pointer = new SteamVR_LaserPointer[2];
    public bool selected;

    [SerializeField] private UnityEngine.UI.Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<UnityEngine.UI.Button>();
        for (int i = 0; i < pointer.Length; i++)
        {
            pointer[i].PointerIn += PointerInside;
            pointer[i].PointerClick += PointerClick;
            pointer[i].PointerOut += PointerOutside;
        }
        selected = false;
    }
    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && selected == false)
        {
            selected = true;
            Debug.Log("pointer is inside this object : " + e.target.name);
        }
    }
    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && selected == true)
        {
            selected = false;
            Debug.Log("pointer is outside this object : " + e.target.name);
        }
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && selected == true && btn)
        {
            btn.onClick.Invoke();
            Debug.Log("pointer is click this object : " + e.target.name);
        }
    }

    public bool get_selected_value()
    {
        return selected;
    }
}
