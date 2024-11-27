namespace FamilyPortal.ServiceModel;
using ServiceStack;

public class Sponsorship
{
    public int SponsorshipID { get; set; }
    public int AssociateID { get; set; }
    public int ChildID { get; set; }
    public long CreatedOn { get; set; }
}