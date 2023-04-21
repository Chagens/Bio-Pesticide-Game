import React, { useState } from 'react';


export default function Auth(props) {
    let [authMode, setAuthMode] = useState('signin')
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [password2, setPassword2] = useState('');
    const [first_name, setFirstName] = useState('');
    const [last_name, setLastName] = useState('');
    const [research_experience, setResearchExperience] = useState('');

    const changeAuthMode = () => {
        setAuthMode(authMode === 'signin' ? 'signup' : 'signin')
    }

    const handleLogin = async (event) => {
        event.preventDefault();
        const response = await fetch('http://localhost:8000/api/get-details/', {
            method: 'GET',
            mode: 'cors',
            body: JSON.stringify({ email, password }),
          });
          if (response.ok) {
            console.log('Logged in successfully');
          } else {
            console.error('Error Login failed');
          }
    };

    const handleRegister = async (event) => {
        event.preventDefault();
        const response = await fetch('http://localhost:8000/api/register/', {
            method: 'POST',
            headers: {},
            mode: 'no-cors',
            body: JSON.stringify({ first_name, last_name, email, password, password2, research_experience }),
        });
        if (response.ok) {
            console.log('User registered successfully');
        } else {
            console.error('Error registration failed');
        }
    };

    if (authMode === 'signin') {
        return (
            <div className="Auth-form-container">
                <form className='Auth-form' onSubmit={handleLogin}>
                    <div className='Auth-form-content'>
                        <h3 className='Auth-form-title'>Login</h3>
                        <div className='text-center'>
                            Not registered yet?{" "}
                            <span className='link-primary' onClick={changeAuthMode}>
                                Sign Up
                            </span>
                        </div>
                        <div className='form-group mt-3'>
                            <label>Email address</label>
                            <input
                                type='email'
                                value={email}
                                className='form-control mt-1'
                                placeholder='Enter email'
                                onChange={(event) => setEmail(event.target.value)}
                            />
                        </div>
                        <div className='form-group mt-3'>
                            <label>Password</label>
                            <input 
                                type='password'
                                value={password}
                                className='form-control mt-3'
                                placeholder='Enter password'
                                onChange={(event) => setPassword(event.target.value)}
                            />
                        </div>
                        <div className='d-grid gap-2 mt-3'>
                            <button type='submit' className='btn btn-primary'>
                                Submit
                            </button>
                        </div>
                        <p className='forgot-password text-right mt-2'>
                            Forgot passowrd?
                        </p>
                    </div>
                </form>
            </div>
        )
    }

    return (
        <div className='Auth-form-container'>
            <form className='Auth-form' onSubmit={handleRegister}>
                <div className='Auth-form-content'>
                    <h3 className='Auth-form-title'>Sign Up</h3>
                    <div className='text-center'>
                        Already registered?{" "}
                        <span className='link-primary' onClick={changeAuthMode}>
                            Sign In
                        </span>
                    </div>
                    <div className='form-group mt-3'>
                        <label>First Name</label>
                        <input
                            type='text'
                            value={first_name}
                            className='form-control mt-1'
                            placeholder='First Name'
                            onChange={(event) => setFirstName(event.target.value)}
                        />
                        <div className='form-group mt-3'>
                            <label>Last Name</label>
                            <input
                                type='text'
                                value={last_name}
                                className='form-control mt-1'
                                placeholder='Last Name'
                                onChange={(event) => setLastName(event.target.value)}
                            />
                        </div>
                        <div className='form-group mt-3'>
                            <label>Email address</label>
                            <input 
                                type='email'
                                value={email}
                                className='form-control mt-1'
                                placeholder='Email Address'
                                onChange={(event) => setEmail(event.target.value)}
                            />
                        </div>
                        <div className='form-group mt-3'>
                            <label>Password</label>
                            <input
                                type='password'
                                value={password}
                                className='form-control mt-1'
                                placeholder='Password'
                                onChange={(event) => setPassword(event.target.value)}
                            />
                        </div>
                        <div className='form-group mt-3'>
                            <label>Re-Enter Password</label>
                            <input
                                type='password'
                                value={password2}
                                className='form-control mt-1'
                                placeholder='Password'
                                onChange={(event) => setPassword2(event.target.value)}
                            />
                        </div>
                        <div className='form-group mt-3'>
                            <label>Research Experience</label>
                            <input type='radio' id='r1' name='research_experience' value='None' className='form-control mt-1' checked={research_experience === 'None'} onChange={(event) => setResearchExperience(event.target.value)}/>
                            <label for="r1">None</label>
                            <input type='radio' id='r2' name='research_experience' value='Undergrad' className='form-control mt-1' checked={research_experience === 'Undergrad'} onChange={(event) => setResearchExperience(event.target.value)}/>
                            <label for="r2">Undergrad</label>
                            <input type='radio' id='r3' name='research_experience' value='Postgrad' className='form-control mt-1' checked={research_experience === 'Postgrad'} onChange={(event) => setResearchExperience(event.target.value)}/>
                            <label for="r3">Graduate</label>
                            <input type='radio' id='r4' name='research_experience' value='Doctoral' className='form-control mt-1' checked={research_experience === 'Doctoral'} onChange={(event) => setResearchExperience(event.target.value)}/>
                            <label for="r4">Doctoral</label>
                        </div>
                        <div className='d-grid gap-2 mt-3'>
                            <button type='submit' className='btn btn-primary'>
                                Submit
                            </button>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    )

}