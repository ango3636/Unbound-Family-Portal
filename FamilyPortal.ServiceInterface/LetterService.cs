
using FamilyPortal.Data;
using FamilyPortal.ServiceModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FamilyPortal.ServiceInterface
{
    public class LetterService
    {
        private readonly ApplicationDbContext _context;


        public LetterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveDraftAsync(ELetter draft)
        {
            // Set IsDraft to true before saving
            draft.IsDraft = 1;

            _context.ELetter.Add(draft);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDraftAsync(ELetter draft)
        {
            // Find the existing draft from the database
            var existingDraft = await _context.ELetter
                .Where(e => e.ELetterID == draft.ELetterID && e.IsDraft == 1)
                .FirstOrDefaultAsync();

            if (existingDraft != null)
            {
                // Update the properties of the existing draft
                existingDraft.ELetterText = draft.ELetterText;  // Update the letter text
                existingDraft.BlobID = draft.BlobID;  // Update the photos (if any)
                existingDraft.CreatedOn = DateTimeOffset.UtcNow.ToUnixTimeSeconds();  // Update creation date if needed

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            else
            {
                // Handle the case where the draft wasn't found (optional)
                throw new InvalidOperationException("Draft not found or is already sent.");
            }
        }
        public async Task<ELetter> GetDraftByAssociateAndChildAsync(int associateId, int childId)
        {
            // Fetch the draft for the associate and child by checking IsDraft
            return await _context.ELetter
                .Where(e => e.AssociateID == associateId && e.ChildID == childId && e.IsDraft == 1)
                .FirstOrDefaultAsync();
        }

        public async Task SendLetterAsync(int letterId)
        {
            var letter = await _context.ELetter.FindAsync(letterId);

            if (letter != null)
            {
                letter.IsDraft = 0;  // Mark the letter as sent
                _context.Update(letter);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ELetter>> GetDraftsByAssociateIdAsync(int associateId)
        {
            return await _context.ELetter
                .Where(e => e.AssociateID == associateId && e.IsDraft == 1)
                .ToListAsync();
        }

        public async Task<ELetter> GetDraftByIdAsync(int eLetterId)
        {
            // Query the database to find the draft with the given ELetterID
            var draft = await _context.ELetter
                .Where(e => e.ELetterID == eLetterId && e.IsDraft == 1)  // Ensure it's a draft
                .FirstOrDefaultAsync();  // Get the first matching draft or null

            return draft;  // Returns the draft or null if not found
        }

        public async Task DeleteDraftAsync(int letterId)
        {
            var draft = await _context.ELetter.FindAsync(letterId);
            if (draft != null && draft.IsDraft == 1)
            {
                _context.ELetter.Remove(draft);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> GetChildNameByIdAsync(int childId)
        {
            var child = await _context.Child
                                    .Where(c => c.ChildId == childId)
                                    .FirstOrDefaultAsync();

            return child != null ? $"{child.FirstName} {child.LastName}" : "Unknown";
        }

        //returns letters that are not drafts
        public async Task<List<ELetter>> GetLettersByChildId(int childId)
        {
            var letters = await _context.ELetter
                                    .Where(e => e.ChildID == childId && e.IsDraft == 0)
                                    .ToListAsync();

            return letters != null ? letters : null;
        }

    }

}