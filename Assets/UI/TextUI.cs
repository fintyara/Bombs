using UnityEngine;
using UnityEngine.UI;


namespace NucGames.Bombs
{
    public class TextUI : MonoBehaviour
    {
        [SerializeField] private Text _text;
        
        
        public void UpdateText(int count)
        {
            _text.text = count.ToString();
        }
    }
}
