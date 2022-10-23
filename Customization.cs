using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Customization
{
    public string DisplayName;

    public List<Renderer> Renderers;
    public List<Material> Materials;
    public List<GameObject> SubObjects;
    public int materialIndex;
    public int subObjectIndex;

    public void NextMaterial()
    {
        if(materialIndex == Materials.Count - 1)
        {
            materialIndex = 0;
        }
        else
        {
            materialIndex++;
        }

        UpdateRenderers();
    }

    public void PreviousMaterial()
    {
        if(materialIndex == 0)
        {
            materialIndex = Materials.Count - 1;
        }
        else
        {
            materialIndex--;
        }
        
        UpdateRenderers();
    }

    public void NextSubObject()
    {
        if(subObjectIndex == SubObjects.Count - 1)
        {
            subObjectIndex = 0;
        }
        else
        {
            subObjectIndex++;
        }

        UpdateSubObjects();
    }

    public void PreviousSubObject()
    {
        if(subObjectIndex == 0)
        {
            subObjectIndex = SubObjects.Count - 1;
        }
        else
        {
            subObjectIndex--;
        }

        UpdateSubObjects();
    }

    public void UpdateSubObjects()
    {
        for(var i = 0; i < SubObjects.Count; i++)
            if (SubObjects[i])
                SubObjects[i].SetActive(i == subObjectIndex);
    }

    public void UpdateRenderers()
    {
        foreach (var renderer in Renderers)
            if (renderer)
                renderer.material = Materials[materialIndex];
    }
}
