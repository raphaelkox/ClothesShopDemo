using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityEngine.Rendering;
using UnityEditor.U2D.Sprites;

class RenameSpriteUtility : EditorWindow {
    [MenuItem("Tools/Rename Sprite")]

    public static void ShowWindow() {
        GetWindow(typeof(RenameSpriteUtility));
    }

    static Texture2D texture2D;

    void OnGUI() {
        texture2D = (Texture2D)EditorGUILayout.ObjectField(texture2D, typeof(Texture2D), false);

        if (GUILayout.Button("Rename Sprite") && texture2D) {
            RenameSprite();
        }
    }

    void RenameSprite() {
        var factory = new SpriteDataProviderFactories();
        factory.Init();
        var dataProvider = factory.GetSpriteEditorDataProviderFromObject(texture2D);
        dataProvider.InitSpriteEditorDataProvider();

        var sliceMetaData = dataProvider.GetSpriteRects();

        //idle frames
        sliceMetaData[0].name = "idle_down";
        sliceMetaData[8].name = "idle_up";
        sliceMetaData[16].name = "idle_right";
        sliceMetaData[24].name = "idle_left";

        int frame = 32;
        for (int i = 0; i < 6; i++) {
            sliceMetaData[frame].name = $"walk_down_{i}";
            sliceMetaData[frame].pivot = new Vector2(0.5f, 0.35f);
            sliceMetaData[frame].alignment = SpriteAlignment.Custom;
            frame++;
        }

        frame = 40;
        for (int i = 0; i < 6; i++) {
            sliceMetaData[frame].name = $"walk_up_{i}";
            sliceMetaData[frame].pivot = new Vector2(0.5f, 0.35f);
            sliceMetaData[frame].alignment = SpriteAlignment.Custom;
            frame++;
        }

        frame = 48;
        for (int i = 0; i < 6; i++) {
            sliceMetaData[frame].name = $"walk_right_{i}";
            sliceMetaData[frame].pivot = new Vector2(0.5f, 0.35f);
            sliceMetaData[frame].alignment = SpriteAlignment.Custom;
            frame++;
        }

        frame = 56;
        for (int i = 0; i < 6; i++) {
            sliceMetaData[frame].name = $"walk_left_{i}";
            sliceMetaData[frame].pivot = new Vector2(0.5f, 0.35f);
            sliceMetaData[frame].alignment = SpriteAlignment.Custom;
            frame++;
        }

        dataProvider.SetSpriteRects(sliceMetaData);

        dataProvider.Apply();

        // Optional: If you want to auto save your changes
        AutoSaveChanges(dataProvider);
    }


    static void AutoSaveChanges(ISpriteEditorDataProvider dataProvider) {
        var assetImporter = dataProvider.targetObject as AssetImporter;
        assetImporter.SaveAndReimport();
    }
}