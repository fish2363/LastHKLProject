using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolManagerEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset _visualAsset = default;

    [MenuItem("Tools/PoolManager")]
    public static void ShowWindow()
    {
        PoolManagerEditor wnd = GetWindow<PoolManagerEditor>();
        wnd.titleContent = new GUIContent("PoolManager");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        _visualAsset.CloneTree(root);
    }
}
