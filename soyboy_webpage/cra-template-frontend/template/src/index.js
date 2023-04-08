import { createRoot } from "react-dom/client";
import React from "react";
import reportWebVitals from './reportWebVitals';

import {
    createBrowserRouter,
    RouterProvider,
  } from "react-router-dom";

import App from './App';
import { Game } from "./components/game";
import Auth from './components/Auth'
import "./templates/index.css";

const router = createBrowserRouter([
    {
      path: "/",
      element: <App />,
    },
    {
      path: "/auth/",
      element: <Auth />
    },
    {
      path: "/game/",
      element: <Game />
    }
  ]);

const root = createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
)

reportWebVitals();