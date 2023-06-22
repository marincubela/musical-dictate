using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Exercises.Queries.GetExercise;

public class GetExerciseSheetDto : IMapFrom<Sheet>
{
    public string Id { get; set; }
    public string MusicXml { get; set; }
}