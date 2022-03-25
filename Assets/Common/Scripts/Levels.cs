using System.Collections.Generic;
using UnityEngine;


namespace NucGames.Bombs
{
   public class Levels : MonoBehaviour
   {
      [SerializeField] private List<GameObject> levelsPref;


      public void LoadRandomLvl()
      {
         Instantiate(levelsPref[Random.Range(0, levelsPref.Count - 1)], transform.position, transform.rotation);
      }
   }
}
