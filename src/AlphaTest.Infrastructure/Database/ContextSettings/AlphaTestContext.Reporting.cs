using Microsoft.EntityFrameworkCore;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {
        public void DisableTracking()
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}
