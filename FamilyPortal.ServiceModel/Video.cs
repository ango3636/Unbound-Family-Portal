namespace FamilyPortal.ServiceModel;
using ServiceStack;
using ServiceStack.Model;

//CLASS USED FOR VIDEOS BUT CALLED 'DigitalChildLetter' TO BE CONSISTENT WITH DATABASE TABLE
public class DigitalChildLetter
{

    public int DigitalChildLetterID { get; set; }
    public int ChildID { get; set; }
    public int AssociateID { get; set; }
    public string BlobID { get; set; }
    public int VideoID { get; set; }
    public string FileName { get; set; }
    public int CreatedOn { get; set; }
    public int DraftStatus { get; set; }


}

[Tag("digitalChildLetter")]
[Route("/digitalChildLetter", "GET")]
public class QueryDigitalChildLetter : QueryData<DigitalChildLetter>
{
    public int DigitalChildLetterID { get; set; }
    public int ChildID { get; set; }
    public int AssociateID { get; set; }
    public string BlobID { get; set; }
    public string VideoID { get; set; }
    public string FileName { get; set; }
    public int CreatedOn { get; set; }

}

[Tag("digitalChildLetter")]
[Route("/digitalChildLetter/update", "POST")]
public class UpdateDigitalChildLetter : IPost, IReturn<DigitalChildLetter>
{
    [ValidateNotEmpty]
    public int DigitalChildLetterID { get; set; }

    [ValidateNotEmpty]
    public int ChildID { get; set; }

    [ValidateNotEmpty]
    public int AssociateID { get; set; }

    [ValidateNotEmpty]
    public string BlobID { get; set; }

    [ValidateNotEmpty]
    public string VideoID { get; set; }

    [ValidateNotEmpty]
    public string FileName { get; set; }

    public int CreatedOn { get; set; }

}


