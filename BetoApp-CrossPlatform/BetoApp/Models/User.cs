using System;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace BetoApp.Models
{
    public class User
    {
        public static string GENDER_M = "M";
        public static string GENDER_F = "F";

        public static string ROLE_PATIENT = "PATIENT";
        public static string ROLE_KEEPER = "KEEPER";

        public static string EXERCISE_NONE = "NONE";
        public static string EXERCISE_WALK = "WALK";
        public static string EXERCISE_BIKE = "BIKE";
        public static string EXERCISE_GYM = "GYM";
        public static string EXERCISE_OTHER = "OTHER";

        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string Exercise { get; set; }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(this);
            Preferences.Set("user", json);
        }

        public void Delete()
        {
            Preferences.Set("user", null);
        }

        public static User Load()
        {
            var json = Preferences.Get("user", null);
            return json == null ? null : JsonConvert.DeserializeObject<User>(json);
        }
    }
}
