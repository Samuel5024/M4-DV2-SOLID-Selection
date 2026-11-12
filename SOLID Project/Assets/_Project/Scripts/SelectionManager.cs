﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
   
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
        // Deselection/Selection Response
        if (_selection != null)
        {
            _selectionResponse.OnDeselect(_selection);
        }

        #region MyRegion

        // Creating a Ray
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
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

        #endregion
        
        // Deselection/Selection Response
        if (_selection != null)
        {
            _selectionResponse.OnSelect(_selection);
        }
    }
}