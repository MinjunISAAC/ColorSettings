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
                    Debug.LogError("[ColorSettings] 색상 설정 파일을 로드하지 못했습니다. Resources 폴더 안 Color Settings 파일이 존재하는지 확인하십시오.");

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
        /// Color Type에 따른 Color 정보를 받을 수 있는 함수.
        /// Color Type이 존재하더라도 ColorSetting에 Color가 등록되어 있지 않으면 동작하지 않을 수 있습니다.
        /// 색상 설정 파일은 Resources 폴더에 존재합니다.
        /// </summary>
        public Color GetColor(EColorType colorType)
        {
            if (_infoSet.TryGetValue(colorType, out var info))
                return info.TargetColor;

            if (null == _colorDataList || 0 == _colorDataList.Count)
            {
                Debug.LogError("[ColorSetting.GetColor] 색상 정보가 없습니다.");
                return default;
            }

            Debug.LogWarning($"[ColorSettings.GetColor] 해당 유형에 대한 색상 정보가 없습니다. type: {colorType}");
            return _colorDataList[0].TargetColor;
        }

        /// <summary>
        /// 컬러 종류에 따라 등록된 컬러 소재를 받을 수 있는 함수.
        /// Color Type이 존재하더라도 ColorSetting에 Color Material이 등록되어 있지 않으면 동작하지 않을 수 있습니다.
        /// 색상 설정 파일은 Resources 폴더에 존재합니다.
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
        /// Color Info를 초기화하는 함수입니다.
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
