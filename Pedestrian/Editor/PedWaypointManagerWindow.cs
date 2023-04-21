using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PedWaypointManagerWindow : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open()
    {
        GetWindow<PedWaypointManagerWindow>();
    }

    public Transform waypointRoot;

    public void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if(waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be seleced. Please assign a root transform.", MessageType.Warning);
        } 
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    void DrawButtons()
    {
        if (GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }

        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<PedestrianWaypoint>())
        {
            if(GUILayout.Button("Add Branch Waypoint"))
            {
                CreateBranch();
            }

            if (GUILayout.Button("Create Waypoint Before"))
            {
                CreateWaypointBefore();
            }

            if (GUILayout.Button("Create Waypoint After"))
            {
                CreateWaypointAfter();
            }

            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }
        }
    }

    void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(PedestrianWaypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        PedestrianWaypoint waypoint = waypointObject.GetComponent<PedestrianWaypoint>();

        if(waypointRoot.childCount > 1)
        {
            waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<PedestrianWaypoint>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;
            //Place waypoint at the last position
            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
            waypoint.width = waypoint.previousWaypoint.width;
        }

        Selection.activeGameObject = waypoint.gameObject;
    }

    void CreateWaypointBefore()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(PedestrianWaypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        PedestrianWaypoint newWaypoint = waypointObject.GetComponent<PedestrianWaypoint>();
        PedestrianWaypoint selectedWaypoint = Selection.activeGameObject.GetComponent<PedestrianWaypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;

        if(selectedWaypoint.previousWaypoint != null)
        {
            newWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            selectedWaypoint.previousWaypoint.nextWaypoint = newWaypoint;
        }

        newWaypoint.nextWaypoint = selectedWaypoint;
        selectedWaypoint.previousWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        newWaypoint.width = selectedWaypoint.width;
        Selection.activeGameObject = newWaypoint.gameObject;
        
    }

    void CreateWaypointAfter()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(PedestrianWaypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        PedestrianWaypoint newWaypoint = waypointObject.GetComponent<PedestrianWaypoint>();
        PedestrianWaypoint selectedWaypoint = Selection.activeGameObject.GetComponent<PedestrianWaypoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.forward = selectedWaypoint.transform.forward;
        newWaypoint.previousWaypoint = selectedWaypoint;

        if(selectedWaypoint.nextWaypoint != null)
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = newWaypoint;
            newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
        }

        selectedWaypoint.nextWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        newWaypoint.width = selectedWaypoint.width;
        Selection.activeGameObject = newWaypoint.gameObject;

    }

    void RemoveWaypoint()
    {
        PedestrianWaypoint selectedWaypoint = Selection.activeGameObject.GetComponent<PedestrianWaypoint>();

        if(selectedWaypoint.nextWaypoint != null)
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
        }

        if (selectedWaypoint.previousWaypoint != null)
        {
            selectedWaypoint.previousWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
            Selection.activeGameObject = selectedWaypoint.previousWaypoint.gameObject;
        }

        DestroyImmediate(selectedWaypoint.gameObject);
    }

    void CreateBranch()
    {
        GameObject waypointObject = new GameObject("Branch " + waypointRoot.childCount, typeof(PedestrianWaypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        PedestrianWaypoint waypoint = waypointObject.GetComponent<PedestrianWaypoint>();
        PedestrianWaypoint branchedFrom = Selection.activeGameObject.GetComponent<PedestrianWaypoint>();
        branchedFrom.branches.Add(waypoint);

        waypoint.transform.position = branchedFrom.transform.position;
        waypoint.transform.forward = branchedFrom.transform.forward;
        waypoint.width = branchedFrom.width;

        Selection.activeGameObject = waypoint.gameObject;
    }
}
