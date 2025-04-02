    namespace Travels.Domain.Entities
    {
        public class User
        {
            public int Id { get; set; } = default!;
            public string Name { get; set; } = default!;
            public string Surname { get; set; } = default!;
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
            public Role Role { get; set; }
            public virtual ICollection<Review> Reviews { get; set; } = default!;
            public virtual ICollection<Reservation> Reservations { get; set; } = default!;
            public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = default!;
            public bool isActivate { get; set; } = false;
        }
    }
