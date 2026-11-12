﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    private IRayProvider _rayProvider;
    private ISelectionResponse _selectionResponse;
    
    private Transform _currentselection;
    private Transform _selection;

    private void Awake()
    {
        _selectionResponse = GetComponent<ISelectionResponse>();
        SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (_currentselection != null) _selectionResponse.OnDeselect(_currentselection);

        // Creating a Ray
        var ray = _rayProvider.CreateRay();
        
        // Selection Determination
        Check(ray);

        _currentselection = _selection;
        
        if (_currentselection != null) _selectionResponse.OnSelect(_currentselection);
    }

    public void Check(Ray ray)
    {
        _selection = null;
        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
            }
        }
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