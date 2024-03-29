import { useNavigate, useParams } from "react-router-dom"
import { SmallTitle, Title } from "../components/Title"
import { useEffect, useState } from "react";
import StudentSolutionsService from "../api/services/StudentSolution";
import { InfoRow } from "../components/InfoRow";
import Embed from "flat-embed";
import { useInput } from "../components/Input";

export function StudentSolution() {
    const params = useParams();
    const navigate = useNavigate();

    const [solution, setSolution] = useState(null)
    const [embed, setEmbed] = useState(null);
    const [studentEmbed, setStudentEmbed] = useState(null);

    const [grade, setGrade, gradeInput] = useInput({ type: "number", label: "Ocjena" })
    const [percentage, setPercentage, percentageInput] = useInput({ type: "number", label: "Točnost" })
    const [comment, setComment, commentInput] = useInput({ type: "textarea", label: "Komentar" })

    useEffect(() => {
        (async () => {
            const res = await StudentSolutionsService.getStudentSolution(params.solutionId);
            setSolution(res);
            if (!!res.result) {
                setGrade(res.result.grade.value)
                setPercentage(res.result.percentage)
                setComment(res.result.comment)
            }

            const container = document.getElementById('embed-container');
            const embed = new Embed(container, {
                score: '63c5d4aed4118d34674b1b42',
                embedParams: {
                    appId: '63c5d787ddc7bc4767abc961',
                    controlsPosition: 'top'
                }
            });
            await embed.loadMusicXML(res.assignment.exercise.solution.musicXml)
            setEmbed(embed);

            const studentContainer = document.getElementById('embed-container-student');
            const studentEmbed = new Embed(studentContainer, {
                score: '63c5d4aed4118d34674b1b42',
                embedParams: {
                    appId: '63c5d787ddc7bc4767abc961',
                    controlsPosition: 'top',
                    controlsDisplay: false,
                    controlsPlay: false
                }
            });
            await studentEmbed.loadMusicXML(res.solution.musicXml)
            setStudentEmbed(studentEmbed);
        })()
    }, [])

    const saveResult = async () => {
        await StudentSolutionsService.updateStudentSolutionResult({
            studentSolutionId: solution.id,
            grade,
            percentage,
            comment
        })

        navigate("/main")
    }

    const downloadSolution = (e) => {
        e.getMusicXML({ compressed: true }).then(function (buffer) {
            // Create a Blob from a compressed MusicXML file (Uint8Array)
            var blobUrl = window.URL.createObjectURL(new Blob([buffer], {
                type: 'application/vnd.recordare.musicxml+xml'
            }));

            // Create a hidden link to download the blob
            var a = document.createElement('a');
            a.href = blobUrl;
            a.download = 'MusicSheet.mxl';
            document.body.appendChild(a);
            a.style = 'display: none';
            a.click();
            a.remove();
        });
    }

    return <>
        <div className="container">
            <Title title={solution !== null ? solution.assignment.exercise.title : ""} />
            <div className="full-width-container">
                <div className="box-container">
                    {gradeInput}
                    {percentageInput}
                    {commentInput}
                </div>
            </div>
            <div className="button-container">
                <button className="button" onClick={() => saveResult()}>Spremi ocjenu</button>
                <button className="button" onClick={() => navigate(-1)}>Odustani</button>
            </div>
            <SmallTitle title="Predano rješenje"></SmallTitle>
            <div className="sheet">
                <div id="embed-container-student" className="embed-container"></div>
            </div>
            <SmallTitle title="Točno rješenje"></SmallTitle>
            <div className="sheet">
                <div id="embed-container" className="embed-container"></div>
            </div>
            <div className="button-container">
                <button className="button" onClick={() => downloadSolution(embed)}>Preuzmi točno rješenje</button>
                <button className="button" onClick={() => downloadSolution(studentEmbed)}>Preuzmi predano rješenje</button>
                <button className="button" onClick={() => navigate(-1)}>Natrag</button>
            </div>
        </div>
    </>
}