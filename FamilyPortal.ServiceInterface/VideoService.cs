using FamilyPortal.Data;
using FamilyPortal.ServiceModel;
using Microsoft.EntityFrameworkCore;

namespace FamilyPortal.ServiceInterface
{

    public class VideoService
    {

        private readonly ApplicationDbContext _context;

        public VideoService(ApplicationDbContext context)
        {
            _context = context;
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
                .Where(e => e.DigitalChildLetterID == digitalChildLetterId && e.ChildID == childId && e.IsDraft == 1)
                .FirstOrDefaultAsync();
        }
        //BY ASSOCIATEID
        public async Task<List<DigitalChildLetter>> GetDraftsByAssociateIdAsync(int associateId)
        {
            return await _context.DigitalChildLetter
                .Where(e => e.AssociateID == associateId && e.IsDraft == 1)
                .ToListAsync();
        }
        //BY DIGITALCHILD LETTER ID
        public async Task<DigitalChildLetter> GetDraftByIdAsync(int videoID)
        {
            // Query the database to find the draft with the given ELetterID
            var draft = await _context.DigitalChildLetter
                .Where(e => e.VideoID == videoID && e.IsDraft == 1)  // Ensure it's a draft
                .FirstOrDefaultAsync();  // Get the first matching draft or null
            return draft;  // Returns the draft or null if not found
        }

        public async Task<string> GetChildNameByIdAsync(int childId)
        {
            var child = await _context.Child
                                    .Where(c => c.ChildId == childId)
                                    .FirstOrDefaultAsync();

            return child != null ? $"{child.FirstName} {child.LastName}" : "Unknown";
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

        public async Task SaveVideoDraftAsync(DigitalChildLetter draft)
        {
            // Set IsDraft to true before saving
            draft.IsDraft = 1;

            _context.DigitalChildLetter.Add(draft);
            await _context.SaveChangesAsync();
        }

        public async Task SendVideoAsync(int videoId)
        {
            var video = await _context.DigitalChildLetter.FindAsync(videoId);

            if (video != null)
            {
                video.IsDraft = 0;  // Mark the letter as sent
                _context.Update(video);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateVideoDraftAsync(DigitalChildLetter draft)
        {
            // Find the existing draft from the database
            var existingDraft = await _context.DigitalChildLetter
                .Where(e => e.VideoID == draft.VideoID && e.IsDraft == 1)
                .FirstOrDefaultAsync();

            if (existingDraft != null)
            {
                // Update the properties of the existing draft
                existingDraft.FileName = draft.FileName;  // Update the letter text
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

        public async Task DeleteVideoDraftAsync(int? videoId)
        {
            var draft = await _context.DigitalChildLetter.FindAsync(videoId);
            if (draft != null && draft.IsDraft == 1)
            {
                _context.DigitalChildLetter.Remove(draft);
                await _context.SaveChangesAsync();
            }
        }



    }
}