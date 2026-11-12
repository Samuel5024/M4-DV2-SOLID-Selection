using System.Collections.Generic;
using UnityEngine;

public class ResponsiveSelector : MonoBehaviour, ISelector
{
    [SerializeField] private List<Selectable> selectables;
    private Transform _selection;
    public void Check(Ray ray)
    {
        _selection = null;
        for (int i = 0; i < selectables.Count; i++)
        {
            var vector1 = ray.direction;
            var vector2 = selectables[i].transform.position - ray.origin;
            
            var lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);
            
            selectables[i].LookPercentage = lookPercentage;
        }
    }

    public Transform GetSelection()
    {
        return _selection;
    }
}