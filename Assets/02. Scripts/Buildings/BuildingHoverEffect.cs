using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Buildings
{
    public class BuildingHoverEffect : MonoBehaviour
    {
        [SerializeField]
        private Color newColor = default;
        

        private List<Material> buildingMaterials;
        private List<Color> buildingOriginalColors;

        private void Awake()
        {
            buildingMaterials = new List<Material>();
            buildingOriginalColors = new List<Color>();
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
                r.material = Instantiate(r.material);
                buildingMaterials.Add(r.material);
                buildingOriginalColors.Add(r.material.color);
            }
        }

        public void OnEffectStart()
        {
            for (int i = 0; i < buildingMaterials.Count; ++i)
            {
                buildingMaterials[i].SetColor("_BaseColor", newColor);
            }
        }

        public void OnEffectEnd()
        {
            for (int i = 0; i < buildingMaterials.Count; ++i)
            {
                buildingMaterials[i].SetColor("_BaseColor", buildingOriginalColors[i]);
            }
        }
    }
}
