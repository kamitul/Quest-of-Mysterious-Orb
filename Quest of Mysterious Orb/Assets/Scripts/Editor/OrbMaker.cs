﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OrbMaker : EditorWindow
{
    GameObject gameObject;
    string gameObjectName;
    Editor gameObjectEditor;
    Data gameData;
    OrbType orbType;    

    [MenuItem("Window/OrbMaker")]
    static void Init()
    {
        OrbMaker window = (OrbMaker)EditorWindow.GetWindow(typeof(OrbMaker));
        window.Show();
    }

    void OnGUI()
    {
        gameObjectName = (string) EditorGUILayout.TextField(gameObjectName);
        gameObject = (GameObject) EditorGUILayout.ObjectField(gameObject, typeof(GameObject), true);

        GUIStyle bgColor = new GUIStyle();
        bgColor.normal.background = EditorGUIUtility.whiteTexture;

        if (gameObject != null)
        {
            if (gameObjectEditor == null)
                gameObjectEditor = Editor.CreateEditor(gameObject);

            gameObjectEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(256, 256), bgColor);
        }

        orbType = (OrbType)EditorGUILayout.EnumPopup("Orb Type: ", orbType);

        if (GUILayout.Button("Parse"))
            ParseOrb(orbType);

        
    }

    private void ParseOrb(OrbType orbType) {
        if (gameData != null)
        {
            switch(orbType) {
                case OrbType.BOUNCE:
                    gameData = (BounceOrbData) EditorGUILayout.ObjectField(gameData, typeof(BounceOrbData), false);
                    InstantiateOrb<BounceOrbData>(gameObjectName);
                    break;
                case OrbType.CHARGING:
                    gameData = (ChargingOrbData) EditorGUILayout.ObjectField(gameData, typeof(ChargingOrbData), false);
                    InstantiateOrb<ChargingOrbData>(gameObjectName);
                    break;
                case OrbType.SPIRAL:
                    gameData = (SprialOrbData) EditorGUILayout.ObjectField(gameData, typeof(SprialOrbData), false);
                    InstantiateOrb<SprialOrbData>(gameObjectName);
                    break;
            }
        }
    }

    private T InstantiateOrb<T>(string gameObjectName) 
    where T : Data
    {
        T asset = ScriptableObject.CreateInstance<T>();

        AssetDatabase.CreateAsset(asset, "Assets/" + gameObjectName + ".asset");
        AssetDatabase.SaveAssets();

        return asset;
    }
}
