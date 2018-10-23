namespace TutorialLandonAPI.Models
{
    /// <summary>
    /// Splitting the entity model and resource model granting devs 
    /// with flexibility of what to be returned to the user
    /// </summary>
    public class Room : Resource
    {
        public string Name { get; set; }

        public decimal Rate { get; set; }
    }
}
