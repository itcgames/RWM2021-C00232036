using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    [HideInInspector]
    public bool playerPlaced = false;

    [HideInInspector]
    public bool saveLoadMenuOpen = false;

    public MeshFilter mouseObject;
    public MouseScript msScript;
    public InputField levelNameSave;
    public InputField levelNameLoad;
    public Mesh startMarker;
    public Animator itemUIAnimation;
    public Animator optionUIAnimation;
    public Animator saveUIAnimation;
    public Animator loadUIAnimation;
    public Slider flipSlider;
    public GameObject flipUI;
    public Text levelMessage;
    public Animator messageAnim;
    private bool _itemPositionIn = true;
    private bool _optionPositionIn = true;
    private bool _saveLoadPositionIn = false;
    private LevelEditor _level;

    void Start()
    {
        flipSlider.onValueChanged.AddListener(delegate { RotationValueChange(); });
        CreateEditor();
    }

    LevelEditor CreateEditor()
    {
        _level = new LevelEditor();
        _level.editorObjects = new List<EditorObject.Data>();
        return _level;
    }

    void RotationValueChange()
    {
        msScript.rotObject.transform.Rotate(0, flipSlider.value * 180.0f, 0);
        msScript.rotObject.GetComponent<EditorObject>().data.obRotation = msScript.rotObject.transform.rotation;
    }

    public void SlideItemMenu()
    {
        if (_itemPositionIn == false)
        {
            itemUIAnimation.SetTrigger("ItemMenuIn");
            _itemPositionIn = true;
        }
        else
        {
            itemUIAnimation.SetTrigger("ItemMenuOut");
            _itemPositionIn = false;
        }
    }

    public void SlideOptionMenu()
    {
        if (_optionPositionIn == false)
        {
            optionUIAnimation.SetTrigger("OptionMenuIn");
            _optionPositionIn = true;
        }
        else
        {
            optionUIAnimation.SetTrigger("OptionMenuOut");
            _optionPositionIn = false;
        }
    }

    public void ChooseSave()
    {
        if (_saveLoadPositionIn == false)
        {
            saveUIAnimation.SetTrigger("SaveLoadIn");
            _saveLoadPositionIn = true;
            saveLoadMenuOpen = true;
        }
        else
        {
            saveUIAnimation.SetTrigger("SaveLoadOut");
            _saveLoadPositionIn = false;
            saveLoadMenuOpen = false;
        }
    }

    public void Select_Item_1() { msScript.itemOption = MouseScript.ItemList.Item_1; }
    public void Select_Item_2() { msScript.itemOption = MouseScript.ItemList.Item_2; }
    public void Select_Item_3() { msScript.itemOption = MouseScript.ItemList.Item_3; }
    public void Select_Item_4() { msScript.itemOption = MouseScript.ItemList.Item_4; }
    public void Select_Item_5() { msScript.itemOption = MouseScript.ItemList.Item_5; }
    public void Select_Item_6() { msScript.itemOption = MouseScript.ItemList.Item_6; }
    public void Select_Item_7() { msScript.itemOption = MouseScript.ItemList.Item_7; }
    public void Select_Item_8() { msScript.itemOption = MouseScript.ItemList.Item_8; }

    public void ChoosePlayerStart()
    {
        msScript.itemOption = MouseScript.ItemList.Player;
        mouseObject.mesh = startMarker;
    }

    public void ChooseCreate()
    {
        msScript.manipulateOption = MouseScript.LevelManipulation.Create;
        msScript.meshRend.enabled = true;
        flipUI.SetActive(false);
    }

    public void ChooseRotate()
    {
        msScript.manipulateOption = MouseScript.LevelManipulation.Rotate;
        msScript.meshRend.enabled = false;
        flipUI.SetActive(true);
    }

    public void ChooseDestroy()
    {
        msScript.manipulateOption = MouseScript.LevelManipulation.Destroy;
        msScript.meshRend.enabled = false;
        flipUI.SetActive(false);
    }

    public void SaveLevel()
    {
        EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();

        foreach (EditorObject obj in foundObjects)
        {
            _level.editorObjects.Add(obj.data);
        }

        string json = JsonUtility.ToJson(_level);
        string folder = Application.dataPath + "/JSON/";
        string levelFile = "";

        // Default file name
        if (levelNameSave.text == "")
        {
            levelFile = "new_level.json";
        }
        else
        {
            levelFile = levelNameSave.text + ".json";
        }

        // If directory doesn't exist then create it
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        string path = Path.Combine(folder, levelFile);

        // Overwrite existing file
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        // Save
        File.WriteAllText(path, json);

        // Remove save menu
        saveUIAnimation.SetTrigger("SaveLoadOut");
        _saveLoadPositionIn = false;
        saveLoadMenuOpen = false;
        levelNameSave.text = "";
        levelNameSave.DeactivateInputField();
        levelMessage.text = "JSON/" + levelFile + " saved";
        messageAnim.Play("MessageFade", 0, 0);
    }
}


