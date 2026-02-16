using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Assets.Shared
{
    [CreateAssetMenu(fileName = "Jogos", menuName = "Jogos/Jogo")]
    public class Game : ScriptableObject
    {
        [SerializeField] public string Name;
    }
}
