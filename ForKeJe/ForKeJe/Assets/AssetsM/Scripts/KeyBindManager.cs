using System.Collections.Generic;//Dictionary
using UnityEngine.UI;//UI element
using UnityEngine;//Unity

public class KeyBindManager : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text jump, left, right, down, shoot;
    public GameObject currentKey;
    public Color32 changed = new Color32(39, 171, 249, 255);
    public Color32 selected = new Color32(239, 116, 36, 255);

    void Start()
    {
        keys.Add("Jump",(KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Jump", "Space")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keys.Add("Shoot", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Shoot", "B")));


        jump.text = keys["Jump"].ToString();
        left.text = keys["Left"].ToString();
        down.text = keys["Down"].ToString();
        right.text = keys["Right"].ToString();
        shoot.text = keys["Shoot"].ToString();

    }

    void Update()
    {
        if (Input.GetKeyDown(keys["Jump"]))
        {
            Debug.Log("Jump");
        }
        if (Input.GetKeyDown(keys["Left"]))
        {
            Debug.Log("Left");
        }
        if (Input.GetKeyDown(keys["Down"]))
        {
            Debug.Log("Down");
        }
        if (Input.GetKeyDown(keys["Right"]))
        {
            Debug.Log("Right");
        }
        if (Input.GetKeyDown(keys["Shoot"]))
        {
            Debug.Log("Shoot");
        }
    }




    private void OnGUI()//Events
    {
        if(currentKey != null)
        {
            string newKey = "";
            Event e = Event.current;
            if (e.isKey)
            {
                newKey = e.keyCode.ToString();
            }

            if (newKey != "")
            {
                keys[currentKey.name] = (KeyCode)System.Enum.Parse(typeof(KeyCode), newKey);
                currentKey.GetComponentInChildren<Text>().text = newKey;
                currentKey.GetComponent<Image>().color = changed;
                currentKey = null;
            }
        }       
        
    }
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;

        if (currentKey != null)
        {            
            currentKey.GetComponent<Image>().color = selected;
        }  
    }
    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}
