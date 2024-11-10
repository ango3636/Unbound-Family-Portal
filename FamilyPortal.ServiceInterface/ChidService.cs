using FamilyPortal.Data;
using FamilyPortal.ServiceModel;
using Microsoft.EntityFrameworkCore;


namespace FamilyPortal.ServiceInterface
{
    public class ChildService
    {
        private readonly ApplicationDbContext _context;

        public ChildService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        // Method to check if a child exists based on ChildID
        public async Task<Child> GetChildByIdAsync(int childId)
        {
            return await _context.Child
                                 .Where(c => c.ChildId == childId)
                                 .FirstOrDefaultAsync();
        }

        // Method to check if a child exists based on FirstName and LastName
        public async Task<Child?> GetChildByNameAsync(string firstName, string lastName, int childId)
        {
            return await _context.Child
                                 .Where(c => c.FirstName == firstName && c.LastName == lastName && c.ChildId == childId)
                                 .FirstOrDefaultAsync();
        }
    }
}
