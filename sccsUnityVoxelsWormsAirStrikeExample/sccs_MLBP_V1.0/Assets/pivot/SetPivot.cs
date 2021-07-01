/*SetPivot
From Unify Community Wiki
Jump to: navigation, search
Author: Yilmaz Kiymaz(VoxelBoy).

Warning Fixed: Max70.

Contents
1 Description
2 Usage
3 Known Limitations
4 C# - SetPivot.cs
5 Alternatives
Description
This Editor Script provides a solution to move the pivots of Game Objects within the Unity Editor.

Usage
Place the script in the Editor folder under Assets(Project view of the Editor.) The Set Pivot menu item will appear under the GameObject menu.

Known Limitations
1. You can only move the pivot inside the bounds(aka bounding box) of the mesh.It should take little modification to change this though.

2. Pivot movement doesn’t work well with objects that have been rotated.Remember to move the pivot when the object has zero rotation on all three axes.


C# - SetPivot.cs*/
/*
 * Version: 1.0
 * Author:  Yilmaz Kiymaz (@VoxelBoy)
 * Purpose: To be able to change the pivot of Game Objects
 * 			without needing to use a separate 3D application.
 * License: Free to use and distribute, in both free and commercial projects.
 * 			Do not try to sell as your own work. Simply put, play nice 
 * Contact: VoxelBoy on Unity Forums
 */
 
/*
 * TODO:
 * - Doesn't work properly with rotated objects.
 * - Can't compensate for the positioning of Mesh Colliders.
 * - Need to figure out if the "Instantiating mesh" error in Editor is a big issue, if not, how to supress it.
 * - Allowing the pivot to move outside the bounds of the mesh, ideally using the movement gizmo but only affecting the pivot.
 */
 
using UnityEngine;
using UnityEditor;
 
public class SetPivot : EditorWindow
{

    Vector3 p; //Pivot value -1..1, calculated from Mesh bounds
    Vector3 last_p; //Last used pivot

    GameObject obj; //Selected object in the Hierarchy
    MeshFilter meshFilter; //Mesh Filter of the selected object
    Mesh mesh; //Mesh of the selected object
    Collider col; //Collider of the selected object

    bool pivotUnchanged; //Flag to decide when to instantiate a copy of the mesh

    [MenuItem("GameObject/Set Pivot")] //Place the Set Pivot menu item in the GameObject menu
    static void Init()
    {
        SetPivot window = (SetPivot)EditorWindow.GetWindow(typeof(SetPivot));
        window.RecognizeSelectedObject(); //Initialize the variables by calling RecognizeSelectedObject on the class instance
        window.Show();
    }

    void OnGUI()
    {
        if (obj)
        {
            if (mesh)
            {
                p.x = EditorGUILayout.Slider("X", p.x, -1.0f, 1.0f);
                p.y = EditorGUILayout.Slider("Y", p.y, -1.0f, 1.0f);
                p.z = EditorGUILayout.Slider("Z", p.z, -1.0f, 1.0f);
                if (p != last_p)
                { //Detects user input on any of the three sliders
                  //Only create instance of mesh when user changes pivot
                    if (pivotUnchanged) mesh = meshFilter.sharedMesh; pivotUnchanged = false;
                    UpdatePivot();
                    last_p = p;
                }
                if (GUILayout.Button("Center"))
                { //Set pivot to the center of the mesh bounds
                  //Only create instance of mesh when user changes pivot
                    if (pivotUnchanged) mesh = meshFilter.sharedMesh; pivotUnchanged = false;
                    p = Vector3.zero;
                    UpdatePivot();
                    last_p = p;
                }
                GUILayout.Label("Bounds " + mesh.bounds.ToString());
            }
            else
            {
                GUILayout.Label("Selected object does not have a Mesh specified.");
            }
        }
        else
        {
            GUILayout.Label("No object selected in Hierarchy.");
        }
    }

    //Achieve the movement of the pivot by moving the transform position in the specified direction
    //and then moving all vertices of the mesh in the opposite direction back to where they were in world-space
    void UpdatePivot()
    {
        Vector3 diff = Vector3.Scale(mesh.bounds.extents, last_p - p); //Calculate difference in 3d position
        obj.transform.position -= Vector3.Scale(diff, obj.transform.localScale); //Move object position
                                                                                 //Iterate over all vertices and move them in the opposite direction of the object position movement
        Vector3[] verts = mesh.vertices;
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] += diff;
        }
        mesh.vertices = verts; //Assign the vertex array back to the mesh
        mesh.RecalculateBounds(); //Recalculate bounds of the mesh, for the renderer's sake
                                  //The 'center' parameter of certain colliders needs to be adjusted
                                  //when the transform position is modified
        if (col)
        {
            if (col is BoxCollider)
            {
                ((BoxCollider)col).center += diff;
            }
            else if (col is CapsuleCollider)
            {
                ((CapsuleCollider)col).center += diff;
            }
            else if (col is SphereCollider)
            {
                ((SphereCollider)col).center += diff;
            }
        }
    }

    //Look at the object's transform position in comparison to the center of its mesh bounds
    //and calculate the pivot values for xyz
    void UpdatePivotVector()
    {
        Bounds b = mesh.bounds;
        Vector3 offset = -1 * b.center;
        p = last_p = new Vector3(offset.x / b.extents.x, offset.y / b.extents.y, offset.z / b.extents.z);
    }

    //When a selection change notification is received
    //recalculate the variables and references for the new object
    void OnSelectionChange()
    {
        RecognizeSelectedObject();
    }

    //Gather references for the selected object and its components
    //and update the pivot vector if the object has a Mesh specified
    void RecognizeSelectedObject()
    {
        Transform t = Selection.activeTransform;
        obj = t ? t.gameObject : null;
        if (obj)
        {
            meshFilter = obj.GetComponent(typeof(MeshFilter)) as MeshFilter;
            mesh = meshFilter ? meshFilter.sharedMesh : null;
            if (mesh)
                UpdatePivotVector();
            col = obj.GetComponent(typeof(Collider)) as Collider;
            pivotUnchanged = true;
        }
        else
        {
            mesh = null;
        }
    }
}
/*
Alternatives
PivotManager - simplified version of this script

Retrieved from "http://wiki.unity3d.com/index.php?title=SetPivot&oldid=16354"
Categories:
Wizard
ScriptableObject
C Sharp
Personal tools
Log in / create account
Namespaces
Page
Discussion
Variants
Views
Read
View source
View history
Actions
Search


Navigation
Main Page
Extensions
Particle Library
Programming
Scripts
Shaders
Wizards
Extras
Tips, Tricks, Tools
Tutorials
Unity Projects
Contests
IRC Chatroom
Quick Links
Community portal
Recent changes
New Pages
Help
Toolbox
What links here
Related changes
Special pages
Printable version
Permanent link
This page was last modified on 13 February 2013, at 14:33.
This page has been accessed 57,166 times.
Content is available under Creative Commons Attribution Share Alike.
Privacy policy
About Unify Community Wiki
Disclaimers
Creative Commons Attribution Share Alike
Powered by MediaWiki*/