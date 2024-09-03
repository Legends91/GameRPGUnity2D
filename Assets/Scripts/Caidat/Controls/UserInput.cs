using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    #region Cursor

    [SerializeField] private Texture2D _cursorTexture;
    [SerializeField] private Texture2D _clickedCursorTexture;
    private const CursorMode _cursorMode = CursorMode.Auto;
    private readonly Vector2 _hotSpot = Vector2.zero;

    private static UserInput _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetCursor(_cursorTexture);
    }

    private void Update()
    {
            SetCursor(_cursorTexture);
    }

    private void SetCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, _hotSpot, _cursorMode);
    }

    #endregion
}
