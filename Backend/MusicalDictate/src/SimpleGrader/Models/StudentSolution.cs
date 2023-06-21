using System;

namespace SimpleGrader.Models;

public class StudentSolution
{
    private StudentSolution() { }
    public string Id { get; set; }

    public Student Student { get; set; }

    public Assignment Assignment { get; set; }
    public Sheet Solution { get; set; }
    
    public Result? Result { get; set; }
}