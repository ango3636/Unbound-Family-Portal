using FamilyPortal.Data;
using FamilyPortal.ServiceModel;
using Microsoft.EntityFrameworkCore;

namespace FamilyPortal.ServiceInterface
{

    public class VideoService
    {

        private readonly ApplicationDbContext _context;

        public VideoService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        // FETCH VIDEO BY CHILDID
        public async Task<DigitalChildLetter> GetDigitalChildLetterByChildIdAsync(int ChildID)
        {
            return await _context.DigitalChildLetter
                                            .Where(a => a.ChildID == ChildID)
                                            .FirstOrDefaultAsync();
        }

        //FETCHING DRAFTS---------------------------------------------
        //BY DIGITALCHILDID AND CHILDID
        public async Task<DigitalChildLetter> GetDraftByIdAndChildAsync(int digitalChildLetterId, int childId)
        {
            // Fetch the draft for the associate and child by checking IsDraft
            return await _context.DigitalChildLetter
                .Where(e => e.DigitalChildLetterID == digitalChildLetterId && e.ChildID == childId && e.DraftStatus == 1)
                .FirstOrDefaultAsync();
        }
        //BY ASSOCIATEID
        public async Task<List<DigitalChildLetter>> GetDraftsByAssociateIdAsync(int associateId)
        {
            return await _context.DigitalChildLetter
                .Where(e => e.AssociateID == associateId && e.DraftStatus == 1)
                .ToListAsync();
        }
        //BY DIGITALCHILD LETTER ID
        public async Task<DigitalChildLetter> GetDraftByIdAsync(int videoID)
        {
            // Query the database to find the draft with the given ELetterID
            var draft = await _context.DigitalChildLetter
                .Where(e => e.VideoID == videoID && e.DraftStatus == 1)  // Ensure it's a draft
                .FirstOrDefaultAsync();  // Get the first matching draft or null
            return draft;  // Returns the draft or null if not found
        }
        // //BY CHILD NAME -- CAN ONLY IMPLEMENT WHEN CHILD.CS TABLE HAS BEEN PULLED
        // public async Task<string> GetChildNameByIdAsync(int childId)
        // {
        //     var child = await _context.Child
        //                             .Where(c => c.ChildID == childId)
        //                             .FirstOrDefaultAsync();
        //     return child != null ? $"{child.FirstName} {child.LastName}" : "Unknown";
        // }
        //----------------------------------------------------------------

        //SAVING, DELETING, AND SENDING
        public async Task SaveVideoDraftAsync(DigitalChildLetter videoDraft)
        {
            // Set IsDraft to true before saving
            videoDraft.DraftStatus = 1;
            _context.DigitalChildLetter.Add(videoDraft);
            await _context.SaveChangesAsync();
        }

        public async Task SendVideoAsync(int DigitalChildLetterID)
        {
            var letter = await _context.DigitalChildLetter.FindAsync(DigitalChildLetterID);
            if (letter != null)
            {
                letter.DraftStatus = 0;  // Mark the letter as sent
                _context.Update(letter);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDraftAsync(int digitalChildLetterId)
        {
            var draft = await _context.DigitalChildLetter.FindAsync(digitalChildLetterId);
            if (draft != null && draft.DraftStatus == 1)
            {
                _context.DigitalChildLetter.Remove(draft);
                await _context.SaveChangesAsync();
            }
        }



    }
}