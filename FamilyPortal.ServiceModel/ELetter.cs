namespace FamilyPortal.ServiceModel;
using ServiceStack;
using ServiceStack.Model;

public class ELetter
{
    public int ELetterID { get; set; }
    public int AssociateID { get; set; }
    public int ChildID { get; set; }

    public string? ELetterText { get; set; }
    public string? BlobID { get; set; }
    public long CreatedOn { get; set; }

    public int IsDraft { get; set; }
    public string ChildName { get; set; }
}