using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class PostProcessorCollider : AssetPostprocessor
{
    void OnPostprocessModel(GameObject g)
    {
        Apply(g.transform);
    }

    void Apply(Transform t)
    {
        if (t.name.Contains("_COL") || t.name.ToLower().Contains("collider")) {
            MeshRenderer meshRenderer = t.gameObject.GetComponent<MeshRenderer>();
            Object.DestroyImmediate(meshRenderer);

            MeshCollider mesh_collider = t.gameObject.AddComponent<MeshCollider>() as MeshCollider;
            mesh_collider.convex = false;
        }

        if (t.name.Contains("_BOX")){
            MeshRenderer meshRenderer = t.gameObject.GetComponent<MeshRenderer>();

            BoxCollider box_collider = t.gameObject.AddComponent<BoxCollider>() as BoxCollider;
            set_bounds(box_collider, meshRenderer);



            Object.DestroyImmediate(meshRenderer);
        }

        // Recurse
        foreach (Transform child in t)
            Apply(child);
    }




    void set_bounds(BoxCollider _col, Renderer _renderer){
        if (_renderer == null)  {
            return;
        }

        //_col.center = _renderer.bounds.center;
        _col.size = _renderer.bounds.size;

    }
}

#endif