using FamilyPortal.Data;
using FamilyPortal.ServiceModel;
using Microsoft.EntityFrameworkCore;

namespace FamilyPortal.ServiceInterface
{
    public class AssociateService
    {
        private readonly ApplicationDbContext _context;

        public AssociateService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Associate> GetAssociateByAssociateIdAsync(int associateId)
        {
            return await _context.Associate
                                .Where(a => a.AssociateID == associateId)
                                .FirstOrDefaultAsync();
        }

        public async Task UpdateAssociateAsync(Associate associate)
        {
            var existingAssociate = await _context.Associate.FindAsync(associate.AssociateID);
            if (existingAssociate != null)
            {
                existingAssociate.FirstName = associate.FirstName;
                existingAssociate.LastName = associate.LastName;
                existingAssociate.PhoneNumber = associate.PhoneNumber;

                _context.Entry(associate).State = EntityState.Modified;
                _context.Associate.Update(existingAssociate);
                await _context.SaveChangesAsync();
            }
        }
    }
}