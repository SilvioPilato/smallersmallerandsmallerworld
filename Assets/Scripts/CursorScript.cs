using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {
    public Texture2D greenCrosshairTexture;
    public Texture2D redCrosshairTexture;
    public LayerMask pistolMask;
   
   
    SphereCollider rangeCollider;

    CursorMode cm;
    private void Start()
    {
        rangeCollider=gameObject.AddComponent<SphereCollider>();
        rangeCollider.radius= GetComponent<PistolShoot>().shootingRange;
        rangeCollider.isTrigger = true;

        cm = CursorMode.Auto;
        Cursor.SetCursor(greenCrosshairTexture, Vector2.zero, cm);
    }

    private void Update()
    {
        Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, float.PositiveInfinity, pistolMask))
        {
            Cursor.SetCursor(greenCrosshairTexture, Vector2.zero, cm);

        }
        else
        {
            Cursor.SetCursor(redCrosshairTexture, Vector2.zero, cm);
        }
    }
}
