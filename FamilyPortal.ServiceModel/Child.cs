namespace FamilyPortal.ServiceModel;

using System.ComponentModel.DataAnnotations;
using ServiceStack;
using ServiceStack.Model;

public class Child
{
[Required(ErrorMessage ="ChildId is required.")]
public int ChildId {get; set;}
[Required(ErrorMessage ="First Name is required.")]
public string FirstName {get; set;}
[Required(ErrorMessage ="Last Name is required")]
public string LastName {get; set;}



/* Add a string version for binding purposes
    public string ChildIdString
    {
        get => ChildId.ToString(); // Convert int to string when binding
        set
        {
            if (int.TryParse(value, out var parsedId))
            {
                ChildId = parsedId; // Convert back to int on submission
            }
            else
            {
                ChildId = 0; // Default to 0 if conversion fails
            }
        }
    }*/
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