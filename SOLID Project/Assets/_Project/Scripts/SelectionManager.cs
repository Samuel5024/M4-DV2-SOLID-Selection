﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    private IRayProvider _rayProvider;
    private ISelector _selector;
    private ISelectionResponse _selectionResponse;
    
    private Transform _currentselection;

    private void Awake()
    {
        _rayProvider = GetComponent<IRayProvider>();
        _selector = GetComponent<ISelector>();
        _selectionResponse = GetComponent<ISelectionResponse>();
        SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (_currentselection != null) _selectionResponse.OnDeselect(_currentselection);
        
        _selector.Check(_rayProvider.CreateRay());
        _currentselection = _selector.GetSelection();
        
        if (_currentselection != null) _selectionResponse.OnSelect(_currentselection);
    }
}