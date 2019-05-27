using UnityEngine;
   using DiscordPresence;
   
   public class Exemple : MonoBehaviour
   {
       public void UpdateRich()
       {
           PresenceManager.UpdatePresence(detail: "Rich presence mis à jour");
       }
   }