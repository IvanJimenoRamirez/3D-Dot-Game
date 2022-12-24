using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class bolbInit : MonoBehaviour
{
    public Material green, white, blue;
    private bool init = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!init)
        {
            Renderer m_ObjectRenderer;
            //Fetch the GameObject's Renderer component
            m_ObjectRenderer = GetComponent<Renderer>();
            
            //Change the GameObject's Material Color to red
            //m_ObjectRenderer.material.color = Color.red;

            //List<Material> myMaterials = GetComponent<Renderer>().materials.ToList();

            /*Material[] materials = GetComponent<Renderer>().materials;
            GetComponent<Renderer>().materials[0] = blue;
            GetComponent<Renderer>().materials[1] = green;
            GetComponent<Renderer>().materials[2] = white;*/

            init = true;
        }
    }
}
