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
        public async Task<Child> GetChildByNameAsync(string firstName, string lastName, int childId)
        {
            return await _context.Child
                                 .Where(c => c.FirstName == firstName && c.LastName == lastName && c.ChildId == childId)
                                 .FirstOrDefaultAsync();
        }

        // Method to check if a child exists based on Login Info [Register page]
        public async Task<Child> GetChildByLoginInfoAsync(string userName, string passwordHash)
        {
            return await _context.Child
                                 .Where(c => c.UserName == userName && c.PasswordHash == passwordHash)
                                 .FirstOrDefaultAsync();
        }

        //Method to save account details for child
        public async Task UpdateChildAsync(Child child)
        {
            var existingChild = await _context.Child.FindAsync(child.ChildId);
            if (existingChild != null)
            {
                //update account setup details
            existingChild.UserName= child.UserName?.Trim();
            existingChild.PasswordHash= child.PasswordHash;
            existingChild.SecurityQuestion=child.SecurityQuestion;
            existingChild.SecurityAnswer=child.SecurityAnswer;

            
            _context.Child.Update(existingChild);
            await _context.SaveChangesAsync();


            }

            
        }
        public async Task<Child> GetChildByUsernameAsync(string userName)
        {
            return await _context.Child
                                .Where(c => c.UserName == userName)
                                .FirstOrDefaultAsync();
        }


        
    }
}
