namespace TeacherApi.Hubs;

public interface ITeacherApiHubClient
{
    Task StudentSolutionCreated(string solutionId, string firstName, string lastName);
}