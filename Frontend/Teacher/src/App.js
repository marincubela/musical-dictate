import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { Header } from './layout/Header';
import { Exercise } from './pages/Exercise';
import { Error } from './pages/Error';
import { Main } from './pages/Main';
import { CreateGroup, Groups } from './pages/Groups';
import { Group } from './pages/Group';
import { CreateExercise, Exercises } from './pages/Exercises';
import { AssignmentsGroups } from './pages/AssignmentsGroups';
import { AssignmentsGroup } from './pages/AssignmentsGroup';
import { Assignment } from './pages/Assignment';
import { StudentSolution } from './pages/StudentSolution';
import { LoginTeacher, RegisterTeacher } from './pages/Login';
import './App.css';
import { PrivateRoute } from './components/PrivateRoute';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Header />
        <main>
          <Routes>
            <Route path="" element={<Main />} />
            <Route path="/login" element={<LoginTeacher />} />
            <Route path="/register" element={<RegisterTeacher />} />

            <Route path="/main" element={<Main />} />

            <Route path="/groups" element={<PrivateRoute><Groups /></PrivateRoute>} />
            <Route path="/groups/:groupId" element={<PrivateRoute><Group /></PrivateRoute>} />
            <Route path="/group/create" element={<PrivateRoute><CreateGroup /></PrivateRoute>} />
            
            <Route path="/assignments/groups" element={<PrivateRoute><AssignmentsGroups /></PrivateRoute>} />
            <Route path="/assignments/groups/:groupId" element={<PrivateRoute><AssignmentsGroup /></PrivateRoute>} />
            <Route path="/assignments/:assignmentId" element={<PrivateRoute><Assignment /></PrivateRoute>} />

            <Route path="/solutions/:solutionId" element={<PrivateRoute><StudentSolution /></PrivateRoute>} />

            <Route path="/exercises" element={<PrivateRoute><Exercises /></PrivateRoute>} />
            <Route path="/exercises/:exerciseId" element={<PrivateRoute><Exercise /></PrivateRoute>} />
            <Route path="/exercise/create" element={<PrivateRoute><CreateExercise /></PrivateRoute>} />

            <Route path="/*" element={<Error />} />
          </Routes>
        </main>
      </BrowserRouter>

    </div>
  );
}

export default App;
