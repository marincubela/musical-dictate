
namespace SimpleGrader.Models;

public class Assignment
{
    private Assignment() { }
    
    public string Id { get; set; }

    public Exercise Exercise { get; set; }

    // public StudentGroup StudentGroup { get; private set; }

    public Teacher Teacher { get; private set; }
    
    public string GraderType { get; private set; }

    // public ICollection<StudentSolution> StudentSolutions { get; private set; } = new List<StudentSolution>();
}