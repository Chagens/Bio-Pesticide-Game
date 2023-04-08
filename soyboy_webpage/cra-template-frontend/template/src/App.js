import 'bootstrap/dist/css/bootstrap.min.css'
import './templates/App.css';
import './templates/index.css';

function App() {
  return (
    <div className="Home-page">
        <h1 className="Home-page-title">Bio-Pesticide Game</h1>
        <a className="login-link" href={'/auth/'}>login</a>
        <a className="login-link" href={'/game/'}>play</a>
    </div>
)
}

export default App;
