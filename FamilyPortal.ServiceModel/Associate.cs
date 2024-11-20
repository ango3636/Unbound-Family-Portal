namespace FamilyPortal.ServiceModel;
using ServiceStack;
using ServiceStack.Model;

public class Associate
{
    public int AssociateID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public long CreatedOn { get; set; }

    public string? UserName { get; set; }
}

[Tag("associate")]
[Route("/associate", "GET")]
public class QueryAssociate : QueryData<Associate>
{
    public int? Id { get; set; }
    public List<long>? Ids { get; set; }
    public string? NameContains { get; set; }
}

[Tag("associate")]
[Route("/associate/update", "POST")]
public class UpdateAssociate : IPost, IReturn<Associate>
{
    [ValidateNotEmpty]
    public string FirstName { get; set; }

    [ValidateNotEmpty]
    public string LastName { get; set; }

    [ValidateNotEmpty]
    public string PhoneNumber { get; set; }

    public long CreatedOn { get; set; }
}
