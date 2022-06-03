using UnityEngine;

public class ConvertToSkinnedMesh : MonoBehaviour
{
    [ContextMenu("Convert to skinned mesh")]
    void Convert()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        var text = meshRenderer.sharedMaterials;
        DestroyImmediate(meshRenderer);

        SkinnedMeshRenderer skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();

        skinnedMeshRenderer.sharedMaterials = text;
        skinnedMeshRenderer.sharedMesh = meshFilter.sharedMesh;

        DestroyImmediate(meshFilter);
        DestroyImmediate(this);
    }
}
