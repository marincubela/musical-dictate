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

            <Route path="/groups" element={<Groups />} />
            <Route path="/groups/:groupId" element={<Group />} />
            <Route path="/group/create" element={<CreateGroup />} />
            
            <Route path="/assignments/groups" element={<AssignmentsGroups />} />
            <Route path="/assignments/groups/:groupId" element={<AssignmentsGroup />} />
            <Route path="/assignments/:assignmentId" element={<Assignment />} />

            <Route path="/solutions/:solutionId" element={<StudentSolution />} />

            <Route path="/exercises" element={<Exercises />} />
            <Route path="/exercises/:exerciseId" element={<Exercise />} />
            <Route path="/exercise/create" element={<CreateExercise />} />

            <Route path="/*" element={<Error />} />
          </Routes>
        </main>
      </BrowserRouter>

    </div>
  );
}

export default App;
