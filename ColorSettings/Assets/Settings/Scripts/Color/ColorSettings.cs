// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace Settings.ForColor
{
    [CreateAssetMenu(fileName = "ColorSettings", menuName = "Settings/Create Color Settings")]
    public class ColorSettings : ScriptableObject
    {
        // --------------------------------------------------
        // Singleton
        // -------------------------------------------------- 
        // ----- Constructor
        private ColorSettings() { }

        // ----- Instance
        private static ColorSettings _instance = null;

        // ----- Variables
        private const string SETTING_PATH = "Settings/ColorSettings";

        // ----- Instance Getter
        public static ColorSettings Instance
        {
            get
            {
                if (null != _instance)
                    return _instance;

                _instance = Resources.Load<ColorSettings>(SETTING_PATH);

                if (null == _instance)
                    Debug.LogError("[ColorSettings] ���� ���� ������ �ε����� ���߽��ϴ�. Resources ���� �� Color Settings ������ �����ϴ��� Ȯ���Ͻʽÿ�.");

                _instance._InitColorInfoSet();

                return _instance;
            }
        }

        // --------------------------------------------------
        // Color Data Class
        // --------------------------------------------------
        [System.Serializable]
        private class ColorData
        {
            public EColorType ColorType      = EColorType.unknown;
            public Color      TargetColor    = new Color();
            public Material   TargetColorMat = null;
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private List<ColorData> _colorDataList = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<EColorType, ColorData> _infoSet = null;

        // --------------------------------------------------
        // Function - Nomal
        // --------------------------------------------------
        /// <summary>
        /// Color Type�� ���� Color ������ ���� �� �ִ� �Լ�.
        /// Color Type�� �����ϴ��� ColorSetting�� Color�� ��ϵǾ� ���� ������ �������� ���� �� �ֽ��ϴ�.
        /// ���� ���� ������ Resources ������ �����մϴ�.
        /// </summary>
        public Color GetColor(EColorType colorType)
        {
            if (_infoSet.TryGetValue(colorType, out var info))
                return info.TargetColor;

            if (null == _colorDataList || 0 == _colorDataList.Count)
            {
                Debug.LogError("[ColorSetting.GetColor] ���� ������ �����ϴ�.");
                return default;
            }

            Debug.LogWarning($"[ColorSettings.GetColor] �ش� ������ ���� ���� ������ �����ϴ�. type: {colorType}");
            return _colorDataList[0].TargetColor;
        }

        /// <summary>
        /// �÷� ������ ���� ��ϵ� �÷� ���縦 ���� �� �ִ� �Լ�.
        /// Color Type�� �����ϴ��� ColorSetting�� Color Material�� ��ϵǾ� ���� ������ �������� ���� �� �ֽ��ϴ�.
        /// ���� ���� ������ Resources ������ �����մϴ�.
        /// </summary>
        public Material GetColorMatarial(EColorType colorType)
        {
            if (_infoSet.TryGetValue(colorType, out var info))
                return info.TargetColorMat;

            if (null == _colorDataList || 0 == _colorDataList.Count)
            {
                Debug.LogError("[ColorSettings.GetColorMaterial] There is no color information.");
                return default;
            }

            Debug.LogWarning($"[ColorSettings.GetColorMaterial] There is no color information for the corresponding type. type: {colorType}");
            return _colorDataList[0].TargetColorMat;
        }

        /// <summary>
        /// Color Info�� �ʱ�ȭ�ϴ� �Լ��Դϴ�.
        /// </summary>
        private void _InitColorInfoSet()
        {
            _infoSet?.Clear();
            _infoSet = new Dictionary<EColorType, ColorData>();

            for (int i = 0, size = _colorDataList?.Count ?? 0; i < size; ++i)
            {
                var info = _colorDataList[i];
                if (null == info)
                    continue;

                _infoSet[info.ColorType] = info;
            }
        }
    }
}
