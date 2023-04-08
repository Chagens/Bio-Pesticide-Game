# soyboy_webpage
## Installing python requirements for backend
1. Create a virtual environment for python packages using  ` python -m venv <env_name>` or `conda create -n <env_name> python=3.10`
2. Activate the environment 
```
# Python environment
source <env_name>/bin/activate

# Conda environment 
conda activate <env_name>
```
3. Install requirements
```
pip install -r soyboy_webpage/backend/requirements.txt
```
4. Create a local environment file with the template. This file contains the settings for the django secret key, Debug setting, and allowed hosts. It isn't necessary to change any of this now, but once we deploy this we will need to change these settings.
```
cp env.template .env
```

### Testing backend
1. Go to the `soyboy_webpage/backend/` folder
2. Run this command to create a new super user so you can access the admin page. It will ask you for user details like username and password.
```
python manage.py createsuperuser
```
3. Start the server (currently its set to developer mode)
``` 
python manage.py runserver 
```

- Paste `http://127.0.0.1:8000/admin` into web browser. 
- This should take you to a login page. Enter you login info you just made, and then you should be taken to the admin page. From here you can create new users, questions, and whatever other models have be included so far.

## Create React App development environment
1. Install create-react-app globally (`-g`) 
```
npm install -g create-react-app
```
2. From `/soyboy_webpage/` run:
```
create-react-app frontend --template file:./cra-template-frontend
```
- This may take a moment. Once its finished the `/frontend/` folder will be created with all of the dependancies installed to the frontend react app
3. From `/soyboy_webpage/frontend/` run this command to start the app in developer mode.
```
npm start
```
- This should open the home page where you'll find links to the game and login page.

### Notes
- The process of updating the Unity build on the webpage requires you to copy the unity build files into the the `frontend/public/unitybuild/` folder. I'm not sure why, but the package that allows us to deploy the game with react.js only works if the build is put in that location. Also Unity doesn't seem to allow us to build the game directly into that folder. I'm sure there is a way around all this, but I haven't foudn it yet. It is also possible we could store the build on the backend and have the frontend request it upon launching. I'll need to try it out to see if there are any performance hits with either method.
- The login page is currently entirely aesthetic. The buttons and input boxes are functional, but they don't communicate with the backend to login or create a user. Once I've finished with the user authentification components on the backend, and figured out how to pass user settings to the unity instance, I'll implement the login functionality. Until those things are working there isn't much of a reason to have users or logins. 
- The end goal with the back and front end components is to store user, game question, game assets, and things like that stored with the djangorest backend. The frontend will then request all of this information to setup the game according to the current unity build and the users settings. With this setup we should be able to update assets, add new questions, and add new additional reading from the admin page without needing to change any code. This should make it easier for the sponsors to make some changes to the game and webpage even after the end of the semester. 
