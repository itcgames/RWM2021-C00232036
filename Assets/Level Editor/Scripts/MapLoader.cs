using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapLoader : MonoBehaviour
{
    public string mapFilePath; // Saved Levels/level.json
    private LevelEditor _level;
    public GameObject item_1;
    public GameObject item_2;
    public GameObject item_3;
    public GameObject item_4;
    public GameObject item_5;
    public GameObject item_6;
    public GameObject item_7;
    public GameObject item_8;
    public GameObject background_1;
    public GameObject background_2;
    public GameObject background_3;
    public GameObject steakPickup;
    private int backgroundMenuIndex;

    void Start()
    {
        CreateLoader();
        LoadLevel();
    }

    LevelEditor CreateLoader()
    {
        _level = new LevelEditor();
        _level.editorObjects = new List<EditorObject.Data>();
        return _level;
    }

    public void ChangeBackgroundImage()
    {
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

    /// <summary>
    /// Load a level from a file.
    /// </summary>
    public void LoadLevel()
    {
        string path = Application.dataPath;
        string levelFile = Path.Combine(path, mapFilePath);

        if (File.Exists(levelFile))
        {
            // The objects currently in the level will be deleted (if there's any)
            EditorObject[] foundObjects = FindObjectsOfType<EditorObject>();

            foreach (EditorObject obj in foundObjects)
            {
                Destroy(obj.gameObject);
            }

            string json = File.ReadAllText(levelFile);
            _level = JsonUtility.FromJson<LevelEditor>(json);
            CreateFromFile();
        }
        else
        {
            Debug.Log("File not found!");
        }
    }

    /// <summary>
    /// Create level.
    /// </summary>
    void CreateFromFile()
    {
        GameObject newObj;

        for (int i = 0; i < _level.editorObjects.Count; i++)
        {
            if (_level.editorObjects[i].obType == EditorObject.ObjectType.Item_1)
            {
                newObj = Instantiate(item_1);
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
                newObj = Instantiate(item_2);
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
                newObj = Instantiate(item_3);
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
                newObj = Instantiate(item_4);
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
                newObj = Instantiate(item_5);
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
                newObj = Instantiate(item_6);
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
                newObj = Instantiate(item_7);
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
                newObj = Instantiate(item_8);
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
                // This sets the player starting position
                // PLAYER_POSITION = _level.editorObjects[i].obPosition;
            }
            else if (_level.editorObjects[i].obType == EditorObject.ObjectType.Collectable)
            {
                newObj = Instantiate(steakPickup);
                newObj.transform.position = _level.editorObjects[i].obPosition;
                newObj.transform.rotation = _level.editorObjects[i].obRotation;
                newObj.layer = 8;

                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.obPosition = newObj.transform.position;
                eo.data.obRotation = newObj.transform.rotation;
                eo.data.obType = EditorObject.ObjectType.Collectable;
            }
            else if (_level.editorObjects[i].obType == EditorObject.ObjectType.Background)
            {
                backgroundMenuIndex = _level.editorObjects[i].bckGrndIndex;
                ChangeBackgroundImage();
            }
        }
    }
}
