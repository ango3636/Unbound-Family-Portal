
using FamilyPortal.Data;
using FamilyPortal.ServiceModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FamilyPortal.ServiceInterface
{
    public class SponsorService
    {
        private readonly ApplicationDbContext _context;

        public SponsorService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method to get children by associate id
        public async Task<List<SelectListItem>> GetChildrenBySponsorIdAsync(int associateId)
        {
            var children = await _context.Sponsorship
                .Where(s => s.AssociateID == associateId)
                .Join(_context.Child,
                    sponsorship => sponsorship.ChildID,
                    child => child.ChildId,
                    (sponsorship, child) => new { child.ChildId, child.FirstName, child.LastName })
                .ToListAsync();

            var selectListItems = children.Select(c => new SelectListItem
            {
                Value = c.ChildId.ToString(), // Set ChildId as the Value
                Text = $"{c.FirstName} {c.LastName}" // Display Name as Text
            }).ToList();

            return selectListItems;
        }

        public async Task<List<SelectListItem>> GetAssociatesBySponsorIdAsync(int associateId)
        {
            var associates = await _context.Associate
                .Where(a => a.AssociateID == associateId) // Try fetching directly from Associate table
                .Select(a => new { a.AssociateID, a.FirstName, a.LastName })
                .ToListAsync();


            var selectListItems = associates.Select(a => new SelectListItem
            {
                Value = a.AssociateID.ToString(),
                Text = $"{a.FirstName} {a.LastName}"
            }).ToList();


            return selectListItems;
        }

    }


}
