// using System.Collections.Generic;
// using MTAssets.SkinnedMeshCombiner;
// // using Sirenix.OdinInspector;
// using UnityEngine;

// public class DynamicMergeMesh : MonoBehaviour
// {
//     public SkinnedMeshRenderer[] skinnedMeshRenderers;
//     public MeshFilter[] meshFilters;
//     public List<Transform> bones;
//     public Transform rootBones;
//     public SkinnedMeshCombiner someSMeshCombiner;

//     private void Awake()
//     {
//         // // 准备骨骼数据
//         // bones = new List<Transform>();
//         // bones.Add(rootBones);
//         // bones.AddRange(rootBones.GetComponentsInChildren<Transform>().ToList());
//     }
//     public void GetSkinnedMeshRenderers()
//     {
//         skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
//     }

//     // [Button]
//     public void MergeMesh()
//     {
//         if (someSMeshCombiner == null)
//         {
//             someSMeshCombiner = gameObject.AddComponent<SkinnedMeshCombiner>();
//         }
//         // GetSkinnedMeshRenderers();
//         // List<Material> materials = new();
//         // var combineInstances = new List<CombineInstance>();
//         // var curbones = new List<Transform>();
//         // var boneNames = new HashSet<string>();
//         // foreach (var smr in skinnedMeshRenderers)
//         // {
//         //     materials.AddRange(smr.sharedMaterials);

//         //     for (int sub = 0; sub < smr.sharedMesh.subMeshCount; sub++)
//         //     {
//         //         CombineInstance ci = new CombineInstance();
//         //         ci.mesh = smr.sharedMesh;
//         //         ci.subMeshIndex = sub;
//         //         combineInstances.Add(ci);
//         //     }

//         //     // Add bones to curbones and track unique bone names
//         //     foreach (Transform b in smr.bones)
//         //     {
//         //         var t = bones.Find(t => b.name.Equals(t.name));
//         //         if (t != null && !boneNames.Contains(t.name))
//         //         {
//         //             curbones.Add(t);
//         //             boneNames.Add(t.name);
//         //         }
//         //     }
//         // }


//         // // Create a new SkinnedMeshRenderer and combine meshes
//         // var newSkinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
//         // newSkinnedMeshRenderer.sharedMesh = new Mesh();
//         // newSkinnedMeshRenderer.sharedMesh.CombineMeshes(combineInstances.ToArray(), true, false);
//         // newSkinnedMeshRenderer.bones = curbones.ToArray();
//         // newSkinnedMeshRenderer.materials = materials.ToArray();
//         // // Set the root bone
//         // newSkinnedMeshRenderer.rootBone = rootBones;
//         someSMeshCombiner.CombineMeshes();

//     }
// }
