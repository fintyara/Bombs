using UnityEngine;


namespace NucGames.Bombs
{
    public class AddToCounter : MonoBehaviour
    {
        [SerializeField] private int _count;
        public void AddCount()
        {
            Counter counter = FindObjectOfType<Counter>();
            if(counter != null)
                counter.AddCount(_count);
        }
    }
}
