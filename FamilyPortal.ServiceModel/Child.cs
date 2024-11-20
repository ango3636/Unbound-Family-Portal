namespace FamilyPortal.ServiceModel;
using ServiceStack;

public class Child
{
    public int ChildId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long CreatedOn { get; set; }
}