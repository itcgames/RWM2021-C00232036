using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField]
    private float _size = 1.0f;

    public Vector3 GetClosestGridPoint(Vector3 pos)
    {
        pos -= transform.position;
        int x_c = Mathf.RoundToInt(pos.x / _size);
        int y_c = Mathf.RoundToInt(pos.y / _size);
        int z_c = Mathf.RoundToInt(pos.z / _size);
        Vector3 result = new Vector3((float)x_c * _size, (float)y_c * _size, (float)z_c * _size);
        result += transform.position;

        return result;
    }

    private void Start()
    {
        GameObject newObj;

        for (float x = 0; x < 99; x += _size)
        {
            for (float y = 0; y < 39; y += _size)
            {
                var point = GetClosestGridPoint(new Vector3(x - 49, y - 19, 0.0f));
                newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                newObj.transform.localScale = new Vector3(-0.1f, -0.1f, -0.1f);
                var newObjRenderer = newObj.GetComponent<Renderer>();
                newObjRenderer.material.SetColor("_Color", Color.red);
                newObj.transform.Translate(point);
            }
        }
    }
}
