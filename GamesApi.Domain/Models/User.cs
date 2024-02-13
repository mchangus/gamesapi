using System.Text.Json.Serialization;

namespace Games.Core.Models
{
    /// <summary>
    /// The User Class
    /// </summary>
    [Serializable]
    public class User
    {
        private int _userId;

        [JsonPropertyName("userId")]

        public int UserId 
        {
            get { return _userId; } 
            set { _userId = value; } 
        }

        public HashSet<Game> Games { get; init; } = new HashSet<Game>();


        public void Increment()
        {            
            // From MSDN: Increments a specified variable and stores the result, as an atomic operation.
            Interlocked.Increment(ref _userId);
        }

        public User CloneEmpty() 
        {
            return new User
            {
                _userId = _userId,
                Games = new HashSet<Game>(),
            };
        }

    }
}