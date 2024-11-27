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

        // Method to check if an associate exists based on FirstName, LastName, and ID [Register page]
        public async Task<Associate> GetAssociateByNameAsync(string firstName, string lastName, int associateId)
        {
            return await _context.Associate
                                 .Where(a => a.FirstName == firstName && a.LastName == lastName && a.AssociateID == associateId)
                                 .FirstOrDefaultAsync();
        }

        // Method to check if an associate exists based on Login Info [Register page]
        public async Task<Associate> GetAssociateByLoginInfoAsync(string userName, string passwordHash)
        {
            return await _context.Associate
                                 .Where(a => a.UserName == userName && a.PasswordHash == passwordHash)
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

        //Method to save account details for Associate [Register page]
        public async Task UpdateAssociateAccountAsync(Associate associate)
        {
            var existingAssociate = await _context.Associate.FindAsync(associate.AssociateID);
            if (existingAssociate != null)
            {
            //update account setup details
            existingAssociate.UserName= associate.UserName?.Trim();
            existingAssociate.PasswordHash= associate.PasswordHash;
            existingAssociate.SecurityQuestion=associate.SecurityQuestion;
            existingAssociate.SecurityAnswer=associate.SecurityAnswer;

            
            _context.Associate.Update(existingAssociate);
            await _context.SaveChangesAsync();


            }

        }
    }
}