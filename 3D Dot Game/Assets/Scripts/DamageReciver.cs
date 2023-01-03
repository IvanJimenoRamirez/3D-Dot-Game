using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciver : MonoBehaviour
{
    // Material
    public Material defaultMaterial;

    private List<Material> realMaterials;
    private Renderer[] renderers;
    
    // Start is called before the first frame update
    void Start()
    {
        realMaterials = new List<Material>();

        // Get a list of all the materials from the object the script is attached
        renderers = GetComponentsInChildren<Renderer>();
        
        foreach (Renderer renderer in renderers)
        {
            realMaterials.Add(renderer.material);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material = defaultMaterial;
        }
        Invoke("restoreOldMaterial", 0.1f);
    }

    
    private void restoreOldMaterial()
    {
        // Restore the old materials
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = realMaterials[i];
        }
    }
}
