﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Artngame.CommonTools.Cables
{
    [CustomEditor(typeof(ProceduralCable))]
    public class ProceduralCableInspector : Editor
    {

        ProceduralCable proceduralCable;

        public void OnEnable()
        {
            proceduralCable = (ProceduralCable)target;
            Undo.undoRedoPerformed += () => { proceduralCable.UpdateObject(); };
        }

        public override void OnInspectorGUI()
        {
            proceduralCable.drawEditorLines = EditorGUILayout.Toggle("Draw lines ", proceduralCable.drawEditorLines);

            EditorGUI.BeginChangeCheck();
            float newCurvature = EditorGUILayout.FloatField("Curvature", proceduralCable.curvature);
            int newStep = EditorGUILayout.IntField("Step", proceduralCable.step);
            int newRadiusStep = EditorGUILayout.IntField("Radius step", proceduralCable.radiusStep);
            float newRadius = EditorGUILayout.FloatField("Radius", proceduralCable.radius);
            Vector2 newUvMultiply = EditorGUILayout.Vector2Field("UV Multiply", proceduralCable.uvMultiply);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(proceduralCable, "Change parameter");

                proceduralCable.curvature = newCurvature;

                newStep = newStep < 1 ? 1 : newStep;
                proceduralCable.step = newStep;

                newRadiusStep = newRadiusStep < 3 ? 3 : newRadiusStep;
                proceduralCable.radiusStep = newRadiusStep;

                newRadius = newRadius < 0 ? 0 : newRadius;
                proceduralCable.radius = newRadius;

                proceduralCable.uvMultiply = newUvMultiply;

                proceduralCable.UpdateObject();

                EditorUtility.SetDirty(proceduralCable);
            }
        }

        private void OnSceneGUI()
        {
            int step = proceduralCable.step;

            EditorGUI.BeginChangeCheck();

            Vector3 newAposition = Handles.DoPositionHandle(proceduralCable.a, Quaternion.identity);
            Vector3 newBposition = Handles.DoPositionHandle(proceduralCable.b, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(proceduralCable, "Change position of extremity point");
                proceduralCable.a = newAposition;
                proceduralCable.b = newBposition;
                proceduralCable.UpdateObject();
                EditorUtility.SetDirty(proceduralCable);
            }

            if (proceduralCable.drawEditorLines)
            {
                for (int i = 0; i < step; i++)
                {
                    Handles.DrawLine(proceduralCable.PointPosition(i), proceduralCable.PointPosition(i + 1));
                    DrawVerticesForPoint(i);
                }
                Handles.DrawPolyLine(proceduralCable.VerticesForPoint(step));
                DrawVerticesForPoint(step);
            }



        }

        private void DrawVerticesForPoint(int i)
        {
            Vector3[] verticesForPoint = proceduralCable.VerticesForPoint(i);

            for (int h = 0; h < verticesForPoint.Length - 1; h++)
                Handles.DrawLine(verticesForPoint[h], verticesForPoint[h + 1]);

            Handles.DrawLine(verticesForPoint[proceduralCable.radiusStep - 1], verticesForPoint[0]);
        }

    }
}