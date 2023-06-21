import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Header } from './layout/Header';
import { StudentMain } from './studentPages/StudentMain';
import { StudentAssignment } from './studentPages/StudentAssignment';
import { StudentGroups } from './studentPages/StudentGroups';
import { StudentGroup } from './studentPages/StudentGroup';
import { ResultReview } from './studentPages/ResultReview';
import { ResultReviews } from './studentPages/ResultReviews';
import { LoginStudent, RegisterStudent } from './studentPages/StudentLogin';
import { Error } from "./studentPages/Error";
import { PrivateRoute } from "./components/PrivateRoute";
import './App.css';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Header />
        <main>
          <Routes>
            <Route path="" element={<StudentMain />} />

            <Route path="/student/login" element={<LoginStudent />} />
            <Route path="/student/register" element={<RegisterStudent />} />
            <Route path="/student/main" element={<PrivateRoute><StudentMain /></PrivateRoute>} />

            <Route path="/student/groups" element={<PrivateRoute><StudentGroups /></PrivateRoute>} />
            <Route path="/student/groups/:groupId" element={<PrivateRoute><StudentGroup /></PrivateRoute>} />

            <Route path="/student/assignments/:assignmentId" element={<PrivateRoute><StudentAssignment /></PrivateRoute>} />
            <Route path="/student/assignments/:assignmentId/results" element={<PrivateRoute><ResultReviews /></PrivateRoute>} />
            <Route path="/student/assignments/:assignmentId/results/:solutionId" element={<PrivateRoute><ResultReview /></PrivateRoute>} />

            <Route path="/*" element={<Error />} />
          </Routes>
        </main>
      </BrowserRouter>
    </div>
  );
}

export default App;
