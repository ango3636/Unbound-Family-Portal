namespace FamilyPortal.ServiceModel;

using System.ComponentModel.DataAnnotations;
using ServiceStack;
using ServiceStack.Model;

public class Child
{
    //[Required(ErrorMessage ="ChildId is required.")]
    public int ChildId { get; set; }
    //[Required(ErrorMessage ="First Name is required.")]
    public string FirstName { get; set; }
    //[Required(ErrorMessage ="Last Name is required")]
    public string LastName { get; set; }
    public long CreatedOn { get; set; }


    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
    public string? SecurityQuestion { get; set; }
    public string? SecurityAnswer { get; set; }


}
[Tag("Child")]
[Route("/Child", "GET")]
public class QueryChild : QueryData<Child>
{
    public int Id { get; set; }
    public List<long> Ids { get; set; }
    public string? NameContains { get; set; }
}
/* Example API request to get child





*/