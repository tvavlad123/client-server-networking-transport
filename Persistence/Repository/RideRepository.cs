using Transport.Model;

namespace Transport.Repository
{
    public class RideRepository : AbstractRepository<int, Ride>
    {
        public RideRepository(IValidator<Ride> validator) : base(validator)
        {
        }
    }
}
