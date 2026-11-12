﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;

    private void Awake()
    {
        SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void Update()
    {
        // Deselection/Selection Response
        if (_selection != null)
        {
            var selection = _selection;
            OnDeselect(selection);
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
            var selection = _selection;
            OnSelect(selection);
        }
    }

    private void OnSelect(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        if (selectionRenderer != null)
        {
            selectionRenderer.material = highlightMaterial;
        }
    }

    private void OnDeselect(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        if (selectionRenderer != null)
        {
            selectionRenderer.material = defaultMaterial;
        }
    }
}