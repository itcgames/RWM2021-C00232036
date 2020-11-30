using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private bool _savePositionIn = false;
    private bool _loadPositionIn = false;
    private LevelEditor _level;
    public Transform dropdownMenu;
    public GameObject background_1;
    public GameObject background_2;
    public GameObject background_3;
    private int backgroundMenuIndex;
    private GameObject data;

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

    public void ChangeBackgroundImage()
    {
        backgroundMenuIndex = dropdownMenu.GetComponent<Dropdown>().value;

        background_1.SetActive(false);
        background_2.SetActive(false);
        background_3.SetActive(false);

        switch (backgroundMenuIndex)
        {
            case 0:
                background_1.SetActive(true);
                break;

            case 1:
                background_2.SetActive(true);
                break;

            case 2:
                background_3.SetActive(true);
                break;
        }
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
        if (_savePositionIn == false && _loadPositionIn == false && saveLoadMenuOpen == false)
        {
            saveUIAnimation.SetTrigger("SaveLoadIn");
            _savePositionIn = true;
            saveLoadMenuOpen = true;
        }
        else if (_savePositionIn == true && _loadPositionIn == false && saveLoadMenuOpen == true)
        {
            saveUIAnimation.SetTrigger("SaveLoadOut");
            _savePositionIn = false;
            saveLoadMenuOpen = false;
        }
    }

    public void ChooseLoad()
    {
        if (_loadPositionIn == false && _savePositionIn == false && saveLoadMenuOpen == false)
        {
            loadUIAnimation.SetTrigger("SaveLoadIn");
            _loadPositionIn = true;
            saveLoadMenuOpen = true;
        }
        else if (_loadPositionIn == true && _savePositionIn == false && saveLoadMenuOpen == true)
        {
            loadUIAnimation.SetTrigger("SaveLoadOut");
            _loadPositionIn = false;
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

        foreach (EditorObject tempObj in foundObjects)
        {
            _level.editorObjects.Add(tempObj.data);
        }

        // Save current background value
        EditorObject.Data objData;
        //GameObject obj;
        //EditorObject eo = obj.AddComponent<EditorObject>();
        objData.bckGrndIndex = backgroundMenuIndex;
        objData.obPosition = new Vector3(1, 1, 1);
        objData.obRotation = Quaternion.identity;
        objData.obType = EditorObject.ObjectType.Background;
        _level.editorObjects.Add(objData);

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
        _savePositionIn = false;
        saveLoadMenuOpen = false;
        levelNameSave.text = "";
        levelNameSave.DeactivateInputField();
        levelMessage.text = "JSON/" + levelFile + " saved";
        messageAnim.Play("MessageFade", 0, 0);
    }

    public void LoadLevel()
    {
        string folder = Application.dataPath + "/JSON/";
        string levelFile = "";

        // Default file name
        if (levelNameLoad.text == "")
        {
            levelFile = "new_level.json";
        }
        else
        {
            levelFile = levelNameLoad.text + ".json";
        }

        string path = Path.Combine(folder, levelFile);

        // Overwrite existing file
        if (File.Exists(path))
        {
            // The objects currently in the level will be deleted
            EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();

            foreach (EditorObject obj in foundObjects)
            {
                Destroy(obj.gameObject);
            }

            playerPlaced = false;

            string json = File.ReadAllText(path);
            _level = JsonUtility.FromJson<LevelEditor>(json);
            CreateFromFile();
        }
        else
        {
            loadUIAnimation.SetTrigger("SaveLoadOut");
            _loadPositionIn = false;
            saveLoadMenuOpen = false;
            levelMessage.text = levelFile + " not found";
            messageAnim.Play("MessageFade", 0, 0);
            levelNameLoad.DeactivateInputField();
        }
    }

    void CreateFromFile()
    {
        GameObject newObj;

        for (int i = 0; i < _level.editorObjects.Count; i++)
        {
            if (_level.editorObjects[i].obType == EditorObject.ObjectType.Item_1)
            {
                newObj = Instantiate(msScript.item_1);
                newObj.transform.position = _level.editorObjects[i].obPosition;
                newObj.transform.rotation = _level.editorObjects[i].obRotation;
                newObj.layer = 9;

                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.obPosition = newObj.transform.position;
                eo.data.obRotation = newObj.transform.rotation;
                eo.data.obType = EditorObject.ObjectType.Item_1;
            }
            else if (_level.editorObjects[i].obType == EditorObject.ObjectType.Item_2)
            {
                newObj = Instantiate(msScript.item_2);
                newObj.transform.position = _level.editorObjects[i].obPosition;
                newObj.transform.rotation = _level.editorObjects[i].obRotation;
                newObj.layer = 9;

                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.obPosition = newObj.transform.position;
                eo.data.obRotation = newObj.transform.rotation;
                eo.data.obType = EditorObject.ObjectType.Item_2;
            }
            else if (_level.editorObjects[i].obType == EditorObject.ObjectType.Item_3)
            {
                newObj = Instantiate(msScript.item_3);
                newObj.transform.position = _level.editorObjects[i].obPosition;
                newObj.transform.rotation = _level.editorObjects[i].obRotation;
                newObj.layer = 9;

                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.obPosition = newObj.transform.position;
                eo.data.obRotation = newObj.transform.rotation;
                eo.data.obType = EditorObject.ObjectType.Item_3;
            }
            else if (_level.editorObjects[i].obType == EditorObject.ObjectType.Item_4)
            {
                newObj = Instantiate(msScript.item_4);
                newObj.transform.position = _level.editorObjects[i].obPosition;
                newObj.transform.rotation = _level.editorObjects[i].obRotation;
                newObj.layer = 9;

                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.obPosition = newObj.transform.position;
                eo.data.obRotation = newObj.transform.rotation;
                eo.data.obType = EditorObject.ObjectType.Item_4;
            }
            else if (_level.editorObjects[i].obType == EditorObject.ObjectType.Item_5)
            {
                newObj = Instantiate(msScript.item_5);
                newObj.transform.position = _level.editorObjects[i].obPosition;
                newObj.transform.rotation = _level.editorObjects[i].obRotation;
                newObj.layer = 9;

                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.obPosition = newObj.transform.position;
                eo.data.obRotation = newObj.transform.rotation;
                eo.data.obType = EditorObject.ObjectType.Item_5;
            }
            else if (_level.editorObjects[i].obType == EditorObject.ObjectType.Item_6)
            {
                newObj = Instantiate(msScript.item_6);
                newObj.transform.position = _level.editorObjects[i].obPosition;
                newObj.transform.rotation = _level.editorObjects[i].obRotation;
                newObj.layer = 9;

                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.obPosition = newObj.transform.position;
                eo.data.obRotation = newObj.transform.rotation;
                eo.data.obType = EditorObject.ObjectType.Item_6;
            }
            else if (_level.editorObjects[i].obType == EditorObject.ObjectType.Item_7)
            {
                newObj = Instantiate(msScript.item_7);
                newObj.transform.position = _level.editorObjects[i].obPosition;
                newObj.transform.rotation = _level.editorObjects[i].obRotation;
                newObj.layer = 9;

                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.obPosition = newObj.transform.position;
                eo.data.obRotation = newObj.transform.rotation;
                eo.data.obType = EditorObject.ObjectType.Item_7;
            }
            else if (_level.editorObjects[i].obType == EditorObject.ObjectType.Item_8)
            {
                newObj = Instantiate(msScript.item_8);
                newObj.transform.position = _level.editorObjects[i].obPosition;
                newObj.transform.rotation = _level.editorObjects[i].obRotation;
                newObj.layer = 9;

                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.obPosition = newObj.transform.position;
                eo.data.obRotation = newObj.transform.rotation;
                eo.data.obType = EditorObject.ObjectType.Item_8;
            }
            else if (_level.editorObjects[i].obType == EditorObject.ObjectType.Player)
            {
                newObj = Instantiate(msScript.playerSt, transform.position, Quaternion.identity);
                newObj.layer = 9;
                newObj.AddComponent<CapsuleCollider>();
                newObj.GetComponent<CapsuleCollider>().height = 2;
                newObj.transform.position = _level.editorObjects[i].obPosition;
                newObj.transform.rotation = _level.editorObjects[i].obRotation;
                playerPlaced = true;

                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.obPosition = newObj.transform.position;
                eo.data.obRotation = newObj.transform.rotation;
                eo.data.obType = EditorObject.ObjectType.Player;
            }
            else if (_level.editorObjects[i].obType == EditorObject.ObjectType.Background)
            {
                backgroundMenuIndex = _level.editorObjects[i].bckGrndIndex;
                dropdownMenu.GetComponent<Dropdown>().value = backgroundMenuIndex;
                ChangeBackgroundImage();
            }
        }

        levelNameLoad.text = "";
        levelNameLoad.DeactivateInputField();
        loadUIAnimation.SetTrigger("SaveLoadOut");
        _loadPositionIn = false;
        saveLoadMenuOpen = false;
        levelMessage.text = "Level loaded";
        messageAnim.Play("MessageFade", 0, 0);
    }

    public void BackToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}


