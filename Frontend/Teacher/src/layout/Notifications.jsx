import { useNavigate } from "react-router-dom"
import "../styles/notifications.css"

export function Notifications({ messages, close }) {
    const navigate = useNavigate();

    return (
        <div className="notifications-container">
            {messages.map(message => {
                return <div
                    onClick={() => navigate(`/solutions/${message.solutionId}`)} 
                    className="notification-list-item">
                        <span>

                    Učenik {message.firstName + " " + message.lastName} predao je rješenje.
                        </span>
                        <span>{new Date().toLocaleString()}</span>
                </div>
            })}
        </div>
    )
}