using System;
using Microsoft.EntityFrameworkCore;

namespace TutorialLandonAPI.Models
{
    /// <summary>
    /// Splitting the entity model and resource model granting devs 
    /// with flexibility of control what to be returned to the user
    /// </summary>
    public class RoomEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Rate { get; set; }

    }
}
