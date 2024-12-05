namespace FamilyPortal.ServiceModel;
using ServiceStack;

public class DigitalChildLetter
{
    public int DigitalChildLetterID { get; set; }
    public int ChildID { get; set; }
    public int AssociateID { get; set; }
    public string? BlobID { get; set; }
    public string? VideoID { get; set; }
    public string? FileName { get; set; }
    public long CreatedOn { get; set; }
}