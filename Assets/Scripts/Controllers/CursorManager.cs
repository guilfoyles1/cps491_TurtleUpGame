using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursor;
    [SerializeField] Texture2D clickCursor;
    [SerializeField] Vector2 cursorHotspot;

    private bool isDragging = false;
    private Vector3 initialClickPos;
    [SerializeField] float dragThreshold = 5f; // Pixels moved to count as a drag

    void Start()
    {
        cursorHotspot = new Vector2(5, 5); // Adjust as needed
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialClickPos = Input.mousePosition;
            isDragging = false;
        }

        if (Input.GetMouseButton(0))
        {
            // Check if the cursor moved enough to count as a drag
            if (!isDragging && Vector3.Distance(Input.mousePosition, initialClickPos) > dragThreshold)
            {
                isDragging = true;
                Cursor.SetCursor(clickCursor, cursorHotspot, CursorMode.Auto);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Reset cursor back on release
            Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
            isDragging = false;
        }
    }
}
