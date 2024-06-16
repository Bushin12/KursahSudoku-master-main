using UnityEngine;
namespace FreeVoiceEffector
{
    public class MaterialColorControl : MonoBehaviour
    {
        public enum ColorPalette
        {
            Red,
            Orange,
            Yellow,
            Green,
            Blue,
            Indigo,
            Violet,
            Black,
            White
        }

        [SerializeField] private ColorPalette color; // 색상 선택을 위한 enum 변수

        void Start()
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = GetColorFromPalette(color);
            }
        }

        Color GetColorFromPalette(ColorPalette palette)
        {
            switch (palette)
            {
                case ColorPalette.Red:
                    return Color.red;
                case ColorPalette.Orange:
                    return new Color(1f, 0.65f, 0f); // RGB for Orange
                case ColorPalette.Yellow:
                    return Color.yellow;
                case ColorPalette.Green:
                    return Color.green;
                case ColorPalette.Blue:
                    return Color.blue;
                case ColorPalette.Indigo:
                    return new Color(0.29f, 0f, 0.51f); // RGB for Indigo
                case ColorPalette.Violet:
                    return new Color(0.58f, 0f, 0.83f); // RGB for Violet
                case ColorPalette.Black:
                    return Color.black;
                case ColorPalette.White:
                    return Color.white;
                default:
                    return Color.white;
            }
        }
    }
}