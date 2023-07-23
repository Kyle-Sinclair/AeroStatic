using System.Collections.Generic;
using UnityEngine;


namespace Game_Systems.Services {
    [CreateAssetMenu(fileName = "Service Reference List", menuName = "Services/ServiceReferenceList", order = 15)]
    public class ServiceReferenceList : ScriptableObject {

        [SerializeReference] public List<GameService> MapCreationRef;

    }
}
