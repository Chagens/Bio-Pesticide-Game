import React, { useState } from 'react'

export default function Auth(props) {
    let [authMode, setAuthMode] = useState('signin')

    const changeAuthMode = () => {
        setAuthMode(authMode === 'signin' ? 'signup' : 'signin')
    }

    if (authMode === 'signin') {
        return (
            <div className="Auth-form-container">
                <form className='Auth-form'>
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
                                className='form-control mt-1'
                                placeholder='Enter email'
                            />
                        </div>
                        <div className='form-group mt-3'>
                            <label>Password</label>
                            <input 
                                type='password'
                                className='form-control mt-3'
                                placeholder='Enter password'
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
            <form className='Auth-form'>
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
                            className='form-control mt-1'
                            placeholder='First Name'
                        />
                        <div className='form-group mt-3'>
                            <label>Last Name</label>
                            <input
                                type='text'
                                className='form-control mt-1'
                                placeholder='Last Name'
                            />
                        </div>
                        <div className='form-group mt-3'>
                            <label>Email address</label>
                            <input 
                                type='email'
                                className='form-control mt-1'
                                placeholder='Email Address'
                            />
                        </div>
                        <div classname='form-group mt-3'>
                            <label>Password</label>
                            <input
                                type='password'
                                className='form-control mt-1'
                                placeholder='Password'
                            />
                        </div>
                        <div classname='form-group mt-3'>
                            <label>Re-Enter Password</label>
                            <input
                                type='password'
                                className='form-control mt-1'
                                placeholder='Password'
                            />
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