using UnityEngine;
using UnityEngine.EventSystems;

public class MouseScript : MonoBehaviour
{
    public enum ItemList { Item_1, Item_2, Item_3, Item_4, Item_5, Item_6, Item_7, Item_8, Player };
    public enum LevelManipulation { Create, Destroy, Rotate };

    [HideInInspector]
    public ItemList itemOption = ItemList.Item_1;

    [HideInInspector]
    public MeshRenderer meshRend;

    [HideInInspector]
    public LevelManipulation manipulateOption = LevelManipulation.Create;

    [HideInInspector]
    public GameObject rotObject;

    public Material allowedPlacement;
    public Material disallowedPlacement;
    public GameObject playerSt;
    public ManagerScript manager;
    
    // Level items
    public GameObject item_1;
    public GameObject item_2;
    public GameObject item_3;
    public GameObject item_4;
    public GameObject item_5;
    public GameObject item_6;
    public GameObject item_7;
    public GameObject item_8;
    public GameObject background;

    // Mouse and ray casting stuff
    private bool _colliding;
    private Vector3 _mousePosition;
    private RaycastHit _rayHit;    
    private Ray _rayCast;
    private GridSystem _gridSystem;

    void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        _gridSystem = FindObjectOfType<GridSystem>();
    }

    void Update()
    {
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);

        // Clamp object movement both x and y coordinates only
        transform.position = new Vector3(Mathf.Clamp(_mousePosition.x, -50, 180), Mathf.Clamp(_mousePosition.y, -20, 20), 0.75f);

        _rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_rayCast, out _rayHit))
        {
            if (_rayHit.collider.gameObject.layer == 9) // Check if raycast hits object
            {
                _colliding = true;
                meshRend.material = disallowedPlacement; // Red for no-no
            }
            else
            {
                _colliding = false;
                meshRend.material = allowedPlacement; // Green for cool and good
            }
        }

        // Mouse left button click
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (_colliding == false && manipulateOption == LevelManipulation.Create)
                {
                    CreateObject();
                }                    
                else if (_colliding == true && manipulateOption == LevelManipulation.Rotate)
                {
                    SetRotateObject();
                }                    
                else if (_colliding == true && manipulateOption == LevelManipulation.Destroy)
                {
                    if (_rayHit.collider.gameObject.name.Contains("Player"))
                    {
                        manager.playerPlaced = false;
                    }                        

                    Destroy(_rayHit.collider.gameObject);
                }
            }
        }
    }

    void SetRotateObject()
    {
        rotObject = _rayHit.collider.gameObject;
        manager.flipSlider.value = rotObject.transform.rotation.y;
    }

    void CreateObject()
    {
        GameObject newObj;

        // Item 1
        if (itemOption == ItemList.Item_1)
        {
            // Create object
            newObj = Instantiate(item_1);
            newObj.transform.position = SnapToGrid(_rayHit.point);
            newObj.transform.Translate(new Vector3(0.0f, -0.5f, 0.0f));
            newObj.layer = 9;

            // Add editor object component
            EditorObject eo = newObj.AddComponent<EditorObject>();
            eo.data.obPosition = newObj.transform.position;
            eo.data.obRotation = newObj.transform.rotation;
            eo.data.obType = EditorObject.ObjectType.Item_1;
        }
        // Item 2
        else if (itemOption == ItemList.Item_2)
        {
            // Create object
            newObj = Instantiate(item_2);
            newObj.transform.position = SnapToGrid(_rayHit.point);
            newObj.transform.Translate(new Vector3(0.0f, -0.5f, 0.0f));
            newObj.layer = 9;

            // Add editor object component
            EditorObject eo = newObj.AddComponent<EditorObject>();
            eo.data.obPosition = newObj.transform.position;
            eo.data.obRotation = newObj.transform.rotation;
            eo.data.obType = EditorObject.ObjectType.Item_2;
        }
        // Item 3
        else if (itemOption == ItemList.Item_3)
        {
            // Create object
            newObj = Instantiate(item_3);
            newObj.transform.position = SnapToGrid(_rayHit.point);
            newObj.transform.Translate(new Vector3(0.0f, -0.5f, 0.0f));
            newObj.layer = 9;

            // Add editor object component
            EditorObject eo = newObj.AddComponent<EditorObject>();
            eo.data.obPosition = newObj.transform.position;
            eo.data.obRotation = newObj.transform.rotation;
            eo.data.obType = EditorObject.ObjectType.Item_3;
        }
        // Item 4
        else if (itemOption == ItemList.Item_4)
        {
            // Create object
            newObj = Instantiate(item_4);
            newObj.transform.position = SnapToGrid(_rayHit.point);
            newObj.transform.Translate(new Vector3(0.0f, -0.5f, 0.0f));
            newObj.layer = 9;

            // Add editor object component
            EditorObject eo = newObj.AddComponent<EditorObject>();
            eo.data.obPosition = newObj.transform.position;
            eo.data.obRotation = newObj.transform.rotation;
            eo.data.obType = EditorObject.ObjectType.Item_4;
        }
        // Item 5
        else if (itemOption == ItemList.Item_5)
        {
            // Create object
            newObj = Instantiate(item_5);
            newObj.transform.position = SnapToGrid(_rayHit.point);
            newObj.transform.Translate(new Vector3(0.0f, -0.5f, 0.0f));
            newObj.layer = 9;

            // Add editor object component
            EditorObject eo = newObj.AddComponent<EditorObject>();
            eo.data.obPosition = newObj.transform.position;
            eo.data.obRotation = newObj.transform.rotation;
            eo.data.obType = EditorObject.ObjectType.Item_5;
        }
        // Item 6
        else if (itemOption == ItemList.Item_6)
        {
            // Create object
            newObj = Instantiate(item_6);
            newObj.transform.position = SnapToGrid(_rayHit.point);
            newObj.transform.Translate(new Vector3(0.0f, -0.5f, 0.0f));
            newObj.layer = 9;

            // Add editor object component
            EditorObject eo = newObj.AddComponent<EditorObject>();
            eo.data.obPosition = newObj.transform.position;
            eo.data.obRotation = newObj.transform.rotation;
            eo.data.obType = EditorObject.ObjectType.Item_6;
        }
        // Item 7
        else if (itemOption == ItemList.Item_7)
        {
            // Create object
            newObj = Instantiate(item_7);
            newObj.transform.position = SnapToGrid(_rayHit.point);
            newObj.transform.Translate(new Vector3(0.0f, -0.5f, 0.0f));
            newObj.layer = 9;

            // Add editor object component
            EditorObject eo = newObj.AddComponent<EditorObject>();
            eo.data.obPosition = newObj.transform.position;
            eo.data.obRotation = newObj.transform.rotation;
            eo.data.obType = EditorObject.ObjectType.Item_7;
        }
        // Item 8
        else if (itemOption == ItemList.Item_8)
        {
            // Create object
            newObj = Instantiate(item_8);
            newObj.transform.position = SnapToGrid(_rayHit.point);
            newObj.transform.Translate(new Vector3(0.0f, -0.5f, 0.0f));
            newObj.layer = 9;

            // Add editor object component
            EditorObject eo = newObj.AddComponent<EditorObject>();
            eo.data.obPosition = newObj.transform.position;
            eo.data.obRotation = newObj.transform.rotation;
            eo.data.obType = EditorObject.ObjectType.Item_8;
        }
        else if (itemOption == ItemList.Player)
        {
            // Create object
            if (manager.playerPlaced == false)
            {
                newObj = Instantiate(playerSt);
                newObj.transform.position = SnapToGrid(_rayHit.point);
                newObj.transform.Translate(new Vector3(0.0f, -0.5f, 0.0f));
                newObj.layer = 9;
                newObj.AddComponent<CapsuleCollider>();
                newObj.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
                newObj.GetComponent<CapsuleCollider>().height = 2;
                manager.playerPlaced = true;

                // Add editor object component
                EditorObject eo = newObj.AddComponent<EditorObject>();
                eo.data.obPosition = newObj.transform.position;
                eo.data.obRotation = newObj.transform.rotation;
                eo.data.obType = EditorObject.ObjectType.Player;
            }
        }
    }

    Vector3 SnapToGrid(Vector3 gridPoint)
    {
        return _gridSystem.GetClosestGridPoint(gridPoint);
    }
}
