import React, {useState} from 'react';
import "../templates/game.css";
import "../templates/App.css";


export default function Comment() {
    const [username, setUsername] = useState('');
    const [research_experience, setResearchExperience] = useState('');
    const [email, setEmail] = useState('');
    const [subject, setSubject] = useState('');
    const [description, setDescription] = useState('');


    const handleSubmit = async (event) => {
        event.preventDefault();
        const response = await fetch('http://localhost:8000/api/comments/', {
        method: 'POST',
        mode: 'cors',
        body: JSON.stringify({ username, research_experience, email, subject, description }),
        });
        if (response.ok) {
            console.log('Comment Posted');
            setSubject('');
            setDescription('');
        } else {
            console.error('Error comment not posted')
        }
    };

    return (
        <div className='container'>
        <h1 className='Game-title'>SoyBoy</h1>
        <a className="comment-link" href={'/game/'}>Play Game</a>
        <div className='Auth-form-container'>
            <form className='Auth-form' onSubmit={handleSubmit}>
                <div className='Auth-form-content'>
                    <h3 className='Auth-form-title'>Comment on Beta</h3>
                    <div className="form-group mt-3">
                        <label>Username</label>
                        <input 
                            type='text'
                            defaultValue={username}
                            className='form-control mt-1'
                            placeholder='Username'
                            onChange={(event) => setUsername(event.target.value)}
                        />
                    </div>
                    <div className='form-group mt-3'>
                        <label>Email</label>
                        <input
                            type='email'
                            value={email}
                            className='form-control mt-1'
                            placeholder='email@university.com'
                            onChange={(event) => setEmail(event.target.value)}
                        />
                    </div>
                    <div className='form-group mt-3'>
                        <label>Research Experience</label>
                        <div className='selection-box'>
                            <input type='radio' name='research_experience' value='None' checked={research_experience === 'None'} onChange={(event) => setResearchExperience(event.target.value)}/>
                            <label>None</label><br/>
                            <input type='radio' name='research_experience' value='Undergrad'  checked={research_experience === 'Undergrad'} onChange={(event) => setResearchExperience(event.target.value)}/>
                            <label>Undergrad</label><br/>
                            <input type='radio' name='research_experience' value='Postgrad'  checked={research_experience === 'Postgrad'} onChange={(event) => setResearchExperience(event.target.value)}/>
                            <label>Graduate</label><br/>
                            <input type='radio' name='research_experience' value='Doctoral'  checked={research_experience === 'Doctoral'} onChange={(event) => setResearchExperience(event.target.value)}/>
                            <label>Doctoral</label>
                        </div>
                    </div>
                    <div className="form-group mt-3">
                        <label>Comment</label>
                        <input
                            type='text'
                            value={subject}
                            className='form-control mt-1'
                            placeholder="Subject"
                            onChange={(event) => setSubject(event.target.value)}
                        />
                    </div>
                    <div className="form-group mt-3">
                        <input
                            type='text'
                            value={description}
                            placeholder='"enter comment here"'
                            className="form-control mt-1"
                            onChange={(event) => setDescription(event.target.value)}
                        />
                    </div>
                    <div className='d-grid gap-2 mt-3'>
                        <button type='submit' className='btn btn-primary'>
                            Submit
                        </button>
                    </div>
                </div>
            </form>
        </div>
        </div>
        );
    }