﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    private IRayProvider _rayProvider;
    private ISelectionResponse _selectionResponse;
    
    private Transform _selection;

    private void Awake()
    {
        _selectionResponse = GetComponent<ISelectionResponse>();
        SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (_selection != null) _selectionResponse.OnDeselect(_selection);

        // Creating a Ray
        var ray = _rayProvider.CreateRay();
        
        // Selection Determination
        _selection = null;
        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
            }
        }
        
        if (_selection != null) _selectionResponse.OnSelect(_selection);
    }
}

public interface IRayProvider
{
    Ray CreateRay();
}

public class MouseScreenRayProvider : MonoBehaviour, IRayProvider
{
    public Ray CreateRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}