using ProjectCard.Game.Utilities;
using UnityEngine;

namespace ProjectCard.Game.SO
{
    [CreateAssetMenu(fileName = "ProjectCards", menuName = "ProjectCards/ThemeData")]
    public class ThemeData : ScriptableObject
    {
        public ThemeType type = ThemeType.Theme1;
        public Sprite backgroundAsset = null;
        public Color backgroundColor = Color.black;
        public Color cardColor = Color.black;
    }
}